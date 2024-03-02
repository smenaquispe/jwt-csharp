using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{

    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest("Invalid data");
        }

        var user = await _userService.Authenticate(model);

        if(user == null)
        {
            return BadRequest("Invalid username or password");
        }

        var token = GenerateJwtToken(user);

        return Ok(new { user, token });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest("Invalid data");
        }

        if(model.Password.Length < 8)
        {
            return BadRequest("Password must be at least 8 characters long");
        }

        if(model.Username.Length < 5)
        {
            return BadRequest("Username must be at least 5 characters long");
        }

        var user = await _userService.Register(model);

        if(user == null)
        {
            return BadRequest("Username already exists");
        }

        return Ok(user);

    }

    private string GenerateJwtToken(UserModel user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("hola"); // Debes mantener tu clave secreta de forma segura
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role) // Suponiendo que tienes un campo "Role" en tu modelo de usuario
            }),
            Expires = DateTime.UtcNow.AddHours(1), // Definir el tiempo de expiración del token
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

}