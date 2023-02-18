using Domain.Entities;
using Domain.Services;

namespace API.Services;

public class HardcodedAuthenticationService : IAuthenticationService
{
    private static readonly List<UserRecord> _users = new List<UserRecord>()
    {
        new UserRecord()
        {
            Id = 1,
            Username = "admin",
            Password = "S3cr3t"
        }
    };

    public bool UsernameExists(string username)
    {
        return _users.Any(x => x.Username == username);
    }

    public bool ValidatePassword(string username, string password)
    {
        return _users.Where(user => user.Username == username)
            .Any(user => PasswordMatch(user.Password, password));
    }

    private static bool PasswordMatch(string password, string match)
    {
        return password == match;
    }
}
