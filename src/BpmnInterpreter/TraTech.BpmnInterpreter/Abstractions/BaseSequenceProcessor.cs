using TraTech.BpmnInterpreter.Core;

namespace TraTech.BpmnInterpreter.Abstractions
{
    public abstract class BaseSequenceProcessor
    {
        protected readonly BpmnSequenceProcessorData data;
        public abstract ISequenceElementHandlerContext SequenceElementHandlerContext { get; }
        public BaseSequenceProcessor(BpmnSequenceProcessorData bpmnSequenceProcessorBuilderData)
        {
            data = bpmnSequenceProcessorBuilderData;
        }

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

        public static BaseSequenceProcessor Create(Type typeOfProcessor, BpmnSequenceProcessorData bpmnSequenceProcessorBuilderData)
        {
            var instance = Activator.CreateInstance(typeOfProcessor, bpmnSequenceProcessorBuilderData);
            if (instance is BaseSequenceProcessor processorInstance)
            {
                return processorInstance;
            }
            throw new InvalidOperationException($"Failed to create an instance of {typeOfProcessor.Name}");
        }

        public abstract void Start();
        public abstract void Stop();
    }
}
