#import <AppTrackingTransparency/ATTrackingManager.h>
#pragma mark - Unity Bridge
extern "C" {
    int getATTSupported() {
        if (@available(iOS 14, *))
        {
            return (int)ATTrackingManager.trackingAuthorizationStatus;
        }
        return -1;
    }
}

