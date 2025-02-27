﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TraTech.BpmnInterpreter.Abstractions;
using TraTech.BpmnInterpreter.Core.Elements;
using TraTech.BpmnInterpreter.Core.SequenceElements;
using TraTech.BpmnInterpreter.Enums;
using TraTech.BpmnInterpreter.Extensions;
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
        private IEnumerable<BpmnSequenceElement> _boundaryElements;

        public override ISequenceElementHandlerContext SequenceElementHandlerContext => _handlerContext;

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
                        //.Where(w => !_elementsToBeProcessed.Contains(w));

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
