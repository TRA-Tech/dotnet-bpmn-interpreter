using System.Collections;
using TraTech.BpmnInterpreter.Core.BpmnElements;
using TraTech.BpmnInterpreter.Core.BpmnSequenceElements;

namespace TraTech.BpmnInterpreter.Abstractions
{
    public abstract class BpmnSequence
    {
        protected ICollection<BpmnSequenceElement> _bpmnSequenceElements;
        public IEnumerable<BpmnSequenceElement> BpmnSequenceElements { get => _bpmnSequenceElements; }

        protected ICollection<StartEvent> _startEventElements;
        public IEnumerable<StartEvent> StartEventElements
        {
            get => _bpmnSequenceElements
                .Where(w => w.Type == StartEvent.ElementTypeName)
                .Select(s => new StartEvent(s.Self, s.NextElements));
        }

        protected ICollection<EndEvent> _endEventElements;
        public IEnumerable<EndEvent> EndEventElements
        {
            get => _bpmnSequenceElements
                .Where(w => w.Type == EndEvent.ElementTypeName)
                .Select(s => new EndEvent(s.Self, s.PreviousElements));
        }

        protected ICollection<SequenceFlow> _sequenceFlowElements;
        public IEnumerable<SequenceFlow> SequenceFlowElements { get => _sequenceFlowElements; }

        public bool HasAStart { get => _startEventElements.Any(); }
        public bool HasAnEnd { get => _endEventElements.Any(); }

        public BpmnSequence(IEnumerable<BpmnElement>? bpmnElements = null)
        {
            _bpmnSequenceElements = new List<BpmnSequenceElement>();
            _startEventElements = new List<StartEvent>();
            _endEventElements = new List<EndEvent>();
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
