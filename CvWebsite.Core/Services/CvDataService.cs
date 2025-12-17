using System.Text.Json;
using System.Net.Http.Json;
using CvWebsite.Core.Models;

namespace CvWebsite.Core.Services;

public interface ICvDataService
{
    Task<CvData> GetAsync(SiteLanguage lang, CancellationToken ct = default);
}

public class CvDataService(HttpClient http) : ICvDataService
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly Dictionary<SiteLanguage, CvData> _cache = new();

    public async Task<CvData> GetAsync(SiteLanguage lang, CancellationToken ct = default)
    {
        if (_cache.TryGetValue(lang, out var cached))
            return cached;

        var file = lang == SiteLanguage.En ? "Data/cv.en.json" : "Data/cv.nl.json";

        var data = await http.GetFromJsonAsync<CvData>(file, Options, ct)
                   ?? throw new InvalidOperationException($"{file} could not be loaded.");

        _cache[lang] = data;
        return data;
    }
}