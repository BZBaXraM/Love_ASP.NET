namespace ToDo_Web_APi.Providers;

public class UserInfo
{
    public string Id { get; set; }
    public string Username { get; set; }

    public UserInfo(string id, string username)
    {
        Id = id;
        Username = username;
    }
}