using System.ComponentModel.DataAnnotations;

namespace noteapi.Dto
{
    public class NoteRequest
    {
        [Required] 
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Content { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;
    }
}
