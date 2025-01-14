using Dapper;
using noteapi.Dto;
using noteapi.Models;

namespace noteapi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;
        
        public UserRepository(DapperContext context,IConfiguration configuration) { 
            _context = context; 
            
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

        public async void Save(UserRequest userRequest)
        {
            var query = "insert into [user](id,username,password,date_created,date_updated) values(@Id,@userName,@password,@date_created,@date_updated)";
            using (var connection = _context.CreateConnection())
            {
                    
                var users = await connection.ExecuteAsync(query, new { 
                    Id=Guid.NewGuid().ToString(),
                    userName=userRequest.UserName,
                    password=userRequest.Password,
                    date_created=DateTime.Now,
                    date_updated=DateTime.Now
                });
            }
        }

    }
}
