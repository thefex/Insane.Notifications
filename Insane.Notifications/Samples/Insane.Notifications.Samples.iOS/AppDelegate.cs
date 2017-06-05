﻿using System;
using Foundation;
using Insane.Notifications.iOS;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.Platform;
using UIKit;

namespace MvvmCross.Plugins.Notifications.Samples.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register("AppDelegate")]
	public class AppDelegate : MvxApplicationDelegate
	{
		// class-level declarations

		public override UIWindow Window
		{
			get;
			set;
		}

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			Window = new UIWindow(UIScreen.MainScreen.Bounds);

			var setup = new Setup(this, Window);
			setup.Initialize();

			var startup = Mvx.Resolve<IMvxAppStart>();
			startup.Start();

			Window.MakeKeyAndVisible();

			return true;
		}

		public override void OnResignActivation(UIApplication application)
		{
			// Invoked when the application is about to move from active to inactive state.
			// This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
			// or when the user quits the application and it begins the transition to the background state.
			// Games should use this method to pause the game.
		}

		public override void DidEnterBackground(UIApplication application)
		{
			// Use this method to release shared resources, save user data, invalidate timers and store the application state.
			// If your application supports background exection this method is called instead of WillTerminate when the user quits.
		}

		public override void WillEnterForeground(UIApplication application)
		{
			// Called as part of the transiton from background to active state.
			// Here you can undo many of the changes made on entering the background.
		}

		public override void OnActivated(UIApplication application)
		{
			// Restart any tasks that were paused (or not yet started) while the application was inactive. 
			// If the application was previously in the background, optionally refresh the user interface.
		}

		public override void WillTerminate(UIApplication application)
		{
			// Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
		}

	    public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
	    {
	        base.RegisteredForRemoteNotifications(application, deviceToken);

	        PushiOSNotificationsSetup.OnRegisterToPushSuccess(deviceToken);
	    }

	    public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
	    {
	        base.FailedToRegisterForRemoteNotifications(application, error);

	        PushiOSNotificationsSetup.OnRegisterToPushFailure(error);
	    }

	    public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
	    {
	        base.ReceivedRemoteNotification(application, userInfo);
	    }
	}
}

