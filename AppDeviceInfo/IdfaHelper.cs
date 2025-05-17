#if __IOS__
using Foundation;
using AdSupport;
using AppTrackingTransparency;
using UIKit;

namespace AppDeviceInfo;

public class IdfaHelper
{
    /* IMPORTANT:
     * This code requires the App Tracking Transparency framework to be linked in your project.
     * Make sure to add the following to your Info.plist:
     * <key>NSUserTrackingUsageDescription</key>
     * <string>This app uses tracking to provide personalized ads.</string>
     */
    
    public static async Task<string> GetAdvertiserIdAsync()
    {
        if (UIDevice.CurrentDevice.CheckSystemVersion(14, 0))
        {
            var status = ATTrackingManager.TrackingAuthorizationStatus;

            // If not determined, request permission
            if (status == ATTrackingManagerAuthorizationStatus.NotDetermined)
            {
                var tcs = new TaskCompletionSource<ATTrackingManagerAuthorizationStatus>();
                ATTrackingManager.RequestTrackingAuthorization(authStatus => tcs.SetResult(authStatus));
                status = await tcs.Task;
            }

            // Check if tracking is authorized
            if (status != ATTrackingManagerAuthorizationStatus.Authorized)
            {
                Console.WriteLine("Tracking not authorized by the user.");
                return "not authorized"; // IDFA is not accessible
            }
        }

        // Get the IDFA using ASIdentifierManager
        return ASIdentifierManager.SharedManager.AdvertisingIdentifier.AsString();

    }
}

#endif
