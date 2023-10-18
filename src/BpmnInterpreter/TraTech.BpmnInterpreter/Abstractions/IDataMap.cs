namespace TraTech.BpmnInterpreter.Abstractions
{
    public interface IDataMap
    {
        void Set(string key, object data);
        bool TrySet(string key, object data);
        TData Get<TData>(string key);
        bool TryGet<TData>(string key, out TData? data);
        bool ContainsKey(string key);
    }
}
