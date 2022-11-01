using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
[MemoryDiagnoser(false)]
public class HttpGetTest {
    public static IServiceProvider ServiceProvider {get; set;} = null!;

    [Params(10)]
    public int Size {get; set;} = 100;

    [GlobalSetup]
    public void Setup() {
    }

    [Benchmark]
    public void For() {
        for(var i = 0; i < 100; i++) Task.Run(async () => await GetAsync(i));
    }

    private async Task GetAsync(int no) {
        var httpClientFactory = ServiceProvider!.GetRequiredService<IHttpClientFactory>();
        using var httpClient = httpClientFactory.CreateClient("default");
        var httpResponseMessage = await httpClient.GetAsync("/WeatherForecast");
        httpResponseMessage.EnsureSuccessStatusCode();
        await httpResponseMessage.Content.ReadAsStringAsync();
        Console.WriteLine($"run time:{no}");
    }
}