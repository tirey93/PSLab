using Microsoft.Extensions.Options;
using PSLab.Settings;
using Spectre.Console;
using System;

namespace PSLab.Commands
{
    public class Lab1Cw2 : Command
    {
        private readonly IOptionsMonitor<Lab1Cw2Settings> _optionsMonitor;
        private readonly Table _table;

        public Lab1Cw2(IOptionsMonitor<Lab1Cw2Settings> optionsMonitor)
        {
            _optionsMonitor = optionsMonitor;

            _table = new Table();
            _table.AddColumn("Thread");
            _table.AddColumn("Value");

            for (int i = 0; i < 10; i++)
            {
                _table.AddRow(new string[2]
                {
                    $"#{i}",
                    $""
                });
            }
        }

        public async Task Execute()
        {
            var r = new Random();
            int x = 0;
            await AnsiConsole.Live(new Panel(Align.Center(_table)))
               .StartAsync(async ctx =>
               {
                   int index = 1;
                  
                   await Task.WhenAll(
                       CreateThread(ctx, 1),
                       CreateThread(ctx, 2),
                       CreateThread(ctx, 3),
                       CreateThread(ctx, 4),
                       CreateThread(ctx, 5),
                       CreateThread(ctx, 6),
                       CreateThread(ctx, 7),
                       CreateThread(ctx, 8),
                       CreateThread(ctx, 9),
                       CreateThread(ctx, 0)
                   );
               });
        }

        private Task CreateThread(LiveDisplayContext ctx, int index)
        {
            return Task.Run(async () =>
            {
                ManualResetEvent mre = new ManualResetEvent(false);
                mre.Set();

                int remainder = 'Z' - 'A' + 1;
                int i = 0;
                while (true)
                {
                    if (!_optionsMonitor.CurrentValue.Threads[index])
                    {
                        mre.Reset();
                    }
                    else
                    {
                        mre.Set();
                        _table.UpdateCell(index, 0, $"#{index}");

                        char c = (char)('A' + i % remainder);
                        _table.UpdateCell(index, 1, $"[green]{c}{index}[/]");
                        ctx.Refresh();
                        i++;
                    }
                    await Task.Delay(1000);
                }
            });
        }
    }
}
