using TraTech.BpmnInterpreter.Abstractions;

namespace TraTech.BpmnInterpreter.Core
{
    public class SequenceProcessorBuilder : ISequenceProcessorBuilder
    {
        private readonly BpmnSequenceProcessorData _data = new();

        public SequenceProcessorBuilder() { }

        public SequenceProcessorBuilder(BpmnSequenceProcessorData data)
        {
            _data = data;
        }

        public TProcessor Build<TProcessor>() where TProcessor : BaseSequenceProcessor
        {
            return BaseSequenceProcessor.Create<TProcessor>(_data);
        }

        public BaseSequenceProcessor Build(Type typeOfProcessor)
        {
            if (!typeOfProcessor.IsAssignableTo(typeof(BaseSequenceProcessor)))
            {
                throw new ArgumentException($"Type {typeOfProcessor.Name} is not assignable to {nameof(BaseSequenceProcessor)}.", nameof(typeOfProcessor));
            }
            return BaseSequenceProcessor.Create(typeOfProcessor, _data);
        }

        public ISequenceProcessorBuilder Clone()
        {
            return new SequenceProcessorBuilder(_data);
        }

        public ISequenceProcessorBuilder UsingElementHandler(string elementTypeName, ISequenceElementHandler bpmnSequenceElementHandler)
        {
            _data.ElementHandlerMap.Add(elementTypeName, bpmnSequenceElementHandler);
            return this;
        }

        public ISequenceProcessorBuilder WithBpmnSequence(BaseSequence bpmnSequence)
        {
            _data.BpmnSequence = bpmnSequence;
            return this;
        }
    }
}
