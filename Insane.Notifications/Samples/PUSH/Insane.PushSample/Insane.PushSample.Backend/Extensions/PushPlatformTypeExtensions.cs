using System;
using Insane.PushSample.Backend.Models;
using Microsoft.Azure.NotificationHubs;

namespace Insane.PushSample.Backend.Extensions
{
    public static class PushPlatformTypeExtensions
    {
        public static RegistrationDescription GetRegistrationDescription(this PushPlatformType platformType, string deviceHandle)
        {
            switch (platformType)
            {
                case PushPlatformType.APNS:
                    return new AppleRegistrationDescription(deviceHandle);
                case PushPlatformType.GCM:
                    return new GcmRegistrationDescription(deviceHandle);
                case PushPlatformType.MPNS:
                    return new MpnsRegistrationDescription(deviceHandle);
                case PushPlatformType.WNS:
                    return new WindowsRegistrationDescription(deviceHandle);
            }

            throw new InvalidOperationException($"{platformType} platform type is invalid.");
        }
    }
}