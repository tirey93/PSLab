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

            for (int i = 0; i < 10; i++)
            {
                table.AddRow(new string[2]
                {
                    DateTime.Now.ToString("HH:mm:ss"),
                    $"[green]{_optionsMonitor.CurrentValue.Field1}[/]"
                });
            }

            int x = 0;
            await AnsiConsole.Live(new Panel(Align.Center(table)))
               .StartAsync(async ctx =>
               {
                   while (true)
                   {
                       table.UpdateCell(x % 10, 0, DateTime.Now.ToString("HH:mm:ss"));
                       table.UpdateCell(x % 10, 1, $"[green]{_optionsMonitor.CurrentValue.Field1}[/]");

                       ctx.Refresh();
                       await Task.Delay(1000);
                       x++;
                   }
               });
        }
    }
}
