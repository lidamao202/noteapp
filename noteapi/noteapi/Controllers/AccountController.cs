using Microsoft.AspNetCore.Mvc;
using noteapi.Dto;
using noteapi.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace noteapi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _user;
        public AccountController(IUserRepository user) {
            _user = user;
        
        }
        [HttpGet]
        [Route("login")]
        public async Task<IActionResult> Login(string username,string password)
        {
            try
            {
                var result = await _user.Get(username);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Post([FromBody] UserRequest userRequest)
        {
            try
            {
                _user.Save(userRequest);

                return StatusCode(201, "Create Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        //// GET: api/<Account>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<Account>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}




        //// PUT api/<Account>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<Account>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
