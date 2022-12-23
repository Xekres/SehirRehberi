using Microsoft.EntityFrameworkCore;
using SehirRehberiWebApi.Models;

namespace SehirRehberiWebApi.Data
{
    public class SehirRehberiContext:DbContext
    {
        public SehirRehberiContext(DbContextOptions<SehirRehberiContext> options):base(options)
        {

        }
        public DbSet<City>? Cities { get; set; }
        public DbSet<Photo>? Photos { get; set; }
        public DbSet<User>? Users { get; set; }
    }
}
