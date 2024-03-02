namespace WebApi.Models;

public class UserModel
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string ? Email { get; set; }

    public string Role { get; set; }

    public UserModel()
    {
        Id = Guid.NewGuid().GetHashCode();
        Username = "";
        Password = "";
        Email = null;
        Role = "User";
    }
}
