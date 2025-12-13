using TraTech.BpmnInterpreter.Core;
using TraTech.BpmnInterpreter.Core.SequenceElements;

namespace TraTech.BpmnInterpreter.Abstractions
{
    /// <summary>
    /// Represents the base class for a sequence processor, responsible for executing the BPMN sequence.
    /// </summary>
    public abstract class BaseSequenceProcessor
    {
        /// <summary>
        /// The data associated with the sequence processor.
        /// </summary>
        protected readonly BpmnSequenceProcessorData data;

        /// <summary>
        /// Gets the context for the sequence element handler.
        /// </summary>
        public abstract ISequenceElementHandlerContext SequenceElementHandlerContext { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSequenceProcessor"/> class.
        /// </summary>
        /// <param name="bpmnSequenceProcessorBuilderData">The data used to build the sequence processor.</param>
        public BaseSequenceProcessor(BpmnSequenceProcessorData bpmnSequenceProcessorBuilderData)
        {
            data = bpmnSequenceProcessorBuilderData;
        }

        /// <summary>
        /// Creates a new instance of the specified sequence processor type.
        /// </summary>
        /// <typeparam name="TProcessor">The type of the sequence processor to create.</typeparam>
        /// <param name="bpmnSequenceProcessorBuilderData">The data used to build the sequence processor.</param>
        /// <returns>A new instance of the sequence processor.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the instance creation fails.</exception>
        public static TProcessor Create<TProcessor>(BpmnSequenceProcessorData bpmnSequenceProcessorBuilderData)
            where TProcessor : BaseSequenceProcessor
        {
            var instance = Activator.CreateInstance(typeof(TProcessor), bpmnSequenceProcessorBuilderData);
            if (instance is TProcessor processorInstance)
            {
                return processorInstance;
            }
            throw new InvalidOperationException($"Failed to create an instance of {typeof(TProcessor).Name}");
        }

        /// <summary>
        /// Creates a new instance of the specified sequence processor type.
        /// </summary>
        /// <param name="typeOfProcessor">The type of the sequence processor to create.</param>
        /// <param name="bpmnSequenceProcessorBuilderData">The data used to build the sequence processor.</param>
        /// <returns>A new instance of the sequence processor.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the instance creation fails.</exception>
        public static BaseSequenceProcessor Create(Type typeOfProcessor, BpmnSequenceProcessorData bpmnSequenceProcessorBuilderData)
        {
            var instance = Activator.CreateInstance(typeOfProcessor, bpmnSequenceProcessorBuilderData);
            if (instance is BaseSequenceProcessor processorInstance)
            {
                return processorInstance;
            }
            throw new InvalidOperationException($"Failed to create an instance of {typeOfProcessor.Name}");
        }

        /// <summary>
        /// Starts the execution of the sequence.
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// Sets the next element to be processed in the sequence.
        /// </summary>
        /// <param name="nextElement">The next BPMN sequence element.</param>
        public abstract void SetNextElement(BpmnSequenceElement nextElement);

        /// <summary>
        /// Stops the execution of the sequence.
        /// </summary>
        public abstract void Stop();
    }
}
