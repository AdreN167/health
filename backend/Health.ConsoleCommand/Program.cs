using Health.ConsoleCommand.Commands;
using Health.ConsoleCommand.Interfaces;

var commands = new Dictionary<string, ICustomCommand>()
{
    { "exit", new ExitCommand() },
    { "create-user", new AddUserCommand() }
};

do
{
    Console.Write("Server > ");

    var command = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(command))
    {
        continue;
    }

    var parsedCommand = command.Split(' ');

    if (commands.ContainsKey(parsedCommand[0]))
    {
        try
        {
            await commands[parsedCommand[0]].Execute(parsedCommand.Skip(1).ToArray());
        }
        catch
        {
            Console.WriteLine($"Invalid arguments for command [{parsedCommand[0]}]");
        }
    }
    else
    {
        Console.WriteLine("Invalid command");
    }

} while (true);