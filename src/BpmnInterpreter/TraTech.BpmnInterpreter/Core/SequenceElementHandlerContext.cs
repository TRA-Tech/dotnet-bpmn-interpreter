using TraTech.BpmnInterpreter.Abstractions;

namespace TraTech.BpmnInterpreter.Core
{
    /// <summary>
    /// Represents the context for a sequence element handler, providing access to data, the sequence, and the processor.
    /// </summary>
    public class SequenceElementHandlerContext : ISequenceElementHandlerContext
    {
        private readonly IDataMap _dataMap;
        private readonly BaseSequence _sequence;
        private readonly BaseSequenceProcessor _sequenceProcessor;

        /// <summary>
        /// Gets the data map associated with the context.
        /// </summary>
        public IDataMap DataMap => _dataMap;

        /// <summary>
        /// Gets the BPMN sequence associated with the context.
        /// </summary>
        public BaseSequence Sequence => _sequence;

        /// <summary>
        /// Gets the sequence processor associated with the context.
        /// </summary>
        public BaseSequenceProcessor SequenceProcessor => _sequenceProcessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceElementHandlerContext"/> class.
        /// </summary>
        /// <param name="dataMap">The data map.</param>
        /// <param name="sequence">The BPMN sequence.</param>
        /// <param name="sequenceProcessor">The sequence processor.</param>
        public SequenceElementHandlerContext(IDataMap dataMap, BaseSequence sequence, BaseSequenceProcessor sequenceProcessor)
        {
            _dataMap = dataMap;
            _sequence = sequence;
            _sequenceProcessor = sequenceProcessor;
        }
    }
}
