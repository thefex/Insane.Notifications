using System;
using System.Collections.Generic;

namespace Insane.Notifications.PushSample.Portable.Data.Push
{
    public class ModifyPushTagsRequest
	{
		public IEnumerable<string> Tags { get; set; }
	}
}
