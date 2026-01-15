namespace TraTech.BpmnInterpreter.Abstractions
{
    /// <summary>
    /// Defines the context passed to sequence element handlers.
    /// Provides access to shared data and processor/sequence services for the current execution.
    /// </summary>
    public interface ISequenceElementHandlerContext
    {
        /// <summary>
        /// Gets the data map for storing and retrieving execution-scoped values.
        /// </summary>
        IDataMap DataMap { get; }

        /// <summary>
        /// Gets the BPMN sequence being executed.
        /// </summary>
        BaseSequence Sequence { get; }

        /// <summary>
        /// Gets the sequence processor coordinating the execution.
        /// </summary>
        BaseSequenceProcessor SequenceProcessor { get; }
    }
}