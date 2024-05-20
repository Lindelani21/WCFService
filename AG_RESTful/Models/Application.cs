namespace AG_RESTful.Models
{
    public class Application
    {
        public int? ApplicationId { get; set; }

        public string? Role { get; set; }

        public string? Module { get; set; }

        public string? Transcript { get; set; }

        public string? Status { get; set; }

        public int Id { get; set; }

        public List<User>? User { get; set; }
    }
}
