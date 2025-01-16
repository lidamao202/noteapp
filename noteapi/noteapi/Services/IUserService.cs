using noteapi.Dto;

namespace noteapi.Services
{
    public interface IUserService
    {
        Task<string> Authenticate(string username, string password);
        Task Register(UserRequest userRequest);
        Task<IEnumerable<UserResponse>> GetAll();
        Task<bool> UserExists(string username);
    }
}
