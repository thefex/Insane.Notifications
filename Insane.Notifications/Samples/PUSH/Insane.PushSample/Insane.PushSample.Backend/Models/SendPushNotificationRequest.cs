namespace Insane.PushSample.Backend.Models
{
    public class SendPushNotificationRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public PushPlatformType Platform { get; set; }
        public string Tag { get; set; }
    }
}