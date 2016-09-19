using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotificationsSample.Portable.Data;

namespace NotificationsSample.Portable.Services
{
    public class LocalNotificationsProvider
    {
        private static int testNotificationNumber = 1;
        public async Task<IEnumerable<LocalNotificationData>> GetLocalNotifications()
        {
            await Task.Delay(1000);

            List<LocalNotificationData> mockedNotifications = new List<LocalNotificationData>();
            mockedNotifications.Add(new LocalNotificationData()
            {
                Title = "This is test notification number " + testNotificationNumber++
            });
            mockedNotifications.Add(new LocalNotificationData()
            {
                Title = "This is test notification number " + testNotificationNumber++
            });
            return mockedNotifications;
        }
    }
}
