
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
            .AddSingleton<Lab1Cw1>()
            .AddSingleton<Lab1Command>()
            .BuildServiceProvider();

        var options = serviceProvider.GetService<IOptions<MainSettings>>();

        try
        {
            Command command = options.Value.Mode switch
            {
                Mode.Lab1Cw1 => serviceProvider.GetService<Lab1Cw1>(),
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