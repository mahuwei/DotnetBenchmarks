// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using ThrowHelperTest;

Console.WriteLine("Hello, World!");
await HttpClientTest.PostTest();

//IServiceProvider serviceProvider = null!;

//string? GetName(string? firstName) {
//    var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
//    using var httpClient = httpClientFactory.CreateClient("default");
//    return firstName;
//}

