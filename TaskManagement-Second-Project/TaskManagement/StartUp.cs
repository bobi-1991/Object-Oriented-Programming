using TaskManagement.Core.Contracts;
using TaskManagement.Core;

namespace TaskManagement
{
    internal class StartUp
    {
        static void Main(string[] args)
        {
            IRepository repository = new Repository();
            ICommandFactory commandFactory = new CommandFactory(repository);
            IEngine engine = new Engine(commandFactory);
            engine.Start();
        }
    }
}
