using System.ComponentModel.DataAnnotations;

namespace noteapi.Dto
{
    public class UserRequest
    {
        [Required]
        public string UserName { get; set; }=string.Empty;
        [Required]
        public string Password { get; set; }=string.Empty ;
    }
}
