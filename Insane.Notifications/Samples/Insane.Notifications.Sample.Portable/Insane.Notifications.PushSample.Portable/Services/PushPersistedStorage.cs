using System;
using Insane.Notifications.CachedStorage;

namespace Insane.Notifications.PushSample.Portable.Services
{
    public class PushPersistedStorage : IPersistedStorage
    {
        public PushPersistedStorage()
        {
        }

        public bool Delete(string key)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string fromKey) where T : class
        {
            throw new NotImplementedException();
        }

        public bool Has(string key)
        {
            throw new NotImplementedException();
        }

        public void SaveOrUpdate<T>(string key, T serializableObject) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
