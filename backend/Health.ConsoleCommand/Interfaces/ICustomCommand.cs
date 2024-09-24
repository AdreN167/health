namespace Health.ConsoleCommand.Interfaces;

public interface ICustomCommand
{
    Task Execute(params string[] param);
}

