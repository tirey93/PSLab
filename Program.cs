
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PSLab.Commands;
using PSLab.Settings;

internal class Program
{
    private static async Task Main()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var serviceProvider = new ServiceCollection()
            .Configure<MainSettings>(configuration.GetSection(nameof(MainSettings)))
            .AddSingleton<Lab1Command>()
            .BuildServiceProvider();

        var lab1Command = serviceProvider.GetRequiredService<Lab1Command>();

        await lab1Command.Execute();
    }
}