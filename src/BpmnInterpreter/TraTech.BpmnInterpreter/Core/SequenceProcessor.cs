using TraTech.BpmnInterpreter.Abstractions;
using TraTech.BpmnInterpreter.Core.SequenceElements;
using TraTech.BpmnInterpreter.Extensions;

namespace TraTech.BpmnInterpreter.Core
{
    /// <summary>
    /// Processes a BPMN sequence by scheduling and executing elements.
    /// </summary>
    /// <remarks>
    /// Element handlers can influence routing by returning a <see cref="SequenceNextDecision"/> from
    /// <see cref="ISequenceElementHandler.ProcessAsync"/>. If a handler returns
    /// <see cref="SequenceNextDecision.UseDefault"/>, the processor schedules the current element's default outgoing elements.
    /// </remarks>
    public class SequenceProcessor : BaseSequenceProcessor
    {
        private enum ElementDecision
        {
            Process,
            MarkDontProcess,
            Skip
        }

        private enum PreconditionStatus
        {
            AllDontProcess,
            Runnable,
            Waiting,
            Blocked
        }

        private enum ExecutionState
        {
            Ready,
            Processed,
            DontProcess,
        }

        private readonly Dictionary<string, ExecutionState> _elementStateDict = [];
        private readonly Queue<BpmnSequenceElement> _workQueue = new();
        private readonly HashSet<string> _scheduledIds = [];
        private readonly SequenceElementHandlerContextt _handlerContext;

        private bool _stopFlag = false;

        /// <summary>
        /// Retrieves the current processing state for an element id.
        /// </summary>
        /// <param name="elementId">The element id.</param>
        /// <returns>The state currently tracked by the processor.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the element id is not known by the processor.</exception>
        private ExecutionState GetState(string elementId)
        {
            if (!_elementStateDict.TryGetValue(elementId, out var state))
            {
                throw new KeyNotFoundException($"Element with id '{elementId}' does not exist in the processor state map.");
            }
            return state;
        }

        /// <summary>
        /// Gets the context passed to element and boundary event handlers.
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

            _handlerContext = new SequenceElementHandlerContextt(bpmnSequenceProcessorBuilderData.DataMap, bpmnSequenceProcessorBuilderData.BpmnSequence, this);

            foreach (var element in bpmnSequenceProcessorBuilderData.BpmnSequence.BpmnSequenceElements)
            {
                _elementStateDict.Add(element.Id, ExecutionState.Ready);
            }
        }

        /// <summary>
        /// Evaluates whether all previous elements have reached a terminal state
        /// (<see cref="ExecutionState.Processed"/> or <see cref="ExecutionState.DontProcess"/>).
        /// </summary>
        /// <param name="element">The element whose prerequisites will be evaluated.</param>
        /// <returns>The precondition evaluation result.</returns>
        private PreconditionStatus EvaluatePreconditions(BpmnSequenceElement element)
        {
            if (element.PreviousElements.Count == 0)
            {
                return PreconditionStatus.Runnable;
            }

            var anyReady = false;
            var anyProcessed = false;
            var allDontProcess = true;

            foreach (var previous in element.PreviousElements)
            {
                var state = GetState(previous.Id);
                if (state == ExecutionState.Ready)
                {
                    anyReady = true;
                    allDontProcess = false;
                    continue;
                }

                if (state == ExecutionState.Processed)
                {
                    anyProcessed = true;
                    allDontProcess = false;
                }
            }

            if (anyReady)
            {
                return PreconditionStatus.Waiting;
            }

            if (allDontProcess)
            {
                return PreconditionStatus.AllDontProcess;
            }

            return anyProcessed ? PreconditionStatus.Runnable : PreconditionStatus.Blocked;
        }

        private bool TryEnqueuePrerequisites(BpmnSequenceElement element)
        {
            var anyEnqueued = false;
            foreach (var previous in element.PreviousElements)
            {
                if (_elementStateDict.TryGetValue(previous.Id, out var state) && state == ExecutionState.Ready)
                {
                    anyEnqueued |= TryEnqueue(previous);
                }
            }

            return anyEnqueued;
        }

