namespace AG_RESTful.Models
{
    public class Document
    {
        public int? ID {  get; set; }

        public int userID {  get; set; }

        public string? Type {  get; set; }

        public string? Path {  get; set; }

        public DateTime TimeStamp {  get; set; } = DateTime.Now;

        public List<User>? User {  get; set; }
    }
}
