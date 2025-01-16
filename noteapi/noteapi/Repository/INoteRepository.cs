using noteapi.Dto;

namespace noteapi.Repository
{
    public interface INoteRepository
    {
        Task Save(NoteRequest noteRequest,string userId);
        Task Update(string id, NoteRequest noteRequest);
        Task Delete(string id);
        Task<IEnumerable<NoteResponse>> GetAll(string userId);
        Task<IEnumerable<NoteResponse>> Search(string userId,string title);
        Task<NoteResponse> GetOne(string id);
    }
}
