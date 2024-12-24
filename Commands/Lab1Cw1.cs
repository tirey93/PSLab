

namespace PSLab.Commands
{
    internal class Lab1Cw1 : Command
    {
        public async Task Execute()
        {
            Task task = Task.Run(() =>
            {
                Console.WriteLine("Hello world");
            });

            await task;
        }
    }
}
