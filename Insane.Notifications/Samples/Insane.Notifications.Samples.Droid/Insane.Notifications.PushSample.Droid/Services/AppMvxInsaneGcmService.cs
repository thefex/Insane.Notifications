using Android.App;
using Android.Content;
using Insane.Notifications.Droid.MvxGCM;

[assembly: UsesPermission(Name = "com.insanelab.lhnxa.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.insanelab.lhnxa.permission.C2D_MESSAGE")]

namespace Insane.Notifications.PushSample.Droid.Services
{
    [Service]
    public class AppMvxInsaneGcmService : MvxInsaneGcmService
    {
        protected override string GetPushJsonData(Intent fromIntent)
        {
            return fromIntent.GetStringExtra("message");
        }
    }
}
