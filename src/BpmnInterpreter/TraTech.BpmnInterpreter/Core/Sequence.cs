using TraTech.BpmnInterpreter.Abstractions;
using TraTech.BpmnInterpreter.Core.Elements;
using TraTech.BpmnInterpreter.Core.SequenceElements;

namespace TraTech.BpmnInterpreter.Core
{
    /// <summary>
    /// Represents a concrete implementation of a BPMN sequence.
    /// Builds a navigable graph of <see cref="BpmnSequenceElement"/> instances from raw BPMN elements and sequence flows.
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
        /// <param name="bpmnElements">The raw BPMN elements used to build the sequence.</param>
        public Sequence(IEnumerable<BpmnElement> bpmnElements) : base(bpmnElements) { }

        /// <summary>
        /// Materializes and connects <see cref="BpmnSequenceElement"/> instances.
        /// Populates element relationships (<c>PreviousElements</c>/<c>NextElements</c>) based on sequence flows and attaches
        /// <see cref="BoundaryEvent"/> instances to their referenced activities.
        /// </summary>
        protected override void SetBpmnSequenceElements()
        {
            var isInFlow = SequenceFlowElements
                .SelectMany(f => new[] { f.SourceRef, f.TargetRef })
                .ToHashSet(StringComparer.Ordinal);

            var boundaryEvents = bpmnElements
                .Where(w => w.Type == BoundaryEvent.ElementTypeName)
                .Select(s => new BoundaryEvent(s.Self))
                .ToList();

            var all = bpmnElements
                .Where(e => e.Type != BoundaryEvent.ElementTypeName && isInFlow.Contains(e.Id))
                .Select(e => SequenceElementFactory.Create(e.Self))
                .ToList();

            bpmnSequenceElements = all;

            var elementsById = all.ToDictionary(k => k.Id);

            foreach (var boundary in boundaryEvents)
            {
                if (elementsById.TryGetValue(boundary.AttachedToRef, out var attachedTo))
                {
                    attachedTo.Boundaries.Add(boundary);
                }
            }

            foreach (var flow in SequenceFlowElements)
            {
                if (!elementsById.TryGetValue(flow.SourceRef, out var source))
                {
                    continue;
                }
                if (!elementsById.TryGetValue(flow.TargetRef, out var target))
                {
                    continue;
                }

                source.NextElements.Add(target);
                target.PreviousElements.Add(source);
            }
        }
    }
}
