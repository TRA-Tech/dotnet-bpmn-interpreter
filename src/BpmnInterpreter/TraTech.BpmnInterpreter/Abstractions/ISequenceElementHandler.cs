using TraTech.BpmnInterpreter.Core.SequenceElements;

namespace TraTech.BpmnInterpreter.Abstractions
{
    /// <summary>
    /// Defines a contract for handling the processing of a BPMN sequence element.
    /// </summary>
    public interface ISequenceElementHandler
    {
        /// <summary>
        /// Processes the specified BPMN sequence element.
        /// </summary>
        /// <param name="currentElement">The BPMN sequence element to process.</param>
        /// <param name="context">The context for the sequence element handler, providing access to data and the sequence processor.</param>
        void Process(
            BpmnSequenceElement currentElement,
            ISequenceElementHandlerContext context
        );
    }
}
