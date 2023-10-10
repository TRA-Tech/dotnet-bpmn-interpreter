using TraTech.BpmnInterpreter.Abstractions;
using TraTech.BpmnInterpreter.Core.Elements;
using TraTech.BpmnInterpreter.Core.SequenceElements;

namespace TraTech.BpmnInterpreter.Core
{
    public class Sequence : BaseSequence
    {
        public Sequence() { }

        public Sequence(IEnumerable<BpmnElement> bpmnElements) : base(bpmnElements) { }

        protected override void SetBpmnSequenceElements(IEnumerable<BpmnElement> bpmnElements)
        {
            _bpmnSequenceElements = bpmnElements
                .Where(w => SequenceFlowElements.Any(a => a.SourceRef == w.Id || a.TargetRef == w.Id))
                .Select(s => new BpmnSequenceElement(s.Self))
                .ToList();

            foreach (var groupedSequenceFlow in SequenceFlowElements.GroupBy(g => g.TargetRef))
            {
                var targetElement = _bpmnSequenceElements.First(f => f.Id == groupedSequenceFlow.Key);
                targetElement.PreviousElements.AddRange(
                    _bpmnSequenceElements.Where(w => groupedSequenceFlow.Any(a => a.SourceRef == w.Id))
                );
            }

            foreach (var groupedSequenceFlow in SequenceFlowElements.GroupBy(g => g.SourceRef))
            {
                var sourceElement = _bpmnSequenceElements.First(f => f.Id == groupedSequenceFlow.Key);
                sourceElement.NextElements.AddRange(
                    _bpmnSequenceElements.Where(w => groupedSequenceFlow.Any(a => a.TargetRef == w.Id))
                );
            }
        }
    }
}
