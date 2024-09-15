namespace Health.Core.Features.Chat.Hubs;

public class UserConnection 
{
    public string UserName { get; set; }
    public string ChatRoom { get; set; }

    public UserConnection(string userName, string chatRoom)
    {
        UserName = userName;
        ChatRoom = chatRoom;
    }
}

