
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
            .Configure<Lab1Cw2Settings>(configuration.GetSection(nameof(Lab1Cw2Settings)))
            .AddSingleton<Lab1Cw1>()
            .AddSingleton<Lab1Cw2>()
            .AddSingleton<Lab1Cw3>()
            .AddSingleton<Lab1Cw4>()
            .BuildServiceProvider();

        var options = serviceProvider.GetService<IOptions<MainSettings>>();

        try
        {
            Command command = options.Value.Mode switch
            {
                Mode.Lab1Cw1 => serviceProvider.GetService<Lab1Cw1>(),
                Mode.Lab1Cw2 => serviceProvider.GetService<Lab1Cw2>(),
                Mode.Lab1Cw3 => serviceProvider.GetService<Lab1Cw3>(),
                Mode.Lab1Cw4 => serviceProvider.GetService<Lab1Cw4>(),
                _ => throw new NotImplementedException(),
            };

            await command.Execute();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Console.ReadKey();
        }
    }
}