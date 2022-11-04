// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

Console.Title = "Get web api test";
Console.WriteLine();
var serviceCollection = new ServiceCollection();
serviceCollection.AddHttpClient("default",
    options => { options.BaseAddress = new Uri("http://192.168.1.200:5186"); }
//options => { options.BaseAddress = new Uri("http://192.168.1.51:5186"); }
);
var serviceProvider = serviceCollection.BuildServiceProvider();
HttpGetTest.ServiceProvider = serviceProvider;
var foregroundColor = Console.ForegroundColor;

do{
    Console.WriteLine(" => 输入运行次数(exit 退出)...");
    var input = Console.ReadLine();
    if(string.IsNullOrWhiteSpace(input)) continue;

    if(input.ToLower() == "exit") break;

    if(int.TryParse(input, out var count) == false){
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(" => 输入错误：{0}", input);
        Console.ForegroundColor = foregroundColor;
        continue;
    }

    var stopwatch = new Stopwatch();
    var tasks = new List<Task>();
    stopwatch.Start();
    for(var i = 0; i < count; i++){
        var no = i;
        var task = Task.Run(() => GetAsync(no));
        if(no % 100 == 0) Thread.Sleep(10);
        tasks.Add(task);
    }

    try{
        Task.WaitAll(tasks.ToArray());
    }
    catch(AggregateException ex){
        Console.WriteLine($" => 执行报错，错误数量:{ex.InnerExceptions.Count}");
    }

    stopwatch.Stop();
    Console.WriteLine($" => 运行次数:{count}, 时长:{stopwatch.ElapsedMilliseconds / 1000}");
} while(true);

Console.WriteLine(" => 测试完成，按任意键退出。");
Console.ReadKey();


async Task GetAsync(int no) {
    var httpClientFactory = serviceProvider!.GetRequiredService<IHttpClientFactory>();
    using var httpClient = httpClientFactory.CreateClient("default");
    var httpResponseMessage = await httpClient.GetAsync("/WeatherForecast");
    httpResponseMessage.EnsureSuccessStatusCode();
    await httpResponseMessage.Content.ReadAsStringAsync();
    Console.WriteLine($" => run time:{no}");
}