using Health.ConsoleCommand.Interfaces;
using System.Net.Http.Json;

namespace Health.ConsoleCommand.Commands;

public class AddUserCommand : ICustomCommand
{
    private const string APP_PATH = "http://localhost:5140";

    public async Task Execute(params string[] param)
    {
        await AddUser(param[0], param[1]);
    }

    public override string ToString()
    {
        return $"create-user         -  create user"
            + $"\n   [user_name]      -  the name of user"
            + $"\n   [user_password]  -  the password";
    }

    private async Task AddUser(string userName, string password)
    {
        var registerModel = new
        {
            Email = userName,
            Password = password,
            ConfirmPassword = password
        };
        try
        {
            using (var client = new HttpClient())
            {
                var response = await (await client.PostAsJsonAsync(APP_PATH + "/SignUp", registerModel)).Content.ReadAsStringAsync();
                Console.WriteLine($"Пользователь-администратор был создан: {response}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

