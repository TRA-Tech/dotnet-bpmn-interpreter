namespace TraTech.BpmnInterpreter.Abstractions
{
    /// <summary>
    /// Defines a contract for building a sequence processor.
    /// </summary>
    public interface ISequenceProcessorBuilder
    {
        /// <summary>
        /// Creates a new instance of the specified sequence processor builder type.
        /// </summary>
        /// <typeparam name="TBuilder">The type of the sequence processor builder to create.</typeparam>
        /// <returns>A new instance of the sequence processor builder.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the instance creation fails.</exception>
        static ISequenceProcessorBuilder Create<TBuilder>()
            where TBuilder : class, ISequenceProcessorBuilder
        {
            var instance = Activator.CreateInstance(typeof(TBuilder));
            if (instance is TBuilder builderInstance)
            {
                return builderInstance;
            }
            throw new InvalidOperationException($"Failed to create an instance of {typeof(TBuilder).Name}");
        }

        /// <summary>
        /// Registers a handler for a specific BPMN element type.
        /// </summary>
        /// <param name="elementTypeName">The name of the BPMN element type.</param>
        /// <param name="bpmnSequenceElementHandler">The handler to use for the specified element type.</param>
        /// <returns>The current builder instance.</returns>
        ISequenceProcessorBuilder UsingElementHandler(string elementTypeName, ISequenceElementHandler bpmnSequenceElementHandler);

        /// <summary>
        /// Sets the default element handler to be used when no specific handler is registered for an element type.
        /// </summary>
        /// <param name="bpmnSequenceElementHandler">The default element handler.</param>
        /// <returns>The current builder instance.</returns>
        ISequenceProcessorBuilder WithDefaultElementHandler(ISequenceElementHandler bpmnSequenceElementHandler);

        /// <summary>
        /// Sets the BPMN sequence to be processed.
        /// </summary>
        /// <param name="bpmnSequence">The BPMN sequence.</param>
        /// <returns>The current builder instance.</returns>
        ISequenceProcessorBuilder WithBpmnSequence(BaseSequence bpmnSequence);

        /// <summary>
        /// Sets the data map to be used by the sequence processor.
        /// </summary>
        /// <param name="dataMap">The data map.</param>
        /// <returns>The current builder instance.</returns>
        ISequenceProcessorBuilder WithDataMap(IDataMap dataMap);

        /// <summary>
        /// Builds a sequence processor of the specified type.
        /// </summary>
        /// <typeparam name="TProcessor">The type of the sequence processor to build.</typeparam>
        /// <returns>A new instance of the sequence processor.</returns>
        TProcessor Build<TProcessor>() where TProcessor : BaseSequenceProcessor;

        /// <summary>
        /// Builds a sequence processor of the specified type.
        /// </summary>
        /// <param name="typeOfProcessor">The type of the sequence processor to build.</param>
        /// <returns>A new instance of the sequence processor.</returns>
        BaseSequenceProcessor Build(Type typeOfProcessor);

        /// <summary>
        /// Creates a clone of the current sequence processor builder.
        /// </summary>
        /// <returns>A new instance of the sequence processor builder with the same configuration.</returns>
        ISequenceProcessorBuilder Clone();
    }
}
