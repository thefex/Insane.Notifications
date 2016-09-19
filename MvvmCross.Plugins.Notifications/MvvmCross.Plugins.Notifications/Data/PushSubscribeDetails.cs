using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmCross.Plugins.Notifications.Data
{
	public class PushSubscribeDetails
	{
		internal PushSubscribeDetails()
		{
			
		}

		public PushPlatformType PushPlatformType { get; set; }

		public IEnumerable<string> TagsToRegisterIn { get; set; }

		public string PushHandle { get; set; }

		public string DeviceId { get; set; }
	}
}
