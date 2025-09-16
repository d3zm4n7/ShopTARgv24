using Microsoft.EntityFrameworkCore;
using ShopTARgv24.Core.Domain;


namespace ShopTARgv24.Data
{
    public class ShopTARgv24Context : DbContext
    {
        public ShopTARgv24Context(DbContextOptions<ShopTARgv24Context> options) 
        : base(options) { }

        public DbSet<Spaceship> Spaceships { get; set; }
        public DbSet<FileToApi> FileToApis { get; set; }
    }
}
