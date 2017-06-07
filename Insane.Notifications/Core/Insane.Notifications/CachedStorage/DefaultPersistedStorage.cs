using System;
using Newtonsoft.Json;

namespace Insane.Notifications.CachedStorage
{
    public abstract class DefaultPersistedStorage : IPersistedStorage
    {
        public DefaultPersistedStorage()
        {
        }

		protected string SerializeToJson(object item)
		{
			return JsonConvert.SerializeObject(item);
		}

		protected T DeserializeJson<T>(string json)
		{
			if (string.IsNullOrWhiteSpace(json))
			{
				return default(T);
			}
			try
			{
				var deserialized = JsonConvert.DeserializeObject<T>(json);
				return deserialized;
			}
			catch (JsonSerializationException e)
			{
				System.Diagnostics.Debug.WriteLine($"Failed to deserialize object {json}, message {e.Message}");
				return default(T);
			}

		}

        public T Get<T>(string fromKey) where T : class
        {
            var serializedObjectJson = GetJson(fromKey);
            return DeserializeJson<T>(serializedObjectJson);
        }

        protected abstract string GetJson(string fromKey);

        public abstract bool Has(string key);

        public void SaveOrUpdate<T>(string key, T serializableObject) where T : class
        {
            var serializedObject = SerializeToJson(serializableObject);
            SaveOrUpdateJson(key, serializedObject);
        }

        protected abstract void SaveOrUpdateJson(string key, string jsonToSave);


        public abstract bool Delete(string key);
    }
}
