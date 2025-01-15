using noteapi.Dto;
using noteapi.Models;

namespace noteapi.Repository
{
    public interface IUserRepository
    {
        Task Save(UserRequest userRequest);
        Task<User> Get(string username);
    }
}
