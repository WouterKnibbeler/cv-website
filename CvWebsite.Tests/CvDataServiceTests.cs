using System.Net;
using CvWebsite.Core.Services;
using Xunit;

public class CvDataServiceTests
{
    [Fact]
    public async Task GetAsync_Caches_Per_Language()
    {
        var handler = new FakeHandler();
        var http = new HttpClient(handler) { BaseAddress = new Uri("http://localhost/") };

        var svc = new CvDataService(http);

        var nl1 = await svc.GetAsync(SiteLanguage.Nl);
        var nl2 = await svc.GetAsync(SiteLanguage.Nl);
        var en1 = await svc.GetAsync(SiteLanguage.En);

        Assert.Same(nl1, nl2);
        Assert.NotSame(nl1, en1);
        Assert.True(handler.CallCount >= 2);
    }

    private sealed class FakeHandler : HttpMessageHandler
    {
        public int CallCount { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            CallCount++;

            var json = request.RequestUri!.ToString().Contains("cv.en.json")
                ? """{ "name":"EN","nav":{},"highlights":[],"projects":[],"work":[] }"""
                : """{ "name":"NL","nav":{},"highlights":[],"projects":[],"work":[] }""";

            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json)
            });
        }
    }
}