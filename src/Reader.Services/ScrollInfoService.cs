using Microsoft.JSInterop;
using Reader.Domain.Interfaces;

namespace Reader.Services;

public class ScrollInfoService : IScrollInfoService
{
    public event EventHandler<float>? OnScrollEnd; 

    public ScrollInfoService(IJSRuntime jsRuntime)
    {
        RegisterServiceViaJsRuntime(jsRuntime);
    }

    private void RegisterServiceViaJsRuntime(IJSRuntime jsRuntime)
    {
        jsRuntime.InvokeVoidAsync("RegisterScrollInfoService", DotNetObjectReference.Create(this));
    }

    public float YPercentValue { get; private set; }

    [JSInvokable("OnScrollEnd")]
    public void JsOnScrollEnd(float yPercentValue)
    {
        yPercentValue *= -1;
        YPercentValue = yPercentValue;
        OnScrollEnd?.Invoke(this, YPercentValue);
    }
}
