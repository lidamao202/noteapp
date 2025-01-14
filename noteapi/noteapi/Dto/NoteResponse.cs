namespace noteapi.Dto
{
    public class NoteResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Date_Created { get; set; }
        public DateTime Date_Updated { get; set; }
        public string userId { get; set; } = string.Empty;
    }
}
