using Dapper;
using noteapi.Dto;
using noteapi.Models;

namespace noteapi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;
        public UserRepository(DapperContext context) { 
            _context = context; 
        }
        public async Task<UserResponse> Get(string username)
        {
            var query = "SELECT * FROM [user]";
            using (var connection = _context.CreateConnection())
            {
                var users = await connection.QueryAsync<User>(query);
                var user = users.ToList().First();
                if (user != null)
                {
                    var userRes = new UserResponse()
                    {
                        UserName = username,
                    };
                    return userRes;
                }
            }
            return null;
        }

        public async void Save(UserRequest userRequest)
        {
            var query = "insert into [user](username,password,date_created,date_updated) values(@userName,@password,@date_created,@date_updated)";
            using (var connection = _context.CreateConnection())
            {
                    
                var users = await connection.ExecuteAsync(query, new { 
                    userName=userRequest.UserName,
                    password=userRequest.Password,
                    date_created=DateTime.Now,
                    date_updated=DateTime.Now
                });
            }
        }
    }
}
