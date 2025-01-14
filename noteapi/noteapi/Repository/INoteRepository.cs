using noteapi.Dto;

namespace noteapi.Repository
{
    public interface INoteRepository
    {
        Task Save(NoteRequest noteRequest);
        Task Update(string id, NoteRequest noteRequest);
        Task Delete(string id);
        Task<IEnumerable<NoteResponse>> GetAll();
        Task<NoteResponse> GetOne(string id);
    }
}
