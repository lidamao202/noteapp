using System.ComponentModel.DataAnnotations;

namespace noteapi.Dto
{
    public class UserRequest
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, ErrorMessage = "Username length can't be more than 50.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password length must be between 6 and 100.")]
        public string Password { get; set; }
    }
}
