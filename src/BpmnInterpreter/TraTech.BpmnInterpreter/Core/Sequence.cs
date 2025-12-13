using TraTech.BpmnInterpreter.Abstractions;
using TraTech.BpmnInterpreter.Core.Elements;
using TraTech.BpmnInterpreter.Core.SequenceElements;

namespace TraTech.BpmnInterpreter.Core
{
    /// <summary>
    /// Represents a concrete implementation of a BPMN sequence.
    /// </summary>
    public class Sequence : BaseSequence
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sequence"/> class.
        /// </summary>
        public Sequence() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sequence"/> class with the specified BPMN elements.
        /// </summary>
        /// <param name="bpmnElements">The collection of BPMN elements.</param>
        public Sequence(IEnumerable<BpmnElement> bpmnElements) : base(bpmnElements) { }

        /// <summary>
        /// Sets the BPMN sequence elements by analyzing the sequence flows and connecting elements.
        /// </summary>
        protected override void SetBpmnSequenceElements()
        {
            bpmnSequenceElements = bpmnElements
                .Where(w => SequenceFlowElements.Any(a => a.SourceRef == w.Id || a.TargetRef == w.Id) ||
                w.Type == BoundaryEvent.ElementTypeName)
                .Select(s => new BpmnSequenceElement(s.Self))
                .ToList();

            foreach (var groupedSequenceFlow in SequenceFlowElements.GroupBy(g => g.TargetRef))
            {
                var targetElement = bpmnSequenceElements.First(f => f.Id == groupedSequenceFlow.Key);
                targetElement.PreviousElements.AddRange(
                    bpmnSequenceElements.Where(w => groupedSequenceFlow.Any(a => a.SourceRef == w.Id))
                );
            }

            foreach (var groupedSequenceFlow in SequenceFlowElements.GroupBy(g => g.SourceRef))
            {
                var sourceElement = bpmnSequenceElements.First(f => f.Id == groupedSequenceFlow.Key);
                sourceElement.NextElements.AddRange(
                    bpmnSequenceElements.Where(w => groupedSequenceFlow.Any(a => a.TargetRef == w.Id))
                );
            }
        }
    }
}
