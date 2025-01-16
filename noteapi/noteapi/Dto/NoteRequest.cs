using System.ComponentModel.DataAnnotations;

namespace noteapi.Dto
{
    public class NoteRequest
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title length can't be more than 100.")]
        public required string Title { get; set; }

        public string Content { get; set; } = string.Empty;
    }
}
