using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using noteapi.Controllers;
using noteapi.Dto;
using noteapi.Repository;
using System.Xml.Linq;

namespace noteApiTest
{
    public class NoteControllerTest
    {
        private readonly NoteController _noteController;
        private readonly Mock<INoteRepository> _noteRepository;
        private readonly Mock<ILogger<NoteController>> _logger;

        public NoteControllerTest()
        {
            _noteRepository = new Mock<INoteRepository>();
            _logger = new Mock<ILogger<NoteController>>();
            _noteController = new NoteController(_noteRepository.Object, _logger.Object);

            // Set up HttpContext with JWT claims
            var httpContext = new DefaultHttpContext();
            var claims = new Dictionary<string, string>
                {
                    { "UserId", "1" }
                };
            httpContext.Items["JwtClaims"] = claims;
            _noteController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        [Fact]
        public async Task TestGetAllNotes_Success()
        {
            // Arrange
            var userId = "1";
            var notes = new List<NoteResponse>
                {
                    new NoteResponse { Id = "1", Title = "Note 1", Content = "Content 1", userId = userId },
                    new NoteResponse { Id = "2", Title = "Note 2", Content = "Content 2", userId = userId }
                };

            _noteRepository.Setup(repo => repo.GetAll(userId)).ReturnsAsync(notes);

            // Act
            var result = await _noteController.GetAll();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnedNotes = Assert.IsType<List<NoteResponse>>(actionResult.Value);
            Assert.Equal(notes.Count, returnedNotes.Count);
        }

        [Fact]
        public async Task TestSearchNotes_Success()
        {
            // Arrange
            var userId = "1";
            var title = "Note";
            var notes = new List<NoteResponse>
                {
                    new NoteResponse { Id = "1", Title = "Note 1", Content = "Content 1", userId = userId },
                    new NoteResponse { Id = "2", Title = "Note 2", Content = "Content 2", userId = userId }
                };

            _noteRepository.Setup(repo => repo.Search(userId, title)).ReturnsAsync(notes);

            // Act
            var result = await _noteController.Search(title);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnedNotes = Assert.IsType<List<NoteResponse>>(actionResult.Value);
            Assert.Equal(notes.Count, returnedNotes.Count);
        }

        [Fact]
        public async Task TestGetOneNote_Success()
        {
            // Arrange
            var noteId = "1";
            var note = new NoteResponse { Id = noteId, Title = "Note 1", Content = "Content 1", userId = "1" };

            _noteRepository.Setup(repo => repo.GetOne(noteId)).ReturnsAsync(note);

            // Act
            var result = await _noteController.GetOne(noteId);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnedNote = Assert.IsType<NoteResponse>(actionResult.Value);
            Assert.Equal(noteId, returnedNote.Id);
        }

        [Fact]
        public async Task TestPostNote_Success()
        {
            // Arrange
            var noteRequest = new NoteRequest { Title = "New Note", Content = "New Content", UserId = "1" };

            _noteRepository.Setup(repo => repo.Save(noteRequest)).Returns(Task.CompletedTask);

            // Act
            var result = await _noteController.Post(noteRequest);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task TestPutNote_Success()
        {
            // Arrange
            var noteId = "1";
            var noteRequest = new NoteRequest { Title = "Updated Note", Content = "Updated Content", UserId = "1" };

            _noteRepository.Setup(repo => repo.Update(noteId, noteRequest)).Returns(Task.CompletedTask);

            // Act
            var result = await _noteController.Put(noteId, noteRequest);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task TestDeleteNote_Success()
        {
            // Arrange
            var noteId = "1";

            _noteRepository.Setup(repo => repo.Delete(noteId)).Returns(Task.CompletedTask);

            // Act
            var result = await _noteController.Delete(noteId);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}