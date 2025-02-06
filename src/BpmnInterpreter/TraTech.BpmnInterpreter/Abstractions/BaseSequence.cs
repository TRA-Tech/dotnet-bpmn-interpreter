using TraTech.BpmnInterpreter.Core.Elements;
using TraTech.BpmnInterpreter.Core.SequenceElements;

namespace TraTech.BpmnInterpreter.Abstractions
{
    public abstract class BaseSequence
    {
        protected ICollection<BpmnSequenceElement> bpmnSequenceElements;
        public IEnumerable<BpmnSequenceElement> BpmnSequenceElements { get => bpmnSequenceElements; }

        protected IEnumerable<BpmnElement> bpmnElements;
        public IEnumerable<BpmnElement> BpmnElements { get => bpmnElements; }

        public IEnumerable<StartEvent> StartEventElements
        {
            get => bpmnSequenceElements
                .Where(w => w.Type == StartEvent.ElementTypeName)
                .Select(s => new StartEvent(s.Self, s.NextElements));
        }

        public IEnumerable<EndEvent> EndEventElements
        {
            get => bpmnSequenceElements
                .Where(w => w.Type == EndEvent.ElementTypeName)
                .Select(s => new EndEvent(s.Self, s.PreviousElements));
        }

        public IEnumerable<ExclusiveGateway> ExclusiveGatewayElements
        {
            get => bpmnSequenceElements
                .Where(w => w.Type == ExclusiveGateway.ElementTypeName)
                .Select(s => new ExclusiveGateway(s.Self, s.PreviousElements));
        }

        public IEnumerable<BoundaryEvent> BoundaryEvents
        {
            get => bpmnSequenceElements
                .Where(w => w.Type == BoundaryEvent.ElementTypeName)
                .Select(s => new BoundaryEvent(s.Self, s.PreviousElements));
        }

        public IEnumerable<IntermediateCatchEvent> IntermediateCatchEvents
        {
            get => bpmnSequenceElements
                .Where(w => w.Type == IntermediateCatchEvent.ElementTypeName)
                .Select(s => new IntermediateCatchEvent(s.Self, s.PreviousElements));
        }

        protected ICollection<SequenceFlow> sequenceFlowElements;
        public IEnumerable<SequenceFlow> SequenceFlowElements { get => sequenceFlowElements; }

        public bool HasAStart { get => bpmnSequenceElements.Any(a => a.Type == StartEvent.ElementTypeName); }
        public bool HasAnEnd { get => bpmnSequenceElements.Any(a => a.Type == EndEvent.ElementTypeName); }

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

        public virtual void LoadSequence(IEnumerable<BpmnElement> bpmnElements)
        {
            this.bpmnElements = bpmnElements;
            SetSequenceFlowElements();
            SetBpmnSequenceElements();
        }

        protected virtual void SetSequenceFlowElements()
        {
            sequenceFlowElements = bpmnElements
                .Where(element => element.Type == SequenceFlow.ElementTypeName)
                .Select(s => new SequenceFlow(s.Self))
                .ToList();
        }

        protected abstract void SetBpmnSequenceElements();
    }
}
