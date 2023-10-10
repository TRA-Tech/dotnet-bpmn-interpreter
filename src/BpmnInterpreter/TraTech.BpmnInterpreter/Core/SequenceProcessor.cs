﻿using System.Collections;
using System.Linq;
using System.Xml.Linq;
using TraTech.BpmnInterpreter.Abstractions;
using TraTech.BpmnInterpreter.Core.SequenceElements;
using TraTech.BpmnInterpreter.Enums;

namespace TraTech.BpmnInterpreter.Core
{
    public class SequenceProcessor : BaseSequenceProcessor
    {
        private readonly Dictionary<string, ProcessorElementState> _elementStateDict = new();
        private readonly LinkedList<BpmnSequenceElement> _elementsToBeProcessed = new();
        private LinkedListNode<BpmnSequenceElement>? _iterator;

        private readonly SequenceElementHandlerContext _handlerContext;
        public SequenceProcessor(BpmnSequenceProcessorData bpmnSequenceProcessorBuilderData)
            : base(bpmnSequenceProcessorBuilderData)
        {
            _handlerContext = new SequenceElementHandlerContext(this);

            if (bpmnSequenceProcessorBuilderData.BpmnSequence is not null)
            {
                foreach (var element in bpmnSequenceProcessorBuilderData.BpmnSequence.BpmnSequenceElements)
                {
                    _elementStateDict.Add(element.Id, ProcessorElementState.Ready);
                }
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

            var elementHandlerContext = GetHandlerContext();

            _iterator = _elementsToBeProcessed.First;
            while (_iterator != null)
            {
                var currentElement = _iterator.Value;
                var currentElementState = _elementStateDict[currentElement.Id];

                var elementHandler = data.ElementHandlerMap[currentElement.Type];
                if (currentElement
                    .PreviousElements
                    .All(a => _elementStateDict[a.Id] == ProcessorElementState.Processed))
                {
                    elementHandler.Process(currentElement, elementHandlerContext);
                    foreach (var nextElement in currentElement.NextElements)
                    {
                        var nextElementState = _elementStateDict[nextElement.Id];
                        if (nextElementState == ProcessorElementState.Ready)
                            _elementsToBeProcessed.AddLast(nextElement);
                    }
                    _iterator = _iterator.Next;
                    _elementsToBeProcessed.Remove(currentElement);
                    _elementStateDict[currentElement.Id] = ProcessorElementState.Processed;
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
        }

        public override ISequenceElementHandlerContext GetHandlerContext()
        {
            return _handlerContext;
        }
    }
}
