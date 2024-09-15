namespace Health.Core.Features.Chat.CustomClientMethods;

public interface IChatClient
{
    public Task RecieveMessage(string userName, string message);
}
