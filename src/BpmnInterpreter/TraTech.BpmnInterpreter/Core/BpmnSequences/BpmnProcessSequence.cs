using TraTech.BpmnInterpreter.Core.BpmnElements;
using TraTech.BpmnInterpreter.Core.BpmnElements.SequenceElements;

namespace TraTech.BpmnInterpreter.Core.BpmnSequences
{
    public class BpmnProcessSequence
    {
        private readonly List<BpmnSequenceElement> _bpmnSequenceElements = new();
        public IEnumerable<BpmnSequenceElement> BpmnSequenceElements { get => _bpmnSequenceElements; }

        public readonly IEnumerable<StartEvent> StartElements;
        public readonly IEnumerable<EndEvent> EndElements;
        public readonly IEnumerable<SequenceFlow> SequenceFlows;

        public bool HasAStart { get => StartElements.Any(); }
        public bool HasAnEnd { get => EndElements.Any(); }

        public BpmnProcessSequence(IEnumerable<BpmnElement> elements)
        {
            SequenceFlows = elements
                .Where(element => element.Type == SequenceFlow.ElementTypeName)
                .Select(s => new SequenceFlow(s.Self))
                .ToList();

            _bpmnSequenceElements = elements
                .Where(w => SequenceFlows.Any(a => a.SourceRef == w.Id || a.TargetRef == w.Id))
                .Select(s => new BpmnSequenceElement(s.Self))
                .ToList();

            StartElements = _bpmnSequenceElements
                .Where(element => element.Type == StartEvent.ElementTypeName)
                .Select(s => new StartEvent(s.Self))
                .ToList();

            EndElements = _bpmnSequenceElements
                .Where(element => element.Type == EndEvent.ElementTypeName)
                .Select(s => new EndEvent(s.Self))
                .ToList();

            foreach (var groupedSequenceFlow in SequenceFlows.GroupBy(g => g.TargetRef))
            {
                var targetElement = _bpmnSequenceElements.First(f => f.Id == groupedSequenceFlow.Key);
                targetElement.PreviousElements.AddRange(
                    _bpmnSequenceElements.Where(w => groupedSequenceFlow.Any(a => a.SourceRef == w.Id))
                );
            }

            foreach (var groupedSequenceFlow in SequenceFlows.GroupBy(g => g.SourceRef))
            {
                var sourceElement = _bpmnSequenceElements.First(f => f.Id == groupedSequenceFlow.Key);
                sourceElement.NextElements.AddRange(
                    _bpmnSequenceElements.Where(w => groupedSequenceFlow.Any(a => a.TargetRef == w.Id))
                );
            }
        }
    }
}
