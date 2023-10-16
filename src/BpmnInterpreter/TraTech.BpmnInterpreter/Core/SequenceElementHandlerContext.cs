using TraTech.BpmnInterpreter.Abstractions;

namespace TraTech.BpmnInterpreter.Core
{
    public class SequenceElementHandlerContext : ISequenceElementHandlerContext
    {
        private readonly Dictionary<string, object> _dataMap = new();

        public SequenceElementHandlerContext()
        {
        }

        public void SetData(string key, object data)
        {
            _dataMap[key] = data;
        }

        public bool TrySetData(string key, object data)
        {
            return _dataMap.TryAdd(key, data);
        }

        public TData GetData<TData>(string key)
        {
            if (_dataMap.TryGetValue(key, out var data) && data is TData typedData)
            {
                return typedData;
            }
            throw new KeyNotFoundException($"Data with key '{key}' not found.");
        }

        public bool TryGetData<TData>(string key, out TData? data)
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
    }
}
