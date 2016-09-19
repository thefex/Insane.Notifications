namespace MvvmCross.Plugins.Notifications.Droid.GCM.GcmClient
{
    public class Logger
    {
        public static bool Enabled = false;

        public static void Debug(string msg)
        {
            if (Enabled)
                Android.Util.Log.Debug("GCM-CLIENT", msg);
        }
    }
}