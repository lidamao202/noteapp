using noteapi.Dto;
using noteapi.Models;

namespace noteapi.Repository
{
    public interface IUserRepository
    {
        Task Save(UserRequest userRequest);
        Task<User> Get(string username);
        Task<IEnumerable<UserResponse>> GetAll();
        Task<User> Login(string username, string password);
    }
}
