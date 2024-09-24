using Health.ConsoleCommand.Interfaces;

namespace Health.ConsoleCommand.Commands;

public class ExitCommand : ICustomCommand
{
    public async Task Execute(params string[] param)
    {
        Environment.Exit(0);
    }

    public override string ToString()
    {
        return $"exit                -  exit command";
    }
}

