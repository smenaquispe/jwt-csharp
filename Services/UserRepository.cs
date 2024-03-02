using WebApi.Models;
using WebApi.Data;
using Microsoft.EntityFrameworkCore;
namespace WebApi.Services;

public interface IUserRepository
{
    Task<UserModel?> Authenticate(string username, string password);
    Task<UserModel?> Register(RegisterModel model);

    string HashPassword(string password);

    bool VerifyPassword(string password, string hash);
}

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserModel?> Authenticate(string username, string password)
    {

        var res = await _context.UserModels.FirstOrDefaultAsync(x => (x.Username == username) || (x.Email == username));
        if (res == null)
        {
            return null;
        } 


        var model = new UserModel {
            Username = res.Username,
            Email = res.Email,
            Password = res.Password
        };


        var passwordCorrect = VerifyPassword(password, model.Password);
        if (!passwordCorrect)
        {
        
            Console.WriteLine("Password incorrect");
            return null;
        }


        Console.WriteLine("Password correct");
        return model;

    
    }

    public async Task<UserModel?> Register(RegisterModel model)
    {
        if(await _context.LoginModels.AnyAsync(x => x.Username == model.Username))
        {
            return null;
        }

        _context.UserModels.Add(new UserModel
        {
            Username = model.Username,
            Email = model.Email,
            Password = HashPassword(model.Password)
        });

        await _context.SaveChangesAsync();

        return new UserModel
        {
            Username = model.Username,
            Email = model.Email
        };

    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}