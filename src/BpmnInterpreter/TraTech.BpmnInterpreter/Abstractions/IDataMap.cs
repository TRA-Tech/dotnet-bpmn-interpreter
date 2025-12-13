namespace TraTech.BpmnInterpreter.Abstractions
{
    /// <summary>
    /// Defines a contract for a data map that stores and retrieves data using string keys.
    /// </summary>
    public interface IDataMap
    {
        /// <summary>
        /// Sets the data for the specified key.
        /// </summary>
        /// <param name="key">The key to associate the data with.</param>
        /// <param name="data">The data to store.</param>
        void Set(string key, object data);

        /// <summary>
        /// Tries to set the data for the specified key.
        /// </summary>
        /// <param name="key">The key to associate the data with.</param>
        /// <param name="data">The data to store.</param>
        /// <returns>True if the data was successfully set; otherwise, false.</returns>
        bool TrySet(string key, object data);

        /// <summary>
        /// Retrieves the data associated with the specified key.
        /// </summary>
        /// <typeparam name="TData">The type of the data to retrieve.</typeparam>
        /// <param name="key">The key of the data to retrieve.</param>
        /// <returns>The data associated with the key.</returns>
        TData Get<TData>(string key);

        /// <summary>
        /// Tries to retrieve the data associated with the specified key.
        /// </summary>
        /// <typeparam name="TData">The type of the data to retrieve.</typeparam>
        /// <param name="key">The key of the data to retrieve.</param>
        /// <param name="data">When this method returns, contains the data associated with the specified key, if the key is found; otherwise, the default value for the type of the data parameter.</param>
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
