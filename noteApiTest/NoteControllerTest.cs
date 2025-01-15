using Microsoft.AspNetCore.Mvc;
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
        public NoteControllerTest()
        {
            _noteRepository = new Mock<INoteRepository>();
            _noteController = new NoteController(_noteRepository.Object);
        }
        [Fact]
        public async void TestGetAllNotes()
        {
           
            List<NoteResponse> dataList = new List<NoteResponse>()
            {
                    new NoteResponse()
                {
                    Id="1",
                    Title="Artiec",
                    Content="The coldest place in the world",
                    userId="1",
                },
                new NoteResponse()
                {
                    Id="2",
                    Title="School",
                    Content="The place we go to study sth",
                    userId="1",
                }
            };
            IEnumerable<NoteResponse> dataEnumerable = dataList;
            Task<IEnumerable<NoteResponse>> task = Task.FromResult(dataEnumerable);
            var taskList = new List<Task<IEnumerable<NoteResponse>>>() { task };


            _noteRepository.Setup(s => s.GetAll("1")).Returns(task);
            var result = await _noteController.GetAll("1");
            
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var NoteResponseData = Assert.IsType<List<NoteResponse>>(actionResult.Value);
            Assert.Equal(dataEnumerable.ElementAt(0).Title, NoteResponseData.ElementAt(0).Title);
        }

        [Fact]
        public async void TestSearchNote()
        {

            List<NoteResponse> dataList = new List<NoteResponse>()
            {
                    new NoteResponse()
                {
                    Id="1",
                    Title="Artiec",
                    Content="The coldest place in the world",
                    userId="1",
                },
                new NoteResponse()
                {
                    Id="2",
                    Title="School",
                    Content="The place we go to study sth",
                    userId="1",
                }
            };
            IEnumerable<NoteResponse> dataEnumerable = dataList;
            Task<IEnumerable<NoteResponse>> task = Task.FromResult(dataEnumerable);
            var taskList = new List<Task<IEnumerable<NoteResponse>>>() { task };


            _noteRepository.Setup(s => s.Search("1","o")).Returns(task);
            var result = await _noteController.Search("1","o");

            var actionResult = Assert.IsType<OkObjectResult>(result);
            var NoteResponseData = Assert.IsType<List<NoteResponse>>(actionResult.Value);
            Assert.Equal(dataEnumerable.ElementAt(0).Content, NoteResponseData.ElementAt(0).Content);
        }

        [Fact]
        public async void TestGetOneNote()
        {

            List<NoteResponse> dataList = new List<NoteResponse>()
            {
                    new NoteResponse()
                {
                    Id="1",
                    Title="Artiec",
                    Content="The coldest place in the world",
                    userId="1",
                },
                new NoteResponse()
                {
                    Id="2",
                    Title="School",
                    Content="The place we go to study sth",
                    userId="1",
                }
            };
            NoteResponse dataEnumerable = dataList[0];
            Task<NoteResponse> task = Task.FromResult(dataEnumerable);


            _noteRepository.Setup(s => s.GetOne("1")).Returns(task);
            var result = await _noteController.Search("1", "o");

            var actionResult = Assert.IsType<OkObjectResult>(result);
            var NoteResponseData = Assert.IsType<NoteResponse[]>(actionResult.Value);
            Assert.Equal(dataEnumerable.Content, NoteResponseData[0].Content);
        }

        [Fact]
        public async void TestPostNote()
        {

   
            var noteRequest = new NoteRequest()
            {
                Title = "School",
                Content = "The place we go to study sth",
                UserId = "1",
            };



            _noteRepository.Setup(s => s.Save(noteRequest)).Returns(Task.CompletedTask);
            var result = await _noteController.Post(noteRequest);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async void TestPutNote()
        {
            List<NoteResponse> dataList = new List<NoteResponse>()
            {
                    new NoteResponse()
                {
                    Id="1",
                    Title="Artiec",
                    Content="The coldest place in the world",
                    userId="1",
                },
                new NoteResponse()
                {
                    Id="2",
                    Title="Update School",
                    Content="The place we go to study sth",
                    userId="1",
                }
            };
            NoteResponse dataEnumerable = dataList[0];
            Task<NoteResponse> task = Task.FromResult(dataEnumerable);

            var noteRequest = new NoteRequest()
            {
                
                Title = "Update School",
                Content = "The place we go to study sth",
                UserId = "1",
            };



            _noteRepository.Setup(s => s.Update("1",noteRequest)).Returns(Task.CompletedTask);
            var result = await _noteController.Put("1",noteRequest);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async void TestDeleteNote()
        {
            List<NoteResponse> dataList = new List<NoteResponse>()
            {
                    new NoteResponse()
                {
                    Id="1",
                    Title="Artiec",
                    Content="The coldest place in the world",
                    userId="1",
                },
                new NoteResponse()
                {
                    Id="2",
                    Title="Update School",
                    Content="The place we go to study sth",
                    userId="1",
                }
            };
            NoteResponse dataEnumerable = dataList[0];
            Task<NoteResponse> task = Task.FromResult(dataEnumerable);

            _noteRepository.Setup(s => s.Delete("1")).Returns(Task.CompletedTask);
            var result = await _noteController.Delete("1");

            Assert.IsType<OkResult>(result);
        }
    }
}