using TraTech.BpmnInterpreter.Core.SequenceElements;

namespace TraTech.BpmnInterpreter.Abstractions
{
    /// <summary>
    /// Defines a contract for handling non-interrupting BPMN boundary events.
    /// </summary>
    /// <remarks>
    /// Boundary event handlers are executed for side effects only and do not influence routing.
    /// Implementations should not modify sequence flow selection.
    /// </remarks>
    public interface IBoundaryEventHandler
    {
        /// <summary>
        /// Processes the specified non-interrupting boundary event asynchronously.
        /// </summary>
        /// <param name="boundaryElement">The boundary event element to process.</param>
        /// <param name="context">The handler context used to access runtime services and shared state.</param>
        /// <param name="cancellationToken">A token used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous processing operation.</returns>
        System.Threading.Tasks.Task ProcessAsync(
            BoundaryEvent boundaryElement,
            ISequenceElementHandlerContext context,
            CancellationToken cancellationToken = default
        );
    }
}
