using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TraTech.BpmnInterpreter.Abstractions;
using TraTech.BpmnInterpreter.Core.Elements;
using TraTech.BpmnInterpreter.Core.SequenceElements;
using TraTech.BpmnInterpreter.Enums;
using static System.Net.Mime.MediaTypeNames;

namespace TraTech.BpmnInterpreter.Core
{
    public class SequenceProcessor : BaseSequenceProcessor
    {
        private readonly Dictionary<string, ProcessorElementState> _elementStateDict = new();
        private readonly LinkedList<BpmnSequenceElement> _elementsToBeProcessed = new();
        private LinkedListNode<BpmnSequenceElement>? _iterator;
        private readonly SequenceElementHandlerContext _handlerContext;
        private bool _stopFlag = false;

        public override ISequenceElementHandlerContext SequenceElementHandlerContext => _handlerContext;

        public SequenceProcessor(BpmnSequenceProcessorData bpmnSequenceProcessorBuilderData)
            : base(bpmnSequenceProcessorBuilderData)
        {
            if (bpmnSequenceProcessorBuilderData.DataMap is null) throw new ArgumentException($"{nameof(bpmnSequenceProcessorBuilderData.DataMap)} can not be null!");
            if (bpmnSequenceProcessorBuilderData.BpmnSequence is null) throw new ArgumentException($"{nameof(bpmnSequenceProcessorBuilderData.BpmnSequence)} can not be null!");

            _handlerContext = new SequenceElementHandlerContext(bpmnSequenceProcessorBuilderData.DataMap, bpmnSequenceProcessorBuilderData.BpmnSequence, this);

            foreach (var element in bpmnSequenceProcessorBuilderData.BpmnSequence.BpmnSequenceElements)
            {
                _elementStateDict.Add(element.Id, ProcessorElementState.Ready);
            }
        }

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


                var elementHandler = GetElementHandler(currentElement.Type);

                if (currentElement
                    .PreviousElements
                    .All(a => _elementStateDict[a.Id] == ProcessorElementState.Processed))
                {
                    ProcessElement(currentElement, elementHandler);
                }
                else
                {
                    var deletedPreviousElements = currentElement
                        .PreviousElements
                        .Any(w => _elementStateDict[w.Id] == ProcessorElementState.Processed) &&
                        currentElement
                        .PreviousElements
                        .Any(w => _elementStateDict[w.Id] == ProcessorElementState.Deleted);

                    if (deletedPreviousElements)
                    {
                        ProcessElement(currentElement, elementHandler);
                    }
                    else
                    {
                        _elementStateDict[currentElement.Id] = ProcessorElementState.Waiting;

                        var previousElements = currentElement
                            .PreviousElements
                            .Where(w => _elementStateDict[w.Id] == ProcessorElementState.Ready)
                            .Where(w => !_elementsToBeProcessed.Contains(w));

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

        public override void Stop()
        {
            _stopFlag = true;
        }

        public override void SetNextElement(BpmnSequenceElement nextElement)
        {
            if (nextElement == null) throw new ArgumentNullException(nameof(nextElement));
            if (_iterator == null) throw new InvalidOperationException("No current element to redirect from.");

            var currentElement = _iterator.Value;

            // Remove all existing next elements from the current element
            var removalElements = currentElement.NextElements.Except(new[] { nextElement }).ToList();

            foreach (var removalElement in removalElements)
            {
                currentElement.NextElements.Remove(removalElement);
                if (_elementStateDict.ContainsKey(removalElement.Id))
                {
                    _elementStateDict[removalElement.Id] = ProcessorElementState.Deleted;
                    MarkNextElementsAsDeleted(removalElement);
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

            // Update the processing queue if needed
            if (!_elementsToBeProcessed.Contains(nextElement))
            {
                _elementsToBeProcessed.AddLast(nextElement);
            }
        }
        private void MarkNextElementsAsDeleted(BpmnSequenceElement element)
        {
            foreach (var next in element.NextElements.ToList())
            {
                if (_elementStateDict.ContainsKey(next.Id) 
                    && next.PreviousElements.Any(prev => _elementStateDict[prev.Id] == ProcessorElementState.Processed) 
                    && next.PreviousElements.Any(w => _elementStateDict[w.Id] == ProcessorElementState.Deleted))
                {
                    _elementStateDict[next.Id] = ProcessorElementState.Deleted;
                    MarkNextElementsAsDeleted(next);
                }
            }
        }

        private void ProcessElement(BpmnSequenceElement currentElement, ISequenceElementHandler elementHandler)
        {
            if (_elementStateDict[currentElement.Id] != ProcessorElementState.Processed)
            {
                elementHandler.Process(currentElement, SequenceElementHandlerContext);
                _elementStateDict[currentElement.Id] = ProcessorElementState.Processed;
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
        //process methodu buraya al end eventin processini tamamla


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
