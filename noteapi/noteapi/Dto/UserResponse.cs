namespace noteapi.Dto
{
    public class UserResponse
    {
        public string Id { get; set; }=string.Empty;
        public string UserName { get; set; }= string.Empty;
        public DateTime Date_Created { get; set; }
        public DateTime Date_Updated { get; set; }

    }
}
