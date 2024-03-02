namespace WebApi.Models;

public class LoginModel
{


    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Hash = "este es el hash";


    public LoginModel()
    {
        Username = "";
        Password = "";
    }
}