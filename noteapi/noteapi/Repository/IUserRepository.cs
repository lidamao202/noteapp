using noteapi.Dto;
using noteapi.Models;

namespace noteapi.Repository
{
    public interface IUserRepository
    {
        void Save(UserRequest userRequest);
        Task<UserResponse> Get(string username);
    }
}
