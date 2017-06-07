using System;
using Foundation;
using Insane.Notifications.CachedStorage;

namespace Insane.Notifications.iOS.CachedStorage
{
    public class iOSDefaultPersistedStorage : DefaultPersistedStorage
    {
        public override bool Has(string key)
        {
            return !string.IsNullOrEmpty(NSUserDefaults.StandardUserDefaults.StringForKey(key));
        }

        protected override void SaveOrUpdateJson(string key, string jsonToSave)
        {
            NSUserDefaults.StandardUserDefaults.SetString(jsonToSave, key);
            NSUserDefaults.StandardUserDefaults.Synchronize();
        }

        protected override string GetJson(string fromKey)
        {
            return NSUserDefaults.StandardUserDefaults.StringForKey(fromKey);        
        }

        public override bool Delete(string key)
        {
            NSUserDefaults.StandardUserDefaults.RemoveObject(key);
            NSUserDefaults.StandardUserDefaults.Synchronize();
            return true;
        }

    }
}
