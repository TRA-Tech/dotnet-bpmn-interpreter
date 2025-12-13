namespace TraTech.BpmnInterpreter.Enums
{
    /// <summary>
    /// Represents the state of a BPMN element during processing.
    /// </summary>
    public enum ProcessorElementState
    {
        /// <summary>
        /// The element is ready to be processed.
        /// </summary>
        Ready,

        /// <summary>
        /// The element is waiting for its dependencies or conditions to be met.
        /// </summary>
        Waiting,

        /// <summary>
        /// The element has been processed.
        /// </summary>
        Processed,

        /// <summary>
        /// The element should not be processed.
        /// </summary>
        DontProcess,
    }
}
