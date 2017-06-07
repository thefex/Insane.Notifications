using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Insane.Notifications.Presenter;
using Insane.Notifications.PushSample.Portable.Data.Push;

namespace Insane.Notifications.PushSample.UWP.Services.Handlers
{
    class UWPRemoteNotificationIdProvider : RemoteNotificationIdProvider<PushData>
    {
        public override string GetNotificationId(PushData data)
        {
            return data?.Type;
        }
    }
}
