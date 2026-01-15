namespace TraTech.BpmnInterpreter.Abstractions
{
    /// <summary>
    /// Defines a contract for a data map that stores and retrieves arbitrary values using string keys.
    /// </summary>
    public interface IDataMap
    {
        /// <summary>
        /// Sets the value for the specified key.
        /// </summary>
        /// <param name="key">The key to associate the data with.</param>
        /// <param name="data">The value to store.</param>
        /// <remarks>
        /// If a value already exists for <paramref name="key"/>, it is overwritten.
        /// </remarks>
        void Set(string key, object data);

        /// <summary>
        /// Tries to set the value for the specified key.
        /// </summary>
        /// <param name="key">The key to associate the data with.</param>
        /// <param name="data">The value to store.</param>
        /// <returns>True if the data was successfully set; otherwise, false.</returns>
        /// <remarks>
        /// This operation does not overwrite an existing value. It returns <see langword="false"/> when the key already exists.
        /// </remarks>
        bool TrySet(string key, object data);

        /// <summary>
        /// Retrieves the value associated with the specified key.
        /// </summary>
        /// <typeparam name="TData">The expected type of the stored value.</typeparam>
        /// <param name="key">The key of the value to retrieve.</param>
        /// <returns>The value associated with the key.</returns>
        TData Get<TData>(string key);

        /// <summary>
        /// Tries to retrieve the value associated with the specified key.
        /// </summary>
        /// <typeparam name="TData">The expected type of the stored value.</typeparam>
        /// <param name="key">The key of the value to retrieve.</param>
        /// <param name="data">
        /// When this method returns, contains the value associated with the specified key if the key is found and the value
        /// is compatible with <typeparamref name="TData"/>; otherwise, the default value for the type of the <paramref name="data"/> parameter.
        /// </param>
        /// <returns>True if the data was successfully retrieved; otherwise, false.</returns>
        bool TryGet<TData>(string key, out TData? data);

        /// <summary>
        /// Determines whether the data map contains the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the data map.</param>
        /// <returns>True if the data map contains an element with the specified key; otherwise, false.</returns>
        bool ContainsKey(string key);
    }
}
