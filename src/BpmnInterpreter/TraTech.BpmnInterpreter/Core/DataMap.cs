using TraTech.BpmnInterpreter.Abstractions;

namespace TraTech.BpmnInterpreter.Core
{
    /// <summary>
    /// Represents a data map that stores and retrieves data using string keys.
    /// </summary>
    public class DataMap : IDataMap
    {
        private readonly Dictionary<string, object> _dataMap = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="DataMap"/> class.
        /// </summary>
        public DataMap()
        {
        }

        /// <summary>
        /// Sets the data for the specified key.
        /// </summary>
        /// <param name="key">The key to associate the data with.</param>
        /// <param name="data">The data to store.</param>
        public void Set(string key, object data)
        {
            _dataMap[key] = data;
        }

        /// <summary>
        /// Tries to set the data for the specified key.
        /// </summary>
        /// <param name="key">The key to associate the data with.</param>
        /// <param name="data">The data to store.</param>
        /// <returns>True if the data was successfully set; otherwise, false.</returns>
        public bool TrySet(string key, object data)
        {
            return _dataMap.TryAdd(key, data);
        }

        /// <summary>
        /// Retrieves the data associated with the specified key.
        /// </summary>
        /// <typeparam name="TData">The type of the data to retrieve.</typeparam>
        /// <param name="key">The key of the data to retrieve.</param>
        /// <returns>The data associated with the key.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the key is not found or the data is not of the expected type.</exception>
        public TData Get<TData>(string key)
        {
            if (_dataMap.TryGetValue(key, out var data) && data is TData typedData)
            {
                return typedData;
            }
            throw new KeyNotFoundException($"Data with key '{key}' not found.");
        }

        /// <summary>
        /// Tries to retrieve the data associated with the specified key.
        /// </summary>
        /// <typeparam name="TData">The type of the data to retrieve.</typeparam>
        /// <param name="key">The key of the data to retrieve.</param>
        /// <param name="data">When this method returns, contains the data associated with the specified key, if the key is found; otherwise, the default value for the type of the data parameter.</param>
        /// <returns>True if the data was successfully retrieved; otherwise, false.</returns>
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

        /// <summary>
        /// Determines whether the data map contains the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the data map.</param>
        /// <returns>True if the data map contains an element with the specified key; otherwise, false.</returns>
        public bool ContainsKey(string key)
        {
            return _dataMap.ContainsKey(key);
        }
    }
}
