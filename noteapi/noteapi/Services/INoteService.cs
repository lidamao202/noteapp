using noteapi.Dto;
namespace noteapi.Services
{
    public interface INoteService
    {
        Task<IEnumerable<NoteResponse>> GetAllNotes(string userId);
        Task<IEnumerable<NoteResponse>> SearchNotes(string userId, string title);
        Task<NoteResponse> GetNoteById(string id);
        Task CreateNote(NoteRequest noteRequest);
        Task UpdateNote(string id, NoteRequest noteRequest);
        Task DeleteNote(string id);
    }
}
