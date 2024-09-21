using Health.ConsoleCommand.Commands;
using Health.ConsoleCommand.Interfaces;

var commands = new Dictionary<string, ICustomCommand>()
{
    { "exit", new ExitCommand() },
    { "create-user", new AddUserCommand() },
    { "help", new HelpCommand() },
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

    if (parsedCommand[0].Equals("help"))
    {
        commands[parsedCommand[0]].Execute(commands.Select(c => c.Value.ToString()).Where(c => !c.Equals("help")).ToArray());
    }

    if (commands.ContainsKey(parsedCommand[0]))
    {
        try
        {
            commands[parsedCommand[0]].Execute(parsedCommand.Skip(1).ToArray());
        }
        catch
        {
            Console.WriteLine($"Invalid arguments for command [{parsedCommand[0]}]");
        }
    }
    else
    {
        Console.WriteLine("Invalid command, use help for more information");
    }

} while (true);