namespace CvWebsite.Core.Services;

public enum SiteLanguage { Nl, En }

public interface ILanguageService
{
    SiteLanguage Current { get; }
    event Action? Changed;
    Task InitializeAsync();
    Task SetAsync(SiteLanguage lang);
}