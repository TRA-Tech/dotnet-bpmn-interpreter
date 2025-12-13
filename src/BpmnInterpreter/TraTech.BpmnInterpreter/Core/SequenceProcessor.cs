using TraTech.BpmnInterpreter.Abstractions;
using TraTech.BpmnInterpreter.Core.SequenceElements;
using TraTech.BpmnInterpreter.Enums;
using TraTech.BpmnInterpreter.Extensions;

namespace TraTech.BpmnInterpreter.Core
{
    /// <summary>
    /// Processes a BPMN sequence by executing elements in order.
    /// </summary>
    public class SequenceProcessor : BaseSequenceProcessor
    {
        private readonly Dictionary<string, ProcessorElementState> _elementStateDict = new();
        private readonly LinkedList<BpmnSequenceElement> _elementsToBeProcessed = new();
        private LinkedListNode<BpmnSequenceElement>? _iterator;
        private readonly SequenceElementHandlerContext _handlerContext;
        private bool _stopFlag = false;
        private IEnumerable<BpmnSequenceElement> _boundaryElements;

        /// <summary>
        /// Gets the context for the sequence element handler.
        /// </summary>
        public override ISequenceElementHandlerContext SequenceElementHandlerContext => _handlerContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceProcessor"/> class.
        /// </summary>
        /// <param name="bpmnSequenceProcessorBuilderData">The data used to build the sequence processor.</param>
        /// <exception cref="ArgumentException">Thrown when DataMap or BpmnSequence is null.</exception>
        public SequenceProcessor(BpmnSequenceProcessorData bpmnSequenceProcessorBuilderData)
            : base(bpmnSequenceProcessorBuilderData)
        {
            if (bpmnSequenceProcessorBuilderData.DataMap is null) throw new ArgumentException($"{nameof(bpmnSequenceProcessorBuilderData.DataMap)} can not be null!");
            if (bpmnSequenceProcessorBuilderData.BpmnSequence is null) throw new ArgumentException($"{nameof(bpmnSequenceProcessorBuilderData.BpmnSequence)} can not be null!");

            _handlerContext = new SequenceElementHandlerContext(bpmnSequenceProcessorBuilderData.DataMap, bpmnSequenceProcessorBuilderData.BpmnSequence, this);

            _boundaryElements = bpmnSequenceProcessorBuilderData.BpmnSequence.BpmnSequenceElements.Where(w => w.Type == BoundaryEvent.ElementTypeName);

            foreach (var element in bpmnSequenceProcessorBuilderData.BpmnSequence.BpmnSequenceElements)
            {
                _elementStateDict.Add(element.Id, ProcessorElementState.Ready);
            }
        }

        /// <summary>
        /// Starts the execution of the BPMN sequence.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when BpmnSequence is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the sequence does not have a start or end event.</exception>
        public override void Start()
        {
            if (data.BpmnSequence is null)
            {
                throw new ArgumentNullException(nameof(data.BpmnSequence), $"{nameof(data.BpmnSequence)} cannot be null.");
            }
            if (!data.BpmnSequence.HasAStart)
            {
                throw new InvalidOperationException($"{nameof(data.BpmnSequence)} does not have a {nameof(StartEvent)}");
            }
            if (!data.BpmnSequence.HasAnEnd)
            {
                throw new InvalidOperationException($"{nameof(data.BpmnSequence)} does not have an {nameof(EndEvent)}");
            }

            foreach (var startEvent in data.BpmnSequence.StartEventElements)
            {
                _elementsToBeProcessed.AddLast(startEvent);
            }

            _iterator = _elementsToBeProcessed.First;
            while (_iterator != null)
            {
                var currentElement = _iterator.Value;
                var currentElementState = _elementStateDict[currentElement.Id];

                var name = currentElement.Name;
                var elementHandler = GetElementHandler(currentElement.Type);

                if (currentElement
                    .PreviousElements
                    .All(a => _elementStateDict[a.Id] == ProcessorElementState.Processed))
                {
                    ProcessElement(currentElement, elementHandler);
                }
                else
                {
                    if (currentElement.PreviousElements.All(w => _elementStateDict[w.Id] == ProcessorElementState.DontProcess))
                    {
                        _elementStateDict[currentElement.Id] = ProcessorElementState.DontProcess;
                        _iterator = _iterator.Next;
                    }
                    else if (
                        currentElement.PreviousElements.All(w =>
                            _elementStateDict[w.Id] == ProcessorElementState.DontProcess ||
                            _elementStateDict[w.Id] == ProcessorElementState.Processed) &&
                        currentElement.PreviousElements.Any(w => _elementStateDict[w.Id] == ProcessorElementState.DontProcess) &&
                        currentElement.PreviousElements.Any(w => _elementStateDict[w.Id] == ProcessorElementState.Processed)
                    )
                        ProcessElement(currentElement, elementHandler);
                    else
                    {
                        _elementStateDict[currentElement.Id] = ProcessorElementState.Waiting;

                        var previousElements = currentElement
                                .PreviousElements
                                .Where(w => _elementStateDict[w.Id] == ProcessorElementState.Ready);

                        if (previousElements.Any())
                        {
                            foreach (var previousElement in previousElements)
                            {
                                _elementsToBeProcessed.AddFirst(previousElement);
                            }
                            _iterator = _elementsToBeProcessed.First;
                        }
                        else
                        {
                            _iterator = _iterator.Next;
                        }
                    }
                }

                if (_stopFlag) break;
            }
        }

