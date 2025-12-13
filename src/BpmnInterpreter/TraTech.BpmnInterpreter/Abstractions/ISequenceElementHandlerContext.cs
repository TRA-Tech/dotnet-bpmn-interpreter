namespace TraTech.BpmnInterpreter.Abstractions
{
    /// <summary>
    /// Defines the context for a sequence element handler, providing access to necessary resources.
    /// </summary>
    public interface ISequenceElementHandlerContext
    {
        /// <summary>
        /// Gets the data map associated with the context.
        /// </summary>
        IDataMap DataMap { get; }

        /// <summary>
        /// Gets the BPMN sequence associated with the context.
        /// </summary>
        BaseSequence Sequence { get; }

        /// <summary>
        /// Gets the sequence processor associated with the context.
        /// </summary>
        BaseSequenceProcessor SequenceProcessor { get; }
    }
}
