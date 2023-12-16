using Microsoft.JSInterop;

namespace BlazorAppPWA.Client.Data
{
    public class PushNotification
    {
        private IJSRuntime js;

        public PushNotification(IJSRuntime js)
        {
            this.js = js;
        }
        public async Task SendNotificationAsync(string message)
        {
            await js.InvokeVoidAsync("blazorPushNotifications.sendPushNotification", $"{message}");
        }
    }
}
