using TraTech.BpmnInterpreter.Core;

namespace TraTech.BpmnInterpreter.Abstractions
{
    /// <summary>
    /// Represents the base class for a BPMN sequence processor.
    /// Implementations are responsible for coordinating the execution of a loaded BPMN sequence.
    /// </summary>
    public abstract class BaseSequenceProcessor
    {
        /// <summary>
        /// The immutable data used to configure and build a processor instance.
        /// </summary>
        protected readonly BpmnSequenceProcessorData data;

        /// <summary>
        /// Gets the handler context used to resolve and execute element handlers.
        /// </summary>
        public abstract ISequenceElementHandlerContext SequenceElementHandlerContext { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSequenceProcessor"/> class.
        /// </summary>
        /// <param name="bpmnSequenceProcessorData">The data used to build the sequence processor.</param>
        public BaseSequenceProcessor(BpmnSequenceProcessorData bpmnSequenceProcessorData)
        {
            data = bpmnSequenceProcessorData;
        }

        /// <summary>
        /// Creates a new instance of the specified sequence processor type.
        /// The processor type must expose a public constructor with a single <see cref="BpmnSequenceProcessorData"/> parameter.
        /// </summary>
        /// <typeparam name="TProcessor">The type of the sequence processor to create.</typeparam>
        /// <param name="bpmnSequenceProcessorData">The data used to build the sequence processor.</param>
        /// <returns>A new instance of the sequence processor.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="bpmnSequenceProcessorData"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the instance creation fails.</exception>
        public static TProcessor Create<TProcessor>(BpmnSequenceProcessorData bpmnSequenceProcessorData)
            where TProcessor : BaseSequenceProcessor
        {
            ArgumentNullException.ThrowIfNull(bpmnSequenceProcessorData);

            var instance = Activator.CreateInstance(typeof(TProcessor), bpmnSequenceProcessorData);
            if (instance is TProcessor processorInstance)
            {
                return processorInstance;
            }
            throw new InvalidOperationException($"Failed to create an instance of {typeof(TProcessor).Name}");
        }

        /// <summary>
        /// Creates a new instance of the specified sequence processor type.
        /// The processor type must expose a public constructor with a single <see cref="BpmnSequenceProcessorData"/> parameter.
        /// </summary>
        /// <param name="typeOfProcessor">The type of the sequence processor to create.</param>
        /// <param name="bpmnSequenceProcessorData">The data used to build the sequence processor.</param>
        /// <returns>A new instance of the sequence processor.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="typeOfProcessor"/> or <paramref name="bpmnSequenceProcessorData"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the instance creation fails.</exception>
        public static BaseSequenceProcessor Create(Type typeOfProcessor, BpmnSequenceProcessorData bpmnSequenceProcessorData)
        {
            ArgumentNullException.ThrowIfNull(typeOfProcessor);
            ArgumentNullException.ThrowIfNull(bpmnSequenceProcessorData);

            var instance = Activator.CreateInstance(typeOfProcessor, bpmnSequenceProcessorData);
            if (instance is BaseSequenceProcessor processorInstance)
            {
                return processorInstance;
            }
            throw new InvalidOperationException($"Failed to create an instance of {typeOfProcessor.Name}");
        }

        /// <summary>
        /// Starts the execution of the sequence asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A token used to cancel the execution.</param>
        public abstract Task StartAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Resets any mutable run state so the processor can be executed again.
        /// Implementations should clear any in-memory execution state, without modifying the configured <see cref="data"/>.
        /// </summary>
        protected abstract void ResetRunState();

        /// <summary>
        /// Stops the execution of the sequence.
        /// </summary>
        public abstract void Stop();
    }
}
