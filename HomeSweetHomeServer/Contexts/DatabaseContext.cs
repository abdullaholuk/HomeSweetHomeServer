using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HomeSweetHomeServer.Models;

namespace HomeSweetHomeServer.Contexts
{
    //All database operations
    public class DatabaseContext : DbContext , IDisposable
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options){ }

        //DbSet<UserModel> UserModels { get; set; } //User table
        DbSet<UserModel> UserModels { get; set; } //User table
        DbSet<InformationModel> InformationModels { get; set; } //Information table
        DbSet<UserInformationModel> UserInformationModels { get; set; } //UserInformation table
        DbSet<FriendshipModel> FriendshipModels { get; set; } //Friendship table
        DbSet<HomeModel> HomeModels { get; set; } //Home table
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Table names 
            builder.Entity<UserModel>().ToTable("User").HasOne(u => u.Home).WithMany(h => h.Users);
            builder.Entity<InformationModel>().ToTable("Information");
            builder.Entity<UserInformationModel>().ToTable("UserInformation");
            builder.Entity<FriendshipModel>().ToTable("Friendship");
            builder.Entity<HomeModel>().ToTable("Home").HasMany(h => h.Users).WithOne(u => u.Home).IsRequired();
        }
    }
}
