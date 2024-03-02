using WebApi.Models;
namespace WebApi.Services;
public interface IUserService
{
    Task<UserModel?> Authenticate(LoginModel model);
    Task<UserModel?> Register(RegisterModel model);

}


public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserModel?> Authenticate(LoginModel model)
    {

        return await _userRepository.Authenticate(model.Username, model.Password);
    }

    public async Task<UserModel?> Register(RegisterModel model)
    {
        return await _userRepository.Register(model);
    }
}