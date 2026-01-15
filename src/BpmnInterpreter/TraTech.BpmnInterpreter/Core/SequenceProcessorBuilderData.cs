using TraTech.BpmnInterpreter.Abstractions;

namespace TraTech.BpmnInterpreter.Core
{
    /// <summary>
    /// Holds the configuration used to build a <see cref="BaseSequenceProcessor"/>.
    /// </summary>
    /// <remarks>
    /// This type is typically populated by a sequence processor builder and then passed to a processor constructor.
    /// </remarks>
    public sealed class BpmnSequenceProcessorData
    {
        /// <summary>
        /// The BPMN sequence to be processed.
        /// </summary>
        public BaseSequence? BpmnSequence;

        /// <summary>
        /// Maps BPMN element type names to their corresponding element handlers.
        /// </summary>
        public readonly Dictionary<string, ISequenceElementHandler> ElementHandlerMap = [];

        /// <summary>
        /// Maps BPMN boundary event type names to their corresponding boundary event handlers.
        /// </summary>
        public readonly Dictionary<string, IBoundaryEventHandler> BoundaryElementHandlerMap = [];

        /// <summary>
        /// The default element handler to use when no specific handler is registered for an element type.
        /// </summary>
        public ISequenceElementHandler? DefaultElementHandler;

        /// <summary>
        /// The default boundary event handler to use when no specific boundary handler is registered for a boundary event type.
        /// </summary>
        public IBoundaryEventHandler? DefaultBoundaryElementHandler;

        /// <summary>
        /// The data map used by the sequence processor for execution-scoped values.
        /// </summary>
        public IDataMap? DataMap;
    }
}
