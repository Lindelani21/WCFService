using Microsoft.EntityFrameworkCore;
using RESTful_Web_API.Models;

namespace RESTful_Web_API.Utilities
{
    public class AssistantGenesisDataContext : DbContext
    {
        public DbSet<User> User { get; set; }
    }
}