        /// <summary>
        /// Stops the execution of the sequence.
        /// </summary>
        public override void Stop()
        {
            _stopFlag = true;
        }

        /// <summary>
        /// Sets the next element to be processed, modifying the flow of execution.
        /// </summary>
        /// <param name="nextElement">The next BPMN sequence element.</param>
        /// <exception cref="ArgumentNullException">Thrown when nextElement is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when there is no current element to redirect from.</exception>
        public override void SetNextElement(BpmnSequenceElement nextElement)
        {
            if (nextElement == null) throw new ArgumentNullException(nameof(nextElement));
            if (_iterator == null) throw new InvalidOperationException("No current element to redirect from.");

            var currentElement = _iterator.Value;

            // Remove all existing next elements from the current element
            var removalElements = currentElement.NextElements
                .Except(new[] { nextElement })
                .Where(w => w.Type != EndEvent.ElementTypeName && w.PreviousElements.Count() == 1)
                .ToList();

            foreach (var removalElement in removalElements)
            {
                if (_elementStateDict.ContainsKey(removalElement.Id))
                {
                    _elementStateDict[removalElement.Id] = ProcessorElementState.DontProcess;
                    if (_elementsToBeProcessed.Contains(removalElement))
                    {
                        _elementsToBeProcessed.Remove(removalElement);
                    }
                    MarkNextElementsAsDontProcess(removalElement);
                }

            }

            // Add the new next element
            if (!currentElement.NextElements.Contains(nextElement))
            {
                currentElement.NextElements.Add(nextElement);
            }

            // Update the previous elements of the next element
            if (!nextElement.PreviousElements.Contains(currentElement))
            {
                nextElement.PreviousElements.Add(currentElement);
            }

        }
        private void MarkNextElementsAsDontProcess(BpmnSequenceElement element)
        {
            foreach (var next in element.NextElements.ToList())
            {
                if (_elementStateDict.ContainsKey(next.Id) && next.PreviousElements.All(w => _elementStateDict[w.Id] == ProcessorElementState.DontProcess))
                {
                    _elementStateDict[next.Id] = ProcessorElementState.DontProcess;
                    if (_elementsToBeProcessed.Contains(next))
                    {
                        _elementsToBeProcessed.Remove(next);
                    }
                    MarkNextElementsAsDontProcess(next);
                }
            }
        }

        private void ProcessElement(BpmnSequenceElement currentElement, ISequenceElementHandler elementHandler)
        {
            if (_elementStateDict[currentElement.Id] != ProcessorElementState.Processed)
            {
                elementHandler.Process(currentElement, SequenceElementHandlerContext);
                _elementStateDict[currentElement.Id] = ProcessorElementState.Processed;

                foreach (var boundary in currentElement.GetBoundaries(_boundaryElements))
                {
                    if (_elementStateDict[boundary.Id] != ProcessorElementState.Processed)
                    {
                        var boundaryElementHandler = GetElementHandler(boundary.Type);
                        boundaryElementHandler.Process(boundary, SequenceElementHandlerContext);
                        _elementStateDict[boundary.Id] = ProcessorElementState.Processed;
                    }
                }
            }

            foreach (var nextElement in currentElement.NextElements)
            {
                var nextElementState = _elementStateDict[nextElement.Id];
                if (nextElementState == ProcessorElementState.Ready)
                    _elementsToBeProcessed.AddLast(nextElement);
            }
            _iterator = _iterator.Next is null ? _iterator.Previous : _iterator.Next;

            _elementsToBeProcessed.Remove(currentElement);
        }


        /// <summary>
        /// Retrieves the element handler for the specified element type.
        /// </summary>
        /// <param name="elementType">The type of the BPMN element.</param>
        /// <returns>The element handler for the specified type.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when no handler is found for the element type and no default handler is provided.</exception>
        public ISequenceElementHandler GetElementHandler(string elementType)
        {
            ISequenceElementHandler? elementHandler;
            if (data.ElementHandlerMap.ContainsKey(elementType))
            {
                elementHandler = data.ElementHandlerMap[elementType];
            }
            else
            {
                elementHandler = data.DefaultElementHandler;
            }

            if (elementHandler is null)
            {
                throw new KeyNotFoundException($"ElementHandler with type '{elementType}' could not found in the {nameof(data.ElementHandlerMap)} and no DefaultElementHandler provided!");
            }
            return elementHandler;
        }
    }
}
