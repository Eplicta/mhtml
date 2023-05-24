using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Eplicta.Mhtml.Sample;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddTransient<Loader>()
            .AddHttpClient()
            .BuildServiceProvider();

        var loader = serviceProvider.GetService<Loader>();
        var result = await loader.Get(new Uri("https://eplicta.se/"));

        var renderer = new Renderer(result);
        await using var archive = renderer.GetStream();
        await File.WriteAllBytesAsync("C:\\temp\\test.mhtml", archive.ToArray());
    }
}