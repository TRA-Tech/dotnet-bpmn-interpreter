using TraTech.BpmnInterpreter.Abstractions;

namespace TraTech.BpmnInterpreter.Core
{
    /// <summary>
    /// Builds a <see cref="BaseSequenceProcessor"/> instance from the configured sequence, data map, and handler registrations.
    /// </summary>
    public class SequenceProcessorBuilder : ISequenceProcessorBuilder
    {
        private readonly BpmnSequenceProcessorData _data = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceProcessorBuilder"/> class.
        /// </summary>
        /// <remarks>
        /// A default <see cref="IDataMap"/> implementation is created.
        /// </remarks>
        public SequenceProcessorBuilder()
        {
            _data.DataMap = new DataMap();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceProcessorBuilder"/> class with the specified data.
        /// </summary>
        /// <param name="data">The processor configuration to initialize the builder with.</param>
        public SequenceProcessorBuilder(BpmnSequenceProcessorData data)
        {
            _data = data;
        }

        /// <summary>
        /// Builds a sequence processor of the specified type.
        /// </summary>
        /// <typeparam name="TProcessor">The type of the sequence processor to build.</typeparam>
        /// <returns>A new instance of the sequence processor.</returns>
        /// <remarks>
        /// The processor type must expose a public constructor with a single <see cref="BpmnSequenceProcessorData"/> parameter.
        /// </remarks>
        public TProcessor Build<TProcessor>() where TProcessor : BaseSequenceProcessor
        {
            return BaseSequenceProcessor.Create<TProcessor>(_data);
        }

        /// <summary>
        /// Builds a sequence processor of the specified type.
        /// </summary>
        /// <param name="typeOfProcessor">The type of the sequence processor to build.</param>
        /// <returns>A new instance of the sequence processor.</returns>
        /// <remarks>
        /// The processor type must be assignable to <see cref="BaseSequenceProcessor"/> and expose a public constructor with a single
        /// <see cref="BpmnSequenceProcessorData"/> parameter.
        /// </remarks>
        public BaseSequenceProcessor Build(Type typeOfProcessor)
        {
            if (!typeOfProcessor.IsAssignableTo(typeof(BaseSequenceProcessor)))
            {
                throw new ArgumentException($"Type {typeOfProcessor.Name} is not assignable to {nameof(BaseSequenceProcessor)}.", nameof(typeOfProcessor));
            }
            return BaseSequenceProcessor.Create(typeOfProcessor, _data);
        }

        /// <summary>
        /// Creates a clone of the current sequence processor builder.
        /// </summary>
        /// <returns>A new builder instance with the same configuration.</returns>
        /// <remarks>
        /// The clone shares the same underlying <see cref="BpmnSequenceProcessorData"/> instance.
        /// </remarks>
        public ISequenceProcessorBuilder Clone()
        {
            return new SequenceProcessorBuilder(_data);
        }

        /// <summary>
        /// Registers a handler for a specific BPMN element type.
        /// </summary>
        /// <param name="elementTypeName">The name of the BPMN element type.</param>
        /// <param name="bpmnSequenceElementHandler">The handler to use for elements of the specified type.</param>
        /// <returns>The current builder instance.</returns>
        public ISequenceProcessorBuilder UsingElementHandler(string elementTypeName, ISequenceElementHandler bpmnSequenceElementHandler)
        {
            _data.ElementHandlerMap.Add(elementTypeName, bpmnSequenceElementHandler);
            return this;
        }

        /// <summary>
        /// Registers a handler for a specific BPMN boundary element type.
        /// </summary>
        /// <param name="elementTypeName">The name of the BPMN boundary element type.</param>
        /// <param name="bpmnBoundaryElementHandler">The handler to use for boundary events of the specified type.</param>
        /// <returns>The current builder instance.</returns>
        public ISequenceProcessorBuilder UsingBoundaryElementHandler(string elementTypeName, IBoundaryEventHandler bpmnBoundaryElementHandler)
        {
            _data.BoundaryElementHandlerMap.Add(elementTypeName, bpmnBoundaryElementHandler);
            return this;
        }

        /// <summary>
        /// Sets the BPMN sequence to be processed.
        /// </summary>
        /// <param name="bpmnSequence">The BPMN sequence to be executed by the processor.</param>
        /// <returns>The current builder instance.</returns>
        public ISequenceProcessorBuilder WithBpmnSequence(BaseSequence bpmnSequence)
        {
            _data.BpmnSequence = bpmnSequence;
            return this;
        }

        /// <summary>
        /// Sets the data map to be used by the sequence processor.
        /// </summary>
        /// <param name="dataMap">The data map used for execution-scoped values.</param>
        /// <returns>The current builder instance.</returns>
        public ISequenceProcessorBuilder WithDataMap(IDataMap dataMap)
        {
            _data.DataMap = dataMap;
            return this;
        }

        /// <summary>
        /// Sets the default element handler to be used when no specific handler is registered for an element type.
        /// </summary>
        /// <param name="bpmnSequenceElementHandler">The default element handler.</param>
        /// <returns>The current builder instance.</returns>
        public ISequenceProcessorBuilder WithDefaultElementHandler(ISequenceElementHandler bpmnSequenceElementHandler)
        {
            _data.DefaultElementHandler = bpmnSequenceElementHandler;
            return this;
        }

        /// <summary>
        /// Sets the default boundary element handler to be used when no specific boundary handler is registered for a boundary event type.
        /// </summary>
        /// <param name="bpmnBoundaryElementHandler">The default boundary element handler.</param>
        /// <returns>The current builder instance.</returns>
        public ISequenceProcessorBuilder WithDefaultBoundaryElementHandler(IBoundaryEventHandler bpmnBoundaryElementHandler)
        {
            _data.DefaultBoundaryElementHandler = bpmnBoundaryElementHandler;
            return this;
        }
    }
}
