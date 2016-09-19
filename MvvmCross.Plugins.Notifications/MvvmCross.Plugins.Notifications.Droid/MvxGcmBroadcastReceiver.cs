using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Gcm.Client;
using MvvmCross.Droid.Platform;

namespace MvvmCross.Plugins.Notifications.Droid
{
	public abstract class MvxGcmBroadcastReceiver<TMvxGcmService> : GcmBroadcastReceiverBase<TMvxGcmService> where TMvxGcmService : MvxGcmService
	{
		protected MvxGcmBroadcastReceiver()
		{
		}

		public override void OnReceive(Context context, Intent intent)
		{
			var setup = MvxAndroidSetupSingleton.EnsureSingletonAvailable(context);
			setup.EnsureInitialized();

			base.OnReceive(context, intent);
		}
	}
}