using TraTech.BpmnInterpreter.Core.SequenceElements;

namespace TraTech.BpmnInterpreter.Abstractions
{
    /// <summary>
    /// Represents a handler-provided decision for what element(s) should be scheduled next.
    /// </summary>
    /// <remarks>
    /// This is used by the processor after a handler finishes executing a BPMN element.
    /// A handler can either keep the default behavior (schedule the current element's outgoing elements),
    /// override the next elements explicitly (for example, for gateways), or schedule nothing.
    /// </remarks>
    public abstract record SequenceNextDecision
    {
        private SequenceNextDecision() { }

        /// <summary>
        /// Keeps the default outgoing flow of the current element.
        /// </summary>
        public sealed record Default : SequenceNextDecision;

        /// <summary>
        /// Overrides the outgoing flow of the current element, scheduling only the provided next element(s).
        /// </summary>
        /// <param name="Next">The elements to be scheduled next.</param>
        public sealed record OverrideNext(IReadOnlyList<BpmnSequenceElement> Next) : SequenceNextDecision;

        /// <summary>
        /// Schedules no next element.
        /// </summary>
        public sealed record NoNext : SequenceNextDecision;

        /// <summary>
        /// Creates a decision that keeps the default scheduling behavior.
        /// </summary>
        public static SequenceNextDecision UseDefault() => new Default();

        /// <summary>
        /// Creates a decision that overrides the next element(s) to be scheduled.
        /// </summary>
        /// <param name="next">The element(s) to be scheduled next.</param>
        public static SequenceNextDecision WithNext(params BpmnSequenceElement[] next) => new OverrideNext(next);

        /// <summary>
        /// Creates a decision that schedules no next element.
        /// </summary>
        public static SequenceNextDecision None() => new NoNext();
    }
}
