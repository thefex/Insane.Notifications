using Android.App;
using Android.Content;
using Insane.Notifications.Droid.MvxGCM;

[assembly: UsesPermission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]

namespace Insane.Notifications.PushSample.Droid.Services
{
    [BroadcastReceiver(Permission = "com.google.android.c2dm.permission.SEND")]
    [IntentFilter(new string[] { "com.google.android.c2dm.intent.RECEIVE" }, Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { "com.google.android.c2dm.intent.REGISTRATION" }, Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { "com.google.android.gcm.intent.RETRY" }, Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new[] { Intent.ActionBootCompleted }, Categories = new[] { Intent.CategoryDefault })]
    public class AppMvxGcmBroadcastReceiver : MvxGcmBroadcastReceiver<AppMvxInsaneGcmService>
    {


    }

}
