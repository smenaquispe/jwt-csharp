namespace WebApi.Models;

public class RegisterModel
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Hash = "este es el hash";

    public RegisterModel()
    {
        Username = "";
        Password = "";
        Email = "";
    }
}
