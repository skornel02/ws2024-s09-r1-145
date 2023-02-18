namespace Domain.Services;

public interface IAuthenticationService
{
    public bool UsernameExists(string username);
    public bool ValidatePassword(string username, string password);
}
