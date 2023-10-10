namespace TraTech.BpmnInterpreter.Abstractions
{
    public interface ISequenceProcessorBuilder
    {
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

        ISequenceProcessorBuilder UsingElementHandler(string elementTypeName, ISequenceElementHandler bpmnSequenceElementHandler);

        ISequenceProcessorBuilder WithBpmnSequence(BaseSequence bpmnSequence);

        TProcessor Build<TProcessor>() where TProcessor : BaseSequenceProcessor;

        BaseSequenceProcessor Build(Type typeOfProcessor);

        ISequenceProcessorBuilder Clone();
    }
}
