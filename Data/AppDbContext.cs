
using DatingApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    //public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    //{
       public DbSet<AppUser> Users { get; set; }
    // }



}

