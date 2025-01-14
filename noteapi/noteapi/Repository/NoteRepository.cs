using Dapper;
using noteapi.Dto;
using noteapi.Models;

namespace noteapi.Repository
{
    public class NoteRepository : INoteRepository
    {
        private readonly DapperContext _context;
        public NoteRepository(DapperContext context) {
            _context = context;
        }
        public async Task Delete(string id)
        {
            var query = "DELETE FROM note WHERE id=@Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    Id = id
                });
            }
        }

        public async Task<IEnumerable<NoteResponse>> GetAll(string userId)
        {
            var query = "SELECT * FROM note WHERE userId=@UserId";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<NoteResponse>(query, new
                {
                    UserId = userId
                });
                var notes = result.ToList();
                return notes;
            }
        }

        public async Task<NoteResponse> GetOne(string id)
        {
            var query = "SELECT * FROM note WHERE id=@Id";
            using (var connection = _context.CreateConnection())
            {
                var note = await connection.QueryFirstAsync<NoteResponse>(query, new
                {
                    Id = id
                });
                return note;
            }
        }

        public async Task Save(NoteRequest noteRequest)
        {
            var query = "INSERT INTO note (id,title,content,date_created,date_updated,userId) values(@id,@title,@content,@date_created,@date_updated,@userId)";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    id=Guid.NewGuid().ToString(),
                    title = noteRequest.Title,
                    content = noteRequest.Content,
                    date_created=DateTime.Now,
                    date_updated = DateTime.Now,
                    userId=noteRequest.UserId,
                });
            }
        }

        public async Task<IEnumerable<NoteResponse>> Search(string userId,string title)
        {
            //var query = "SELECT * FROM note WHERE userId=@UserId and (title like '%@Title%')";
            var query = "SELECT * FROM note WHERE userId=@UserId and title like @Title";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<NoteResponse>(query, new
                {
                    UserId = userId,
                    Title = "%"+title+"%"
                });
                var notes = result.ToList();
                return notes;
            }
        }

        public async Task Update(string id, NoteRequest noteRequest)
        {
            var query = "UPDATE note SET title=@title, content=@content,date_updated=@date_updated WHERE id=@Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    Id = id,
                    title=noteRequest.Title,
                    content=noteRequest.Content,
                    date_updated=DateTime.Now
                });
            }
        }
    }
}
