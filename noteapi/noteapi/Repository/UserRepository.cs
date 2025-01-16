using Dapper;
using noteapi.Dto;
using noteapi.Models;
using Microsoft.AspNetCore.Identity;

namespace noteapi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;
        private readonly PasswordHasher<object> _passwordHasher;

        public UserRepository(DapperContext context,IConfiguration configuration) { 
            _context = context;
            _passwordHasher = new PasswordHasher<object>();

        }
        public async Task<User> Get(string username)
        {
            var query = "SELECT * FROM [user] WHERE username=@username";
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryFirstAsync<User>(query, new
                {
                    username = username,
                });
                return user;
            }
        }

        public async Task Save(UserRequest userRequest)
        {
            var query = "insert into [user](id,username,password,date_created,date_updated) values(@Id,@userName,@password,@date_created,@date_updated)";
            using (var connection = _context.CreateConnection())
            {
                var hashedPassword = _passwordHasher.HashPassword(null, userRequest.Password);
                var users = await connection.ExecuteAsync(query, new { 
                    Id=Guid.NewGuid().ToString(),
                    userName=userRequest.UserName,
                    password= hashedPassword,
                    date_created =DateTime.Now,
                    date_updated=DateTime.Now
                });
            }
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await Get(username);
            if (user == null)
            {
                return null;
            }
            var isPasswordValid = _passwordHasher.VerifyHashedPassword(null, user.Password, password);

            if (isPasswordValid == PasswordVerificationResult.Success)
            {
                return user;
            }
            return null;
        }

        public async Task<IEnumerable<UserResponse>> GetAll()
        {
            var query = "SELECT * FROM [user]";
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryAsync<UserResponse>(query);
                return user.ToList();
            }
        }
    }
}
