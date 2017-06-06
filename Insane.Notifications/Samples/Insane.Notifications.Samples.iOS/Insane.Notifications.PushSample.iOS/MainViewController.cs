using System;
using Cirrious.FluentLayouts.Touch;
using Insane.Notifications.PushSample.Portable.ValueConverter;
using Insane.Notifications.PushSample.Portable.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using UIKit;


namespace Insane.Notifications.PushSample.iOS
{
    public class MainViewController : MvxViewController<MainViewModel>
    {
        public MainViewController()
        {
        }

        public MainViewController(IntPtr handle) : base(handle)
        {
        }

        public MainViewController(string nibName, Foundation.NSBundle bundle) : base(nibName, bundle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            UILabel label = new UILabel();
            label.TextColor = UIColor.Black;
            label.Font = UIFont.SystemFontOfSize(20, UIFontWeight.Medium);

            var subscribeToPushButton = new UIButton();
            subscribeToPushButton.SetTitle("Subscribe to push", UIControlState.Normal);
            subscribeToPushButton.SetTitle("Subscribe to push", UIControlState.Highlighted);
            subscribeToPushButton.SetTitle("Subscribe to push", UIControlState.Disabled);

            var unsubscribeFromPushButton = new UIButton();
            unsubscribeFromPushButton.SetTitle("Unsubscribe from push", UIControlState.Normal);
            unsubscribeFromPushButton.SetTitle("Unsubscribe from push", UIControlState.Highlighted);
            unsubscribeFromPushButton.SetTitle("Unsubscribe from push", UIControlState.Disabled);

            var bindingSet = this.CreateBindingSet<MainViewController, MainViewModel>();

            bindingSet.Bind(label)
                      .To(x => x.IsRegisteredToPush)
                      .WithConversion(new PushStateToTextValueConverter());

            bindingSet.Bind(subscribeToPushButton)
                      .For(x => x.Enabled)
                      .To(x => x.CanSubscribeToPush);

            bindingSet.Bind(subscribeToPushButton)
                      .To(x => x.SubscribeToPush);

            bindingSet.Bind(unsubscribeFromPushButton)
                      .For(x => x.Enabled)
                      .To(x => x.CanUnsubscribeFromPush);

            bindingSet.Bind(unsubscribeFromPushButton)
                      .To(x => x.UnsubscribeFromPush);

            bindingSet.Apply();
            Add(label);
            Add(subscribeToPushButton);
            Add(unsubscribeFromPushButton);

			View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

			View.AddConstraints(
                label.WithSameLeft(View),
                label.WithSameRight(View),
                label.Height().EqualTo(120),
                label.AtTopOf(View).Plus(35),

                subscribeToPushButton.Below(label, 35),

                subscribeToPushButton.Height().EqualTo(50),
                subscribeToPushButton.WithSameLeft(View),
                subscribeToPushButton.WithSameRight(View),

                unsubscribeFromPushButton.Below(subscribeToPushButton, 15),
                unsubscribeFromPushButton.Height().EqualTo(50),
                unsubscribeFromPushButton.WithSameCenterX(View)
            );
        }
    }
}
