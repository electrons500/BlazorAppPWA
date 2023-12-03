using Microsoft.JSInterop;

namespace BlazorAppPWA.Client.SweetAlert
{
    public class SweetAlertBase
    {
        private IJSRuntime _js;
        public SweetAlertBase(IJSRuntime js)
        {
            _js = js;
        }

        public async Task DefaultAlertAsync(string title = "Congratulations", string message = "", string icontype = "success")
        {
            await _js.InvokeVoidAsync("swal", title,message, icontype);
        }

        public async Task<bool> ConfirmAlertAsync(string title = "Congratulations", string message = "", string icontype = "success")
        {
            var result = await _js.InvokeAsync<object>("swal",title,message,icontype, new { buttons = true });
            if (result != null)
            {
                return true;
            }
            return false;
        }
    }
}
