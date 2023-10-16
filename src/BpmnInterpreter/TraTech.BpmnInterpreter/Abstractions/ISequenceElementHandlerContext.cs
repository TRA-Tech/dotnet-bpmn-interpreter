namespace TraTech.BpmnInterpreter.Abstractions
{
    public interface ISequenceElementHandlerContext
    {
        void SetData(string key, object data);
        bool TrySetData(string key, object data);
        TData GetData<TData>(string key);
        bool TryGetData<TData>(string key, out TData? data);
    }
}
