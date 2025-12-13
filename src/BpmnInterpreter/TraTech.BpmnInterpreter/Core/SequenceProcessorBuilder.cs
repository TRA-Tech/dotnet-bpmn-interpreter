using TraTech.BpmnInterpreter.Abstractions;

namespace TraTech.BpmnInterpreter.Core
{
    /// <summary>
    /// Builds a sequence processor with the specified configuration.
    /// </summary>
    public class SequenceProcessorBuilder : ISequenceProcessorBuilder
    {
        private readonly BpmnSequenceProcessorData _data = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceProcessorBuilder"/> class.
        /// </summary>
        public SequenceProcessorBuilder()
        {
            _data.DataMap = new DataMap();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceProcessorBuilder"/> class with the specified data.
        /// </summary>
        /// <param name="data">The data to initialize the builder with.</param>
        public SequenceProcessorBuilder(BpmnSequenceProcessorData data)
        {
            _data = data;
        }

        /// <summary>
        /// Builds a sequence processor of the specified type.
        /// </summary>
        /// <typeparam name="TProcessor">The type of the sequence processor to build.</typeparam>
        /// <returns>A new instance of the sequence processor.</returns>
        public TProcessor Build<TProcessor>() where TProcessor : BaseSequenceProcessor
        {
            return BaseSequenceProcessor.Create<TProcessor>(_data);
        }

        /// <summary>
        /// Builds a sequence processor of the specified type.
        /// </summary>
        /// <param name="typeOfProcessor">The type of the sequence processor to build.</param>
        /// <returns>A new instance of the sequence processor.</returns>
        /// <exception cref="ArgumentException">Thrown when the type is not assignable to BaseSequenceProcessor.</exception>
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
        /// <returns>A new instance of the sequence processor builder with the same configuration.</returns>
        public ISequenceProcessorBuilder Clone()
        {
            return new SequenceProcessorBuilder(_data);
        }

        /// <summary>
        /// Registers a handler for a specific BPMN element type.
        /// </summary>
        /// <param name="elementTypeName">The name of the BPMN element type.</param>
        /// <param name="bpmnSequenceElementHandler">The handler to use for the specified element type.</param>
        /// <returns>The current builder instance.</returns>
        public ISequenceProcessorBuilder UsingElementHandler(string elementTypeName, ISequenceElementHandler bpmnSequenceElementHandler)
        {
            _data.ElementHandlerMap.Add(elementTypeName, bpmnSequenceElementHandler);
            return this;
        }

        /// <summary>
        /// Sets the BPMN sequence to be processed.
        /// </summary>
        /// <param name="bpmnSequence">The BPMN sequence.</param>
        /// <returns>The current builder instance.</returns>
        public ISequenceProcessorBuilder WithBpmnSequence(BaseSequence bpmnSequence)
        {
            _data.BpmnSequence = bpmnSequence;
            return this;
        }

        /// <summary>
        /// Sets the data map to be used by the sequence processor.
        /// </summary>
        /// <param name="dataMap">The data map.</param>
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
    }
}
