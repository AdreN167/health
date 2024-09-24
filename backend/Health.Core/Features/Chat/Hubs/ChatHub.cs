using Health.Core.Features.Chat.CustomClientMethods;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Health.Core.Features.Chat.Hubs;

public class ChatHub : Hub<IChatClient>
{
    private readonly IDistributedCache _cache;

    public ChatHub(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task JoinChat(UserConnection connection)
    {
        // подключаем пользователя в группу с именем ChatRoom
        await Groups.AddToGroupAsync(Context.ConnectionId, connection.ChatRoom);

        // сериализуем connection
        var stringConnection = JsonSerializer.Serialize(connection);

        // добавляем conneciton в кэш
        await _cache.SetStringAsync(Context.ConnectionId, stringConnection);

        // оповещаем всех клиентов текущего чата, что пользователь присоединился
        await Clients
            .Group(connection.ChatRoom)
            .RecieveMessage("Admin", $"{connection.UserName} присоединился к чату");
    }

    public async Task SendMessage(string message)
    {
        var stringConnection = await _cache.GetAsync(Context.ConnectionId);
        var connection = JsonSerializer.Deserialize<UserConnection>(stringConnection);

        if (connection != null)
        {
            await Clients
                .Group(connection.ChatRoom)
                .RecieveMessage(connection.UserName, message);
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var stringConnection = await _cache.GetAsync(Context.ConnectionId);
        var connection = JsonSerializer.Deserialize<UserConnection>(stringConnection);

        if (connection != null)
        {
            await _cache.RemoveAsync(Context.ConnectionId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, connection.ChatRoom);

            await Clients
                .Group(connection.ChatRoom)
                .RecieveMessage("Admin", $"{connection.UserName} вышел из чата");
        }

        await base.OnDisconnectedAsync(exception);
    }
}

