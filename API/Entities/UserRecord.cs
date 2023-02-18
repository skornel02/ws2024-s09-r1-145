namespace API.Entities
{
    public class UserRecord
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }

        public UserRecord(int id, string username, string password)
        {
            Id = id;
            Username = username;
            Password = password;
        }
    }
}
