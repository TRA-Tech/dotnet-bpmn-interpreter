using TraTech.BpmnInterpreter.Abstractions;

namespace TraTech.BpmnInterpreter.Core
{
    /// <summary>
    /// Represents an <see cref="IDataMap"/> implementation backed by an in-memory dictionary.
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
        /// Sets the value for the specified key.
        /// </summary>
        /// <param name="key">The key to associate the data with.</param>
        /// <param name="data">The value to store.</param>
        /// <remarks>
        /// If a value already exists for <paramref name="key"/>, it is overwritten.
        /// </remarks>
        public void Set(string key, object data)
        {
            _dataMap[key] = data;
        }

        /// <summary>
        /// Tries to set the value for the specified key.
        /// </summary>
        /// <param name="key">The key to associate the data with.</param>
        /// <param name="data">The value to store.</param>
        /// <returns><see langword="true"/> if the value was added; otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// This operation does not overwrite an existing value. It returns <see langword="false"/> when the key already exists.
        /// </remarks>
        public bool TrySet(string key, object data)
        {
            return _dataMap.TryAdd(key, data);
        }

        /// <summary>
        /// Retrieves the value associated with the specified key.
        /// </summary>
        /// <typeparam name="TData">The expected type of the stored value.</typeparam>
        /// <param name="key">The key of the value to retrieve.</param>
        /// <returns>The value associated with the key.</returns>
        /// <exception cref="KeyNotFoundException">
        /// Thrown when no value is associated with <paramref name="key"/>, or when the stored value is not of type
        /// <typeparamref name="TData"/>.
        /// </exception>
        public TData Get<TData>(string key)
        {
            if (_dataMap.TryGetValue(key, out var data) && data is TData typedData)
            {
                return typedData;
            }
            throw new KeyNotFoundException($"Data with key '{key}' not found.");
        }

        /// <summary>
        /// Tries to retrieve the value associated with the specified key.
        /// </summary>
        /// <typeparam name="TData">The expected type of the stored value.</typeparam>
        /// <param name="key">The key of the value to retrieve.</param>
        /// <param name="data">
        /// When this method returns, contains the value associated with the specified key if the key is found and the value
        /// is compatible with <typeparamref name="TData"/>; otherwise, the default value for the type of the <paramref name="data"/> parameter.
        /// </param>
        /// <returns><see langword="true"/> if a compatible value was found; otherwise, <see langword="false"/>.</returns>
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
