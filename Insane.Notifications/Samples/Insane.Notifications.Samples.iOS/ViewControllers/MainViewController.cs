using System;
using MvvmCross.iOS.Views;
using MvvmCross.Plugins.Notifications.Sample.Portable.ViewModels;
using UIKit;

namespace MvvmCross.Plugins.Notifications.Samples.iOS
{
	public partial class MainViewController : MvxViewController<MainViewModel>
	{
		protected MainViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public MainViewController()
		{

		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}
