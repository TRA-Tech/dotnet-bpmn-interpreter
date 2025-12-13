using TraTech.BpmnInterpreter.Core.Elements;
using TraTech.BpmnInterpreter.Core.SequenceElements;

namespace TraTech.BpmnInterpreter.Abstractions
{
    /// <summary>
    /// Represents the base class for a BPMN sequence, managing the elements and flow within the sequence.
    /// </summary>
    public abstract class BaseSequence
    {
        /// <summary>
        /// The collection of BPMN sequence elements.
        /// </summary>
        protected ICollection<BpmnSequenceElement> bpmnSequenceElements;

        /// <summary>
        /// Gets the collection of BPMN sequence elements.
        /// </summary>
        public IEnumerable<BpmnSequenceElement> BpmnSequenceElements { get => bpmnSequenceElements; }

        /// <summary>
        /// The collection of raw BPMN elements.
        /// </summary>
        protected IEnumerable<BpmnElement> bpmnElements;

        /// <summary>
        /// Gets the collection of raw BPMN elements.
        /// </summary>
        public IEnumerable<BpmnElement> BpmnElements { get => bpmnElements; }

        /// <summary>
        /// Gets the start event elements in the sequence.
        /// </summary>
        public IEnumerable<StartEvent> StartEventElements
        {
            get => bpmnSequenceElements
                .Where(w => w.Type == StartEvent.ElementTypeName)
                .Select(s => new StartEvent(s.Self, s.NextElements));
        }

        /// <summary>
        /// Gets the end event elements in the sequence.
        /// </summary>
        public IEnumerable<EndEvent> EndEventElements
        {
            get => bpmnSequenceElements
                .Where(w => w.Type == EndEvent.ElementTypeName)
                .Select(s => new EndEvent(s.Self, s.PreviousElements));
        }

        /// <summary>
        /// Gets the exclusive gateway elements in the sequence.
        /// </summary>
        public IEnumerable<ExclusiveGateway> ExclusiveGatewayElements
        {
            get => bpmnSequenceElements
                .Where(w => w.Type == ExclusiveGateway.ElementTypeName)
                .Select(s => new ExclusiveGateway(s.Self, s.PreviousElements));
        }

        /// <summary>
        /// Gets the boundary events in the sequence.
        /// </summary>
        public IEnumerable<BoundaryEvent> BoundaryEvents
        {
            get => bpmnSequenceElements
                .Where(w => w.Type == BoundaryEvent.ElementTypeName)
                .Select(s => new BoundaryEvent(s.Self, s.PreviousElements));
        }

        /// <summary>
        /// Gets the intermediate catch events in the sequence.
        /// </summary>
        public IEnumerable<IntermediateCatchEvent> IntermediateCatchEvents
        {
            get => bpmnSequenceElements
                .Where(w => w.Type == IntermediateCatchEvent.ElementTypeName)
                .Select(s => new IntermediateCatchEvent(s.Self, s.PreviousElements));
        }

        /// <summary>
        /// The collection of sequence flow elements.
        /// </summary>
        protected ICollection<SequenceFlow> sequenceFlowElements;

        /// <summary>
        /// Gets the collection of sequence flow elements.
        /// </summary>
        public IEnumerable<SequenceFlow> SequenceFlowElements { get => sequenceFlowElements; }

        /// <summary>
        /// Gets a value indicating whether the sequence has at least one start event.
        /// </summary>
        public bool HasAStart { get => bpmnSequenceElements.Any(a => a.Type == StartEvent.ElementTypeName); }

        /// <summary>
        /// Gets a value indicating whether the sequence has at least one end event.
        /// </summary>
        public bool HasAnEnd { get => bpmnSequenceElements.Any(a => a.Type == EndEvent.ElementTypeName); }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSequence"/> class.
        /// </summary>
        /// <param name="bpmnElements">The collection of BPMN elements to initialize the sequence with.</param>
        public BaseSequence(IEnumerable<BpmnElement>? bpmnElements = null)
        {
            bpmnSequenceElements = new List<BpmnSequenceElement>();
            sequenceFlowElements = new List<SequenceFlow>();
            this.bpmnElements = bpmnElements ?? Enumerable.Empty<BpmnElement>();

            if (bpmnElements != null)
            {
                LoadSequence(bpmnElements);
            }
        }

        /// <summary>
        /// Loads the sequence from the specified collection of BPMN elements.
        /// </summary>
        /// <param name="bpmnElements">The collection of BPMN elements.</param>
        public virtual void LoadSequence(IEnumerable<BpmnElement> bpmnElements)
        {
            this.bpmnElements = bpmnElements;
            SetSequenceFlowElements();
            SetBpmnSequenceElements();
        }

        /// <summary>
        /// Sets the sequence flow elements from the raw BPMN elements.
        /// </summary>
        protected virtual void SetSequenceFlowElements()
        {
            sequenceFlowElements = bpmnElements
                .Where(element => element.Type == SequenceFlow.ElementTypeName)
                .Select(s => new SequenceFlow(s.Self))
                .ToList();
        }

        /// <summary>
        /// Sets the BPMN sequence elements. This method must be implemented by derived classes.
        /// </summary>
        protected abstract void SetBpmnSequenceElements();
    }
}
