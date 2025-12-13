using TraTech.BpmnInterpreter.Abstractions;

namespace TraTech.BpmnInterpreter.Core
{
    /// <summary>
    /// Contains the data required to build a sequence processor.
    /// </summary>
    public sealed class BpmnSequenceProcessorData
    {
        /// <summary>
        /// The BPMN sequence to be processed.
        /// </summary>
        public BaseSequence? BpmnSequence;

        /// <summary>
        /// A dictionary mapping element types to their corresponding handlers.
        /// </summary>
        public readonly Dictionary<string, ISequenceElementHandler> ElementHandlerMap = new();

        /// <summary>
        /// The default element handler to use when no specific handler is found.
        /// </summary>
        public ISequenceElementHandler? DefaultElementHandler;

        /// <summary>
        /// The data map used by the sequence processor.
        /// </summary>
        public IDataMap? DataMap;
    }
}
