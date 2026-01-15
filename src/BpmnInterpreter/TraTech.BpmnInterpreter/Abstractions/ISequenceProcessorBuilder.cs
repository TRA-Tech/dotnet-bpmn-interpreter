namespace TraTech.BpmnInterpreter.Abstractions
{
    /// <summary>
    /// Defines a contract for configuring and building a <see cref="BaseSequenceProcessor"/>.
    /// </summary>
    public interface ISequenceProcessorBuilder
    {
        /// <summary>
        /// Creates a new instance of the specified sequence processor builder type.
        /// </summary>
        /// <typeparam name="TBuilder">The type of the sequence processor builder to create.</typeparam>
        /// <returns>A new builder instance.</returns>
        /// <remarks>
        /// The builder type must expose a public parameterless constructor.
        /// </remarks>
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
        /// <param name="bpmnSequenceElementHandler">The handler to use for elements of the specified type.</param>
        /// <returns>The current builder instance.</returns>
        ISequenceProcessorBuilder UsingElementHandler(string elementTypeName, ISequenceElementHandler bpmnSequenceElementHandler);

        /// <summary>
        /// Registers a handler for a specific BPMN boundary element type.
        /// </summary>
        /// <param name="elementTypeName">The name of the BPMN boundary element type.</param>
        /// <param name="bpmnBoundaryElementHandler">The handler to use for boundary events of the specified type.</param>
        /// <returns>The current builder instance.</returns>
        ISequenceProcessorBuilder UsingBoundaryElementHandler(string elementTypeName, IBoundaryEventHandler bpmnBoundaryElementHandler);

        /// <summary>
        /// Sets the default element handler to be used when no specific handler is registered for an element type.
        /// </summary>
        /// <param name="bpmnSequenceElementHandler">The default element handler.</param>
        /// <returns>The current builder instance.</returns>
        ISequenceProcessorBuilder WithDefaultElementHandler(ISequenceElementHandler bpmnSequenceElementHandler);

        /// <summary>
        /// Sets the default boundary handler to be used when no specific boundary handler is registered for a boundary element type.
        /// </summary>
        /// <param name="bpmnSequenceElementHandler">The default boundary element handler.</param>
        /// <returns>The current builder instance.</returns>
        ISequenceProcessorBuilder WithDefaultBoundaryElementHandler(IBoundaryEventHandler bpmnSequenceElementHandler);

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
        /// <remarks>
        /// Implementations typically use the configured sequence, data map, and registered handlers to construct a processor.
        /// </remarks>
        TProcessor Build<TProcessor>() where TProcessor : BaseSequenceProcessor;

        /// <summary>
        /// Builds a sequence processor of the specified type.
        /// </summary>
        /// <param name="typeOfProcessor">The type of the sequence processor to build.</param>
        /// <returns>A new instance of the sequence processor.</returns>
        /// <remarks>
        /// Implementations typically use the configured sequence, data map, and registered handlers to construct a processor.
        /// </remarks>
        BaseSequenceProcessor Build(Type typeOfProcessor);

        /// <summary>
        /// Creates a clone of the current sequence processor builder.
        /// </summary>
        /// <returns>A new builder instance with the same configuration.</returns>
        ISequenceProcessorBuilder Clone();
    }
}
