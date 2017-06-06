using System;
using MvvmCross.Core.ViewModels;

namespace Insane.Notifications.PushSample.Portable.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        bool _isRegisteredToPush;
        bool _isRegisteringInProgress;

        public MainViewModel()
        {
        }

        public bool IsRegisteredToPush 
        {
            get { return _isRegisteredToPush; }    
            set { SetProperty(ref _isRegisteredToPush, value); }
        }

        public bool IsRegisteringInProgress {
            get { return _isRegisteringInProgress; }
            set { SetProperty(ref _isRegisteringInProgress, value); }
        }


        public bool CanSubscribeToPush => !IsRegisteredToPush && !IsRegisteringInProgress;

        public bool CanUnsubscribeFromPush => IsRegisteredToPush && !IsRegisteringInProgress;

        public MvxCommand SubcribeToPush { get; }
            = new MvxCommand(() =>
            {

            });

        public MvxCommand UnsubscribeFromPush { get; }
           = new MvxCommand(() =>
            {

            });


    }
}
