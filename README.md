# Insane Notifications
Xamarin plugin to handle notifications with ease.

# Get it on NuGet
Nuget packages are available on my nuget - https://www.nuget.org/profiles/thefex

To use with:
1. Android GCM - Install-Package Insane.Notifications.Droid.GCM (for MvvmCross go with: Insane.Notifications.Droid.MvxGCM)
2. iOS - Install-Package Insane.Notifications.iOS
3. UWP - Install-Package Insane.Notifications.UWP
4. Portable project - Install.Package Insane.Notifications

# Changelog
1.0.0
- official, initial library release

# Things TODO - "ROADMAP sorted by importance"
1. Implement iOS Local Notifications
2. Improve Android Local Notifications sample
3. Write Local Notifications documentation.
4. Add FCM support

# Sample - PUSH Notifications - Azure Notification Hub Backend
As current version is mainly focused on PUSH Based of Azure Notification Hubs I have prepared C# ASP.Net WebAPI sample project - implementation of Azure Notification Hub usage / push send.

Sample API project is available here:
https://github.com/thefex/Insane.Notifications/tree/master/Insane.Notifications/Samples/PUSH/Insane.PushSample/Insane.PushSample.Backend

Keep in mind to get project works you have to insert your "Azure Notification Hub" connection string + notification hub name in this file:
https://github.com/thefex/Insane.Notifications/blob/master/Insane.Notifications/Samples/PUSH/Insane.PushSample/Insane.PushSample.Backend/Services/NotificationHubRegistrationServices.cs

This API is also deployed here:
http://xamarinpushplugintests.azurewebsites.net/swagger/ui/index

# Documentation
Library documentation/presentation is available here:
https://github.com/thefex/Insane.Notifications/blob/master/InsaneNotificationsPresentation.pdf

# Acknowledgment 
This library has been created thanks to help and support from InsaneLab.com (http://www.insanelab.com).
Thanks for giving me time to work on this project - you guys rock :-)

Also, many thanks to Erlend Angelsen for some great ideas and insights.

# Side notes
If you have observed any weird behaviour with this library - please, fill an issue.
PR's are welcome as well.
