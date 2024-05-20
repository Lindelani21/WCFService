using Microsoft.EntityFrameworkCore;

namespace AG_RESTful.Models
{
    public class AssistantGenesisDataContext:DbContext
    {
        public AssistantGenesisDataContext(DbContextOptions options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Application> Application { get; set; }
        public DbSet<Document> Document { get; set; }
    }
}
