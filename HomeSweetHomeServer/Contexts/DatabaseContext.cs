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
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options){ }

        //DbSet<UserModel> UserModels { get; set; } //User table
        DbSet<AuthenticationModel> AuthenticationModels { get; set; } //User table
        DbSet<InformationModel> InformationModels { get; set; } //Information table
        DbSet<UserInformationModel> UserInformationModels { get; set; } //UserInformation table
        DbSet<FriendshipModel> FriendshipModels { get; set; } //Friendship table
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Table names 
            builder.Entity<UserModel>().ToTable("User");
            builder.Entity<InformationModel>().ToTable("Information");
            builder.Entity<UserInformationModel>().ToTable("UserInformation");
            builder.Entity<FriendshipModel>().ToTable("Friendship");
            
        }
    }
}
