using System;
using Android.Content;
using Insane.Notifications.CachedStorage;

namespace Insane.Notifications.Droid.CachedStorage
{
    public class DroidDefaultPersistedStorage : DefaultPersistedStorage
    {
		private const string ContainerName = "InsaneNotificationsPushServicesContainer";
		private readonly ISharedPreferences _sharedPreferences;

        public DroidDefaultPersistedStorage(Context androidContext)
        {
			_sharedPreferences = androidContext.GetSharedPreferences(ContainerName, FileCreationMode.Private);
		}

        public override bool Delete(string key)
        {
			_sharedPreferences.Edit()
				.Remove(key)
				.Commit();
            return true;
        }

        public override bool Has(string key)
        {
			var value = _sharedPreferences.GetString(key, string.Empty);

			return !string.IsNullOrEmpty(value);
        }

        protected override string GetJson(string fromKey)
        {
			var jsonValue = _sharedPreferences.GetString(fromKey, string.Empty);
            return jsonValue;
		}

        protected override void SaveOrUpdateJson(string key, string jsonToSave)
        {
			_sharedPreferences.Edit()
                    .PutString(key, jsonToSave)
					.Commit();
        }
    }
}
