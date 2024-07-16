using System.ComponentModel;

namespace RESTful_Web_API.Models
{
    public partial class User
    {
        public int? UserId { get; set; }

        public string? StudentNum { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? Role { get; set; }

        
    }
}