        /// <summary>
        /// Decides what to do with the current element based on its own state and the state of its prerequisites.
        /// </summary>
        private ElementDecision DecideForCurrentElement(BpmnSequenceElement currentElement)
        {
            var currentState = GetState(currentElement.Id);
            if (currentState is ExecutionState.Processed or ExecutionState.DontProcess)
            {
                return ElementDecision.Skip;
            }

            return EvaluatePreconditions(currentElement) switch
            {
                PreconditionStatus.Runnable => ElementDecision.Process,
                PreconditionStatus.AllDontProcess => ElementDecision.MarkDontProcess,
                PreconditionStatus.Waiting => ElementDecision.Skip,
                _ => ElementDecision.Skip,
            };
        }

        /// <summary>
        /// Enqueues an element for processing if it is not already scheduled.
        /// </summary>
        /// <param name="element">The element to enqueue.</param>
        /// <returns><c>true</c> if the element was enqueued; otherwise <c>false</c>.</returns>
        private bool TryEnqueue(BpmnSequenceElement element)
        {
            if (!_scheduledIds.Add(element.Id))
            {
                return false;
            }

            _workQueue.Enqueue(element);
            return true;
        }

        /// <summary>
        /// Executes the specified element by resolving its handler and processing it.
        /// </summary>
        private async System.Threading.Tasks.Task ExecuteCurrentElementAsync(BpmnSequenceElement currentElement, CancellationToken cancellationToken)
        {
            var elementHandler = GetElementHandler(currentElement.Type);
            await ProcessElementAsync(currentElement, elementHandler, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Executes the main element handler and returns its routing decision.
        /// </summary>
        /// <remarks>
        /// This is the only decision that influences routing. Boundary events are handled separately and do not affect
        /// what gets scheduled next.
        /// </remarks>
        private async Task<SequenceNextDecision> ExecuteElementHandlerAsync(BpmnSequenceElement currentElement, ISequenceElementHandler elementHandler, CancellationToken cancellationToken)
        {
            var nextDecision = await elementHandler.ProcessAsync(currentElement, SequenceElementHandlerContext, cancellationToken).ConfigureAwait(false);
            _elementStateDict[currentElement.Id] = ExecutionState.Processed;
            return nextDecision;
        }

        /// <summary>
        /// Executes non-interrupting boundary event handlers for the given element.
        /// </summary>
        /// <remarks>
        /// Boundary events are executed for side effects only; their execution does not influence routing.
        /// </remarks>
        private async System.Threading.Tasks.Task ExecuteBoundaryHandlersAsync(BpmnSequenceElement currentElement, CancellationToken cancellationToken)
        {
            foreach (var boundary in currentElement.Boundaries)
            {
                if (_stopFlag) return;
                cancellationToken.ThrowIfCancellationRequested();

                var boundaryElementHandler = GetBoundaryElementHandler(boundary.Type);
                await boundaryElementHandler.ProcessAsync(boundary, SequenceElementHandlerContext, cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Schedules next elements from the handler-provided decision.
        /// </summary>
        private void ScheduleFromDecision(BpmnSequenceElement currentElement, SequenceNextDecision nextDecision)
        {
            IReadOnlyList<BpmnSequenceElement>? nextElements = nextDecision switch
            {
                SequenceNextDecision.Default => currentElement.NextElements,
                SequenceNextDecision.OverrideNext o => o.Next,
                SequenceNextDecision.NoNext => null,
                _ => currentElement.NextElements,
            };

            if (nextElements is null)
            {
                return;
            }

            foreach (var nextElement in nextElements)
            {
                var nextElementState = GetState(nextElement.Id);
                if (nextElementState == ExecutionState.Ready)
                {
                    TryEnqueue(nextElement);
                }
            }
        }

        /// <summary>
        /// Processes an element by executing its handler, executing non-interrupting boundaries, and scheduling the next elements.
        /// </summary>
        private async System.Threading.Tasks.Task ProcessElementAsync(BpmnSequenceElement currentElement, ISequenceElementHandler elementHandler, CancellationToken cancellationToken)
        {
            if (_stopFlag) return;
            cancellationToken.ThrowIfCancellationRequested();

            if (GetState(currentElement.Id) == ExecutionState.Processed)
            {
                ScheduleFromDecision(currentElement, SequenceNextDecision.UseDefault());
                return;
            }

            var nextDecision = await ExecuteElementHandlerAsync(currentElement, elementHandler, cancellationToken).ConfigureAwait(false);
            await ExecuteBoundaryHandlersAsync(currentElement, cancellationToken).ConfigureAwait(false);
            ScheduleFromDecision(currentElement, nextDecision);
        }

        /// <summary>
        /// Retrieves the element handler for the specified element type.
        /// </summary>
        /// <param name="elementType">The type of the BPMN element.</param>
        /// <returns>The element handler for the specified type.</returns>
        private ISequenceElementHandler GetElementHandler(string elementType)
        {
            ISequenceElementHandler? elementHandler;
            if (data.ElementHandlerMap.TryGetValue(elementType, out ISequenceElementHandler? value))
            {
                elementHandler = value;
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

        /// <summary>
        /// Retrieves the boundary event handler for the specified boundary event type.
        /// </summary>
        /// <param name="elementType">The type of the BPMN boundary event.</param>
        /// <returns>The boundary event handler for the specified type.</returns>
        private IBoundaryEventHandler GetBoundaryElementHandler(string elementType)
        {
            IBoundaryEventHandler? elementHandler;
            if (data.BoundaryElementHandlerMap.TryGetValue(elementType, out var value))
            {
                elementHandler = value;
            }
            else
            {
                elementHandler = data.DefaultBoundaryElementHandler;
            }

            if (elementHandler is null)
            {
                throw new KeyNotFoundException($"BoundaryElementHandler with type '{elementType}' could not found in the {nameof(data.BoundaryElementHandlerMap)} and no DefaultBoundaryElementHandler provided!");
            }

            return elementHandler;
        }

        protected override void ResetRunState()
        {
            _stopFlag = false;
            _workQueue.Clear();
            _scheduledIds.Clear();

            if (data.BpmnSequence is null)
            {
                return;
            }

            foreach (var element in data.BpmnSequence.BpmnSequenceElements)
            {
                _elementStateDict[element.Id] = ExecutionState.Ready;
            }
        }

        /// <summary>
        /// Starts the execution of the BPMN sequence asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A token used to cancel the execution.</param>
        /// <remarks>
        /// Calling <see cref="Stop"/> requests a graceful stop; cancellation requests are honored via <paramref name="cancellationToken"/>.
        /// </remarks>
        public override async System.Threading.Tasks.Task StartAsync(CancellationToken cancellationToken = default)
        {
            ResetRunState();

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

            foreach (var startEvent in data.BpmnSequence.ElementsOfType<StartEvent>())
            {
                TryEnqueue(startEvent);
            }

            while (_workQueue.Count > 0)
            {
                if (_stopFlag) break;
                cancellationToken.ThrowIfCancellationRequested();
                var currentElement = _workQueue.Dequeue();
                _scheduledIds.Remove(currentElement.Id);

                switch (DecideForCurrentElement(currentElement))
                {
                    case ElementDecision.Process:
                        await ExecuteCurrentElementAsync(currentElement, cancellationToken).ConfigureAwait(false);
                        break;

                    case ElementDecision.MarkDontProcess:
                        _elementStateDict[currentElement.Id] = ExecutionState.DontProcess;
                        break;

                    case ElementDecision.Skip:
                        if (EvaluatePreconditions(currentElement) == PreconditionStatus.Waiting)
                        {
                            TryEnqueuePrerequisites(currentElement);
                            TryEnqueue(currentElement);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Requests the execution to stop.
        /// </summary>
        /// <remarks>
        /// This sets a stop flag which is checked between element executions.
        /// </remarks>
        public override void Stop()
        {
            _stopFlag = true;
        }
    }
}
