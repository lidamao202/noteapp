using Microsoft.AspNetCore.Mvc;
using noteapi.Dto;
using noteapi.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace noteapi.Controllers
{
    [Route("api/note")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteRepository _noteRepository;
        public NoteController(INoteRepository noteRepository) {
            _noteRepository = noteRepository;
        }


        [HttpGet]
        [Route("getAll/{userId}")]
        public async Task<IActionResult> GetAll(string userId)
        {
            try
            {
                var list = await _noteRepository.GetAll(userId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Search(string userId, string title="")
        {
            try
            {
                var list = await _noteRepository.Search(userId, title);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("getOne/{id}")]
        public async Task<IActionResult> GetOne(string id)
        {
            try
            {
                var note = await _noteRepository.GetOne(id);
                return Ok(note);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NoteRequest noteRequest)
        {
            try
            {
                await _noteRepository.Save(noteRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<NoteController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] NoteRequest noteRequest)
        {
            try
            {
                await _noteRepository.Update(id, noteRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _noteRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
