# Insane Notifications
Xamarin plugin to handle notifications with ease.

# Get it on NuGet
Prerelease package is available on my nuget - https://www.nuget.org/profiles/thefex
InsaneNotifications.* (platform)

# Changelog
1.0.0-prerelease
- initial library release

# Things TODO - "ROADMAP"
1. Remove tight coupling with MvvmCross and release Mvx support as separated package.
2. Implement iOS Local Notifications
3. Improve Android Local Notifications sample

# Sample - PUSH Notifications - Azure Notification Hub Backend
As current version is mainly focused on PUSH Based of Azure Notification Hubs I have prepared C# ASP.Net WebAPI sample project - implementation of Azure Notification Hub usage / push send.

Sample API project is available here:
https://github.com/thefex/Insane.Notifications/tree/master/Insane.Notifications/Samples/PUSH/Insane.PushSample/Insane.PushSample.Backend

Keep in mind to get project works you have to insert your "Azure Notification Hub" connection string + notification hub name in this file:
https://github.com/thefex/Insane.Notifications/blob/master/Insane.Notifications/Samples/PUSH/Insane.PushSample/Insane.PushSample.Backend/Services/NotificationHubRegistrationServices.cs

This API is also deployed here:
http://xamarinpushplugintests.azurewebsites.net/swagger/ui/index

# Acknowledgment 
This library has been created thanks to help and support from InsaneLab.com (http://www.insanelab.com).
Thanks for giving me time to work on this project - you guys rock :-)

Also, many thanks to Erlend Angelsen for some great ideas and insights.

# Side notes
If you have observed any weird behaviour with this library - please, fill an issue.
PR's are welcome as well.
