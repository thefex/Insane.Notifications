using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmCross.Plugins.Notifications.CachedStorage
{
	public interface IPersistedStorage
	{
		T Get<T>(string fromKey) where T : class;
		bool Has(string key);
		void SaveOrUpdate<T>(string key, T serializableObject) where T : class;
		bool Delete(string key);
	}
}
