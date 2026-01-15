using TraTech.BpmnInterpreter.Core.SequenceElements;

namespace TraTech.BpmnInterpreter.Abstractions
{
    /// <summary>
    /// Defines a contract for handling the processing of a single BPMN sequence element.
    /// </summary>
    public interface ISequenceElementHandler
    {
        /// <summary>
        /// Processes the specified BPMN sequence element asynchronously.
        /// </summary>
        /// <param name="currentElement">The BPMN sequence element to process.</param>
        /// <param name="context">The handler context providing access to runtime services and shared state.</param>
        /// <param name="cancellationToken">A token used to cancel the operation.</param>
        /// <returns>
        /// A <see cref="SequenceNextDecision"/> that instructs the processor which element(s) should be scheduled next.
        /// Return <see cref="SequenceNextDecision.UseDefault"/> to keep the default routing behavior.
        /// </returns>
        Task<SequenceNextDecision> ProcessAsync(
            BpmnSequenceElement currentElement,
            ISequenceElementHandlerContext context,
            CancellationToken cancellationToken = default
        );
    }
}
