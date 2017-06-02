using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Insane.Notifications.Data;

namespace Insane.Notifications.Droid.Local
{
    public class MvxPeriodicLocalNotificationsService<TPeriodicUpdateAlarmReceiver, TMvxDroidLocalNotificationsService,
            TNotificationData> : INotificationsService
        where TPeriodicUpdateAlarmReceiver : MvxPeriodicUpdateAlarmReceiver<TMvxDroidLocalNotificationsService, TNotificationData>
        where TMvxDroidLocalNotificationsService : MvxDroidLocalNotificationService<TNotificationData>
        where TNotificationData : class
    {
        private readonly Context _context;
        private readonly int _requestCode;
        private readonly TimeSpan _updateAfterTimeSpan;

        public MvxPeriodicLocalNotificationsService(Context context, TimeSpan updateAfterTimeSpan,
            int requestCode = 1005)
        {
            _context = context;
            _updateAfterTimeSpan = updateAfterTimeSpan;
            _requestCode = requestCode;
        }

        public Task<ServiceResponse> SubscribeToNotifications(bool forceSubscribe = false)
        {
            var alarmManager = GetAlarmManager();

            var pendingIntent = GetAlarmPendingIntent();
            alarmManager.SetRepeating(AlarmType.ElapsedRealtimeWakeup,
                (long) _updateAfterTimeSpan.TotalMilliseconds,
                (long) _updateAfterTimeSpan.TotalMilliseconds, pendingIntent);

            return Task.FromResult(ServiceResponse.Build());
        }

        public Task<ServiceResponse> UnsubscribeFromNotifications()
        {
            var alarmManager = GetAlarmManager();
            var pendingIntent = GetAlarmPendingIntent();
            alarmManager.Cancel(pendingIntent);

            return Task.FromResult(ServiceResponse.Build());
        }


        private AlarmManager GetAlarmManager()
        {
            return (AlarmManager) _context.GetSystemService(Context.AlarmService);
        }

        private PendingIntent GetAlarmPendingIntent()
        {
            var alarmIntent = new Intent(_context, typeof(TPeriodicUpdateAlarmReceiver));
            var pendingIntent = PendingIntent.GetBroadcast(_context, _requestCode, alarmIntent,
                PendingIntentFlags.UpdateCurrent);
            return pendingIntent;
        }
    }
}