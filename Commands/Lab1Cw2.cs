using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.Options;
using PSLab.Settings;
using Spectre.Console;

namespace PSLab.Commands
{
    public class Lab1Cw2 : Command
    {
        private readonly IOptionsMonitor<Lab1Cw2Settings> _optionsMonitor;
        private readonly Table _table;
        private readonly List<Thread> _threads;

        public Lab1Cw2(IOptionsMonitor<Lab1Cw2Settings> optionsMonitor)
        {
            _optionsMonitor = optionsMonitor;

            _table = new Table();
            _table.AddColumn("Thread");
            _table.AddColumn("Value");

            _threads = new List<Thread>();


            for (int i = 0; i < 10; i++)
            {
                _table.AddRow(new string[2] { "#", "" });
                int threadIndex = i;
                Thread thread = new Thread(() => CreateThread(threadIndex));
                _threads.Add(thread);

            }
        }

        public Task Execute()
        {
            AnsiConsole.Live(new Panel(Align.Center(_table)))
                .Start(ctx =>
                {
                    foreach (var thread in _threads)
                        thread.Start();


                    while (true)
                    {
                        Thread.Sleep(100); // Zapobiegamy zajmowaniu 100% procesora.
                        ctx.Refresh();
                    }

                });

            return Task.CompletedTask;
        }


        private void CreateThread(int index)
        {
            int remainder = 'Z' - 'A' + 1;
            int i = 0;

            while (true)
            {
                if (_optionsMonitor.CurrentValue.Threads[index])
                {
                    _table.UpdateCell(index, 0, $"#{index}");

                    char c = (char)('A' + i % remainder);
                    _table.UpdateCell(index, 1, $"[green]{c}{index}[/]");
                    i++;
                }
                Thread.Sleep(1000);

            }
        }
    }
}