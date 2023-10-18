namespace TraTech.BpmnInterpreter.Abstractions
{
    public interface ISequenceElementHandlerContext
    {
        IDataMap DataMap { get; }
        BaseSequence Sequence { get; }
        BaseSequenceProcessor SequenceProcessor { get; }
    }
}
