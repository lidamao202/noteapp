using System.ComponentModel.DataAnnotations;

namespace noteapi.Dto
{
    public class NoteRequest
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title length can't be more than 100.")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public required string Content { get; set; }

        public required string UserId { get; set; }
    }
}
