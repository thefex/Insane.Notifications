using Windows.Storage;
using Insane.Notifications.CachedStorage;

namespace Insane.Notifications.UWP.CachedStorage
{
    public class UWPDefaultPersistedStorage : DefaultPersistedStorage
    {
        private readonly ApplicationDataContainer appDataContainer;

        public UWPDefaultPersistedStorage()
        {
            appDataContainer = ApplicationData.Current.LocalSettings;
        }

        protected override string GetJson(string fromKey)
        {
            return appDataContainer.Values[fromKey] as string ?? string.Empty;
        }

        public override bool Has(string key)
        {
            return appDataContainer.Values.ContainsKey(key) &&
                   !string.IsNullOrEmpty(appDataContainer.Values[key] as string);
        }

        protected override void SaveOrUpdateJson(string key, string jsonToSave)
        {
            if (!appDataContainer.Values.ContainsKey(key))
                appDataContainer.Values.Add(key, jsonToSave);
            else
                appDataContainer.Values[key] = jsonToSave;
        }

        public override bool Delete(string key)
        {
            return appDataContainer.Values.Remove(key);
        }
    }
}