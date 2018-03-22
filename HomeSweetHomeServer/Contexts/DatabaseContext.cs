using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HomeSweetHomeServer.Models;

namespace HomeSweetHomeServer.Contexts
{
    //All database operations
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        DbSet<AuthenticationModel> AuthenticationModels { get; set; } //User table
        DbSet<InformationModel> InformationModels { get; set; } //Information table
        DbSet<UserInformationModel> UserInformationModels { get; set; } //UserInformation table
        DbSet<FriendshipModel> FriendshipModels { get; set; } //Friendship table
        DbSet<StringModel> StringModels { get; set; } //String table

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Table names
            builder.Entity<AuthenticationModel>().ToTable("User");
            builder.Entity<InformationModel>().ToTable("Information");
            builder.Entity<UserInformationModel>().ToTable("UserInformation");
            builder.Entity<FriendshipModel>().ToTable("Friendship");
            builder.Entity<StringModel>().ToTable("KeyString");
        }
    }
}
