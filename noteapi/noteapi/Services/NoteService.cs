using noteapi.Dto;
using noteapi.Models;
using noteapi.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace noteapi.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<IEnumerable<NoteResponse>> GetAllNotes(string userId)
        {
            return await _noteRepository.GetAll(userId);
        }

        public async Task<IEnumerable<NoteResponse>> SearchNotes(string userId, string title)
        {
            return await _noteRepository.Search(userId, title);
        }

        public async Task<NoteResponse> GetNoteById(string id)
        {
            return await _noteRepository.GetOne(id);
        }

        public async Task CreateNote(NoteRequest noteRequest,string userId)
        {
            await _noteRepository.Save(noteRequest, userId);
        }

        public async Task UpdateNote(string id, NoteRequest noteRequest)
        {
            await _noteRepository.Update(id, noteRequest);
        }

        public async Task DeleteNote(string id)
        {
            await _noteRepository.Delete(id);
        }


    }
}
