using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using noteapi.Dto;
using noteapi.Models;
using noteapi.Repository;
using System.IdentityModel.Tokens.Jwt;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace noteapi.Controllers
{
    [Route("api/note")]
    [ApiController,Authorize]
    public class NoteController : ControllerBase
    {
        private readonly INoteRepository _noteRepository;
        private readonly ILogger<NoteController> _logger;

        public NoteController(INoteRepository noteRepository, ILogger<NoteController> logger)
        {
            _noteRepository = noteRepository;
            _logger = logger;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("GetAll method called.");

            if (HttpContext.Items["JwtClaims"] is not Dictionary<string, string> claims)
            {
                _logger.LogWarning("No valid token found.");
                return Unauthorized(new { Message = "No valid token found" });
            }

            var userId = claims.GetValueOrDefault("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("UserId is null or empty.");
                return Unauthorized(new { Message = "Invalid UserId" });
            }

            var list = await _noteRepository.GetAll(userId);
            _logger.LogInformation("GetAll method completed successfully.");
            return Ok(list);
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Search(string title = "")
        {
            _logger.LogInformation("Search method called with title: {Title}", title);

            if (HttpContext.Items["JwtClaims"] is not Dictionary<string, string> claims)
            {
                _logger.LogWarning("No valid token found.");
                return Unauthorized(new { Message = "No valid token found" });
            }

            var userId = claims.GetValueOrDefault("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("UserId is null or empty.");
                return Unauthorized(new { Message = "Invalid UserId" });
            }

            var list = await _noteRepository.Search(userId, title);
            _logger.LogInformation("Search method completed successfully.");
            return Ok(list);
        }

        [HttpGet]
        [Route("getOne/{id}")]
        public async Task<IActionResult> GetOne(string id)
        {
            _logger.LogInformation("GetOne method called with id: {Id}", id);

            var note = await _noteRepository.GetOne(id);
            _logger.LogInformation("GetOne method completed successfully.");
            return Ok(note);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NoteRequest noteRequest)
        {
            _logger.LogInformation("Post method called.");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state.");
                return BadRequest(ModelState);
            }
            
            if (HttpContext.Items["JwtClaims"] is not Dictionary<string, string> claims)
            {
                _logger.LogWarning("No valid token found.");
                return Unauthorized(new { Message = "No valid token found" });
            }

            var userId = claims.GetValueOrDefault("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("UserId is null or empty.");
                return Unauthorized(new { Message = "Invalid UserId" });
            }

            await _noteRepository.Save(noteRequest,userId);
            _logger.LogInformation("Post method completed successfully.");
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] NoteRequest noteRequest)
        {
            _logger.LogInformation("Put method called with id: {Id}", id);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state.");
                return BadRequest(ModelState);
            }

            await _noteRepository.Update(id, noteRequest);
            _logger.LogInformation("Put method completed successfully.");
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInformation("Delete method called with id: {Id}", id);

            await _noteRepository.Delete(id);
            _logger.LogInformation("Delete method completed successfully.");
            return Ok();
        }

        private IActionResult ValidateToken()
        {
            if (HttpContext.Items["JwtClaims"] is not Dictionary<string, string> claims)
            {
                _logger.LogWarning("No valid token found.");
                return Unauthorized(new { Message = "No valid token found" });
            }

            var userId = claims.GetValueOrDefault("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("UserId is null or empty.");
                return Unauthorized(new { Message = "Invalid UserId" });
            }

            return Ok(userId);
        }
    }
}
