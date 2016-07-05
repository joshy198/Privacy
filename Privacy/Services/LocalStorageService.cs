using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Privacy.Services
{
    public class LocalStorageService:IStorageService
    {
        #region variables
        private readonly ApplicationDataContainer appData = ApplicationData.Current.LocalSettings;
        #endregion

        /// <summary>
        /// Writes the given Data as a Json Obect to the LocalSettings Storage
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Name of the given Object</param>
        /// <param name="value">Object itself</param>
        public void Write<T>(string key, T value)
        {
            var json = JsonConvert.SerializeObject(value);
            appData.Values[key] = json;
        }

        /// <summary>
        /// reads the Data with the given key and returns it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Read<T>(string key)
        {
            return Read<T>(key, default(T));
        }

        /// <summary>
        /// reads the Data and deserialize it from a json object to the previous object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Key of the object</param>
        /// <param name="defaultValue">return value, if object not found</param>
        /// <returns></returns>
        public T Read<T>(string key, T defaultValue)
        {
            if (!appData.Values.ContainsKey(key))
            {
                return defaultValue;
            }
            var json = (string)appData.Values[key];
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
