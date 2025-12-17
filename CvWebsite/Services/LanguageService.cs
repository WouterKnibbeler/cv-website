using CvWebsite.Core.Services;
using Microsoft.JSInterop;

namespace CvWebsite.Services;

public class LanguageService(IJSRuntime js) : ILanguageService
{
    public SiteLanguage Current { get; private set; } = SiteLanguage.Nl;
    public event Action? Changed;

    public async Task InitializeAsync()
    {
        var module = await js.InvokeAsync<IJSObjectReference>("import", "./js/lang.js");
        var lang = await module.InvokeAsync<string>("getLang");
        Current = lang.ToLowerInvariant() == "en" ? SiteLanguage.En : SiteLanguage.Nl;
        Changed?.Invoke();
    }

    public async Task SetAsync(SiteLanguage lang)
    {
        Current = lang;
        var module = await js.InvokeAsync<IJSObjectReference>("import", "./js/lang.js");
        await module.InvokeVoidAsync("setLang", lang == SiteLanguage.En ? "en" : "nl");
        Changed?.Invoke();
    }
}