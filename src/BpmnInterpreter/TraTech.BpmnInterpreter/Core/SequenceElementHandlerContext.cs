using TraTech.BpmnInterpreter.Abstractions;

namespace TraTech.BpmnInterpreter.Core
{
    /// <summary>
    /// Represents the context passed to sequence element handlers.
    /// Provides access to shared data and processor/sequence services for the current execution.
    /// </summary>
    public class SequenceElementHandlerContext : ISequenceElementHandlerContext
    {
        private readonly IDataMap _dataMap;
        private readonly BaseSequence _sequence;
        private readonly BaseSequenceProcessor _sequenceProcessor;

        /// <summary>
        /// Gets the data map for storing and retrieving execution-scoped values.
        /// </summary>
        public IDataMap DataMap => _dataMap;

        /// <summary>
        /// Gets the BPMN sequence being executed.
        /// </summary>
        public BaseSequence Sequence => _sequence;

        /// <summary>
        /// Gets the sequence processor coordinating the execution.
        /// </summary>
        public BaseSequenceProcessor SequenceProcessor => _sequenceProcessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceElementHandlerContext"/> class.
        /// </summary>
        /// <param name="dataMap">The data map used for execution-scoped values.</param>
        /// <param name="sequence">The BPMN sequence being executed.</param>
        /// <param name="sequenceProcessor">The processor coordinating the execution.</param>
        public SequenceElementHandlerContext(IDataMap dataMap, BaseSequence sequence, BaseSequenceProcessor sequenceProcessor)
        {
            _dataMap = dataMap;
            _sequence = sequence;
            _sequenceProcessor = sequenceProcessor;
        }
    }
}