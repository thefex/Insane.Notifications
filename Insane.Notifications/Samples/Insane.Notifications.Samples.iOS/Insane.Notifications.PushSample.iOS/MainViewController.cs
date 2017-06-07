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
            NavigationController.SetNavigationBarHidden(true, false);

            UIImageView logoImage = new UIImageView(UIImage.FromBundle("Image"));

			UILabel companyLabel = new UILabel();
			companyLabel.TextColor = UIColor.Black;
			companyLabel.Font = UIFont.SystemFontOfSize(16, UIFontWeight.Light);
            companyLabel.Text = "Insane Notifications";
         
            UILabel label = new UILabel();
            label.TextColor = UIColor.Black;
            label.Font = UIFont.SystemFontOfSize(18, UIFontWeight.Light);
            label.Lines = 2;
            label.TextAlignment = UITextAlignment.Center;

            var subscribeToPushButton = new UIButton(UIButtonType.System);
            subscribeToPushButton.SetTitle("Subscribe to push", UIControlState.Normal);
            subscribeToPushButton.SetTitle("Subscribe to push", UIControlState.Highlighted);
            subscribeToPushButton.SetTitle("Subscribe to push", UIControlState.Disabled);

            var unsubscribeFromPushButton = new UIButton(UIButtonType.System);
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
            Add(logoImage);
            Add(companyLabel);
            Add(label);
            Add(subscribeToPushButton);
            Add(unsubscribeFromPushButton);

			View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            View.AddConstraints(
                logoImage.AtTopOf(View, 30),
                logoImage.WithSameCenterX(View),

                companyLabel.Below(logoImage, 10),
				companyLabel.WithSameCenterX(View),

                label.AtTopOf(companyLabel, 40),
                label.AtLeftOf(View, 40),
                label.AtRightOf(View, 40),
                label.Height().EqualTo(120),
                label.WithSameCenterX(View),

				subscribeToPushButton.WithSameCenterX(label),
                subscribeToPushButton.Below(label, 35),

                subscribeToPushButton.Height().EqualTo(50),
                subscribeToPushButton.AtLeftOf(View, 40),
                subscribeToPushButton.AtRightOf(View, 40),

                unsubscribeFromPushButton.Below(subscribeToPushButton, 15),
                unsubscribeFromPushButton.Height().EqualTo(50),
                unsubscribeFromPushButton.WithSameCenterX(subscribeToPushButton),
                subscribeToPushButton.WithSameCenterX(View)
            );
        }
    }
}
