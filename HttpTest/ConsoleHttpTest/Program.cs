// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.DependencyInjection;

Console.Title = "Get web api test";
Console.WriteLine();
var serviceCollection = new ServiceCollection();
serviceCollection.AddHttpClient("default", options => {
    options.BaseAddress = new Uri("http://localhost:5186");
});
var serviceProvider = serviceCollection.BuildServiceProvider();
HttpGetTest.ServiceProvider = serviceProvider;

Console.WriteLine("按任意键开始测试...");
Console.ReadKey();
var stopwatch = new Stopwatch();
var count = 10000;
var tasks = new List<Task>();
stopwatch.Start();
for (int i = 0; i < count; i++){
    var no = i;
    var task = Task.Run(() => GetAsync(no));
    if(no % 100 == 0){
        Thread.Sleep(10);
    }
    tasks.Add(task);
}

Task.WaitAll(tasks.ToArray());
stopwatch.Stop();
Console.WriteLine($"运行次数:{count}, 时长:{stopwatch.ElapsedMilliseconds / 1000}");
//BenchmarkRunner.Run<HttpGetTest>();

Console.WriteLine("测试完成，按任意键退出。");
Console.ReadKey();


async Task GetAsync(int no) {
    var httpClientFactory = serviceProvider!.GetRequiredService<IHttpClientFactory>();
    using var httpClient = httpClientFactory.CreateClient("default");
    var httpResponseMessage = await httpClient.GetAsync("/WeatherForecast");
    httpResponseMessage.EnsureSuccessStatusCode();
    await httpResponseMessage.Content.ReadAsStringAsync();
    Console.WriteLine($"run time:{no}");
}