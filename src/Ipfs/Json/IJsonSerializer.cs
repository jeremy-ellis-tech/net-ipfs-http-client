namespace Ipfs.Json
{
    public interface IJsonSerializer
    {
        /// <summary>
        /// Converts an object into its Json string representation
        /// </summary>
        /// <param name="obj">The object to serailize</param>
        /// <returns>The json representation of the object</returns>
        string Serialize(object obj);

        /// <summary>
        /// Converts json into a known .NET type
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="json">The json representation of the object, of type T</param>
        /// <returns>The json object as a .NET type</returns>
        T Deserialize<T>(string json);
    }
}
