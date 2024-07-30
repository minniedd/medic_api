using medic_api.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Reflection;

namespace medic_api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
    }
}
