using Microsoft.Extensions.Options;
using PSLab.Settings;
using Spectre.Console;

namespace PSLab.Commands
{
    public class Lab1Command
    {
        private readonly IOptionsMonitor<MainSettings> _optionsMonitor;

        public Lab1Command(IOptionsMonitor<MainSettings> optionsMonitor)
        {
            _optionsMonitor = optionsMonitor;
        }

        public async Task Execute()
        {
            var table = new Table();
            table.AddColumn("Time");
            table.AddColumn("Value");

            for (int i = 0; i < 2; i++)
            {
                table.AddRow(new string[2]
                {
                    "",
                    $"abc"
                });
            }

            var r = new Random();
            int x = 0;
            await AnsiConsole.Live(new Panel(Align.Center(table)))
               .StartAsync(async ctx =>
               {
                   await Task.WhenAll(
                       Task.Run(async () =>
                       {
                           while (true)
                           {
                               table.UpdateCell(0, 0, "1");
                               table.UpdateCell(0, 1, $"[green]{_optionsMonitor.CurrentValue.Field1 * r.Next(100)}[/]");
                               ctx.Refresh();
                               await Task.Delay(123);
                           }
                       }),
                       Task.Run(async () =>
                       {
                           while (true)
                           {
                               table.UpdateCell(1, 0, "2");
                               table.UpdateCell(1, 1, $"[green]{_optionsMonitor.CurrentValue.Field2 * r.Next(100)}[/]");
                               ctx.Refresh();
                               await Task.Delay(265);
                           }
                       }));
               });
        }
    }
}
