using System.ComponentModel;

namespace AssistantGenesis_Web_App.Models
{
    public partial class User
    {
        public int? Id { get; set; }

        public string? StudentNum { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? Role { get; set; }

        public List<Application>? Applications { get; set; }

        public List<Document>? Documents { get; set; }
    }
}
