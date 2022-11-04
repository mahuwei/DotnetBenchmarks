// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");
IServiceProvider serviceProvider = null!;

string? GetName(string? firstName) {
    var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
    using var httpClient = httpClientFactory.CreateClient("default");
    return firstName;
}
    
    