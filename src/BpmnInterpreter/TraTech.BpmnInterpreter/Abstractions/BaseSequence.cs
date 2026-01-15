using TraTech.BpmnInterpreter.Core.Elements;
using TraTech.BpmnInterpreter.Core.SequenceElements;

namespace TraTech.BpmnInterpreter.Abstractions
{
    /// <summary>
    /// Represents the base class for a BPMN sequence.
    /// Manages the raw BPMN elements, the materialized sequence elements, and the sequence flows between them.
    /// </summary>
    public abstract class BaseSequence
    {
        /// <summary>
        /// Backing collection that stores the materialized BPMN sequence elements.
        /// </summary>
        protected ICollection<BpmnSequenceElement> bpmnSequenceElements;

        /// <summary>
        /// Gets the materialized BPMN sequence elements in this sequence.
        /// </summary>
        public IEnumerable<BpmnSequenceElement> BpmnSequenceElements { get => bpmnSequenceElements; }

        /// <summary>
        /// Backing collection that stores the raw BPMN elements that the sequence was loaded from.
        /// </summary>
        protected IEnumerable<BpmnElement> bpmnElements;

        /// <summary>
        /// Gets the raw BPMN elements that the sequence was loaded from.
        /// </summary>
        public IEnumerable<BpmnElement> BpmnElements { get => bpmnElements; }


        /// <summary>
        /// Backing collection that stores the materialized sequence flow elements.
        /// </summary>
        protected ICollection<SequenceFlow> sequenceFlowElements;

        /// <summary>
        /// Gets the materialized sequence flow elements in this sequence.
        /// </summary>
        public IEnumerable<SequenceFlow> SequenceFlowElements { get => sequenceFlowElements; }

        /// <summary>
        /// Gets a value indicating whether the sequence contains at least one start event.
        /// </summary>
        public bool HasAStart { get => bpmnSequenceElements.Any(a => a.Type == StartEvent.ElementTypeName); }

        /// <summary>
        /// Gets a value indicating whether the sequence contains at least one end event.
        /// </summary>
        public bool HasAnEnd { get => bpmnSequenceElements.Any(a => a.Type == EndEvent.ElementTypeName); }


        /// <summary>
        /// Gets the materialized BPMN sequence elements of the specified type.
        /// </summary>
        /// <typeparam name="T">The BPMN sequence element type.</typeparam>
        /// <returns>The sequence elements of the specified type.</returns>
        public IEnumerable<T> ElementsOfType<T>() where T : BpmnSequenceElement
        {
            return bpmnSequenceElements.OfType<T>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSequence"/> class.
        /// </summary>
        /// <param name="bpmnElements">
        /// The raw BPMN elements to initialize the sequence with.
        /// When provided, <see cref="LoadSequence(IEnumerable{BpmnElement})"/> is invoked.
        /// </param>
        public BaseSequence(IEnumerable<BpmnElement>? bpmnElements = null)
        {
            bpmnSequenceElements = [];
            sequenceFlowElements = [];
            this.bpmnElements = bpmnElements ?? [];

            if (bpmnElements != null)
            {
                LoadSequence(bpmnElements);
            }
        }

        /// <summary>
        /// Loads (or reloads) the sequence from the specified collection of BPMN elements.
        /// </summary>
        /// <param name="bpmnElements">The raw BPMN elements.</param>
        public virtual void LoadSequence(IEnumerable<BpmnElement> bpmnElements)
        {
            this.bpmnElements = bpmnElements;
            SetSequenceFlowElements();
            SetBpmnSequenceElements();
        }

        /// <summary>
        /// Populates <see cref="sequenceFlowElements"/> from <see cref="bpmnElements"/>.
        /// </summary>
        protected virtual void SetSequenceFlowElements()
        {
            sequenceFlowElements = bpmnElements
                .Where(element => element.Type == SequenceFlow.ElementTypeName)
                .Select(s => new SequenceFlow(s.Self))
                .ToList();
        }

        /// <summary>
        /// Populates <see cref="bpmnSequenceElements"/> from <see cref="bpmnElements"/>.
        /// Implementations are expected to interpret raw BPMN elements and materialize them as
        /// <see cref="BpmnSequenceElement"/> instances.
        /// </summary>
        protected abstract void SetBpmnSequenceElements();
    }
}
