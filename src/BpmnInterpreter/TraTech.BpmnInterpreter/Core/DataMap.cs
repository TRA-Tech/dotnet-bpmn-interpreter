using TraTech.BpmnInterpreter.Abstractions;

namespace TraTech.BpmnInterpreter.Core
{
    public class DataMap : IDataMap
    {
        private readonly Dictionary<string, object> _dataMap = new();

        public DataMap()
        {
        }

        public void Set(string key, object data)
        {
            _dataMap[key] = data;
        }

        public bool TrySet(string key, object data)
        {
            return _dataMap.TryAdd(key, data);
        }

        public TData Get<TData>(string key)
        {
            if (_dataMap.TryGetValue(key, out var data) && data is TData typedData)
            {
                return typedData;
            }
            throw new KeyNotFoundException($"Data with key '{key}' not found.");
        }

        public bool TryGet<TData>(string key, out TData? data)
        {
            var result = _dataMap.TryGetValue(key, out var resultData);

            if (result)
            {
                if (resultData is TData typedData)
                {
                    data = typedData;
                    return true;
                }
            }

            data = default;
            return false;
        }

        public bool ContainsKey(string key)
        {
            return _dataMap.ContainsKey(key);
        }
    }
}
