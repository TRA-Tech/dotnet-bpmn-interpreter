using TraTech.BpmnInterpreter.Core.Elements;
using TraTech.BpmnInterpreter.Core.SequenceElements;

namespace TraTech.BpmnInterpreter.Abstractions
{
    public abstract class BaseSequence
    {
        protected ICollection<BpmnSequenceElement> _bpmnSequenceElements;
        public IEnumerable<BpmnSequenceElement> BpmnSequenceElements { get => _bpmnSequenceElements; }

        public IEnumerable<StartEvent> StartEventElements
        {
            get => _bpmnSequenceElements
                .Where(w => w.Type == StartEvent.ElementTypeName)
                .Select(s => new StartEvent(s.Self, s.NextElements));
        }

        public IEnumerable<EndEvent> EndEventElements
        {
            get => _bpmnSequenceElements
                .Where(w => w.Type == EndEvent.ElementTypeName)
                .Select(s => new EndEvent(s.Self, s.PreviousElements));
        }

        protected ICollection<SequenceFlow> _sequenceFlowElements;
        public IEnumerable<SequenceFlow> SequenceFlowElements { get => _sequenceFlowElements; }

        public bool HasAStart { get => _bpmnSequenceElements.Any(a => a.Type == StartEvent.ElementTypeName); }
        public bool HasAnEnd { get => _bpmnSequenceElements.Any(a => a.Type == EndEvent.ElementTypeName); }

        public BaseSequence(IEnumerable<BpmnElement>? bpmnElements = null)
        {
            _bpmnSequenceElements = new List<BpmnSequenceElement>();
            _sequenceFlowElements = new List<SequenceFlow>();

            if (bpmnElements != null)
            {
                LoadSequence(bpmnElements);
            }
        }

        public virtual void LoadSequence(IEnumerable<BpmnElement> bpmnElements)
        {
            SetSequenceFlowElements(bpmnElements);
            SetBpmnSequenceElements(bpmnElements);
        }

        protected virtual void SetSequenceFlowElements(IEnumerable<BpmnElement> bpmnElements)
        {
            _sequenceFlowElements = bpmnElements
                .Where(element => element.Type == SequenceFlow.ElementTypeName)
                .Select(s => new SequenceFlow(s.Self))
                .ToList();
        }

        protected abstract void SetBpmnSequenceElements(IEnumerable<BpmnElement> bpmnElements);
    }
}
