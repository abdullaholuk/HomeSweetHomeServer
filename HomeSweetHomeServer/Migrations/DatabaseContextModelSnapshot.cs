﻿// <auto-generated />
using HomeSweetHomeServer.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace HomeSweetHomeServer.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HomeSweetHomeServer.Models.ExpenseModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AuthorId");

                    b.Property<string>("Content");

                    b.Property<double>("Cost");

                    b.Property<int>("EType");

                    b.Property<int?>("HomeId");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("HomeId");

                    b.ToTable("Expense");
                });

            modelBuilder.Entity("HomeSweetHomeServer.Models.FriendshipModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Debt");

                    b.Property<int?>("User1Id");

                    b.Property<int?>("User2Id");

                    b.HasKey("Id");

                    b.HasIndex("User1Id");

                    b.HasIndex("User2Id");

                    b.ToTable("Friendship");
                });

            modelBuilder.Entity("HomeSweetHomeServer.Models.HomeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AdminId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.ToTable("Home");
                });

            modelBuilder.Entity("HomeSweetHomeServer.Models.InformationModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("InformationName");

                    b.Property<string>("InformationType");

                    b.HasKey("Id");

                    b.ToTable("Information");
                });

            modelBuilder.Entity("HomeSweetHomeServer.Models.MealModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Ingredients");

                    b.Property<int?>("MenuId")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.Property<string>("Note");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.ToTable("Meal");
                });

            modelBuilder.Entity("HomeSweetHomeServer.Models.MenuModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int?>("HomeId");

                    b.HasKey("Id");

                    b.HasIndex("HomeId");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("HomeSweetHomeServer.Models.NotepadModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<int?>("HomeId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("HomeId");

                    b.ToTable("Notepad");
                });

            modelBuilder.Entity("HomeSweetHomeServer.Models.ShoppingListModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("HomeId");

                    b.Property<string>("List")
                        .IsRequired();

                    b.Property<string>("Status")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("HomeId");

                    b.ToTable("ShoppingList");
                });

            modelBuilder.Entity("HomeSweetHomeServer.Models.UserExpenseModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ExpenseId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ExpenseId");

                    b.HasIndex("UserId");

                    b.ToTable("UserExpense");
                });

            modelBuilder.Entity("HomeSweetHomeServer.Models.UserInformationModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("InformationId");

                    b.Property<int?>("UserId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("InformationId");

                    b.HasIndex("UserId");

                    b.ToTable("UserInformation");
                });

            modelBuilder.Entity("HomeSweetHomeServer.Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DeviceId");

                    b.Property<int?>("HomeId")
                        .IsRequired();

                    b.Property<string>("Password");

                    b.Property<int>("Position");

                    b.Property<int>("Status");

                    b.Property<string>("Token");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.HasIndex("HomeId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("HomeSweetHomeServer.Models.ExpenseModel", b =>
                {
                    b.HasOne("HomeSweetHomeServer.Models.UserModel", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("HomeSweetHomeServer.Models.HomeModel", "Home")
                        .WithMany()
                        .HasForeignKey("HomeId");
                });

            modelBuilder.Entity("HomeSweetHomeServer.Models.FriendshipModel", b =>
                {
                    b.HasOne("HomeSweetHomeServer.Models.UserModel", "User1")
                        .WithMany()
                        .HasForeignKey("User1Id");

                    b.HasOne("HomeSweetHomeServer.Models.UserModel", "User2")
                        .WithMany()
                        .HasForeignKey("User2Id");
                });

            modelBuilder.Entity("HomeSweetHomeServer.Models.HomeModel", b =>
                {
                    b.HasOne("HomeSweetHomeServer.Models.UserModel", "Admin")
                        .WithMany()
                        .HasForeignKey("AdminId");
                });

            modelBuilder.Entity("HomeSweetHomeServer.Models.MealModel", b =>
                {
                    b.HasOne("HomeSweetHomeServer.Models.MenuModel", "Menu")
                        .WithMany("Meals")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("HomeSweetHomeServer.Models.MenuModel", b =>
                {
                    b.HasOne("HomeSweetHomeServer.Models.HomeModel", "Home")
                        .WithMany()
                        .HasForeignKey("HomeId");
                });

            modelBuilder.Entity("HomeSweetHomeServer.Models.NotepadModel", b =>
                {
                    b.HasOne("HomeSweetHomeServer.Models.HomeModel", "Home")
                        .WithMany()
                        .HasForeignKey("HomeId");
                });

            modelBuilder.Entity("HomeSweetHomeServer.Models.ShoppingListModel", b =>
                {
                    b.HasOne("HomeSweetHomeServer.Models.HomeModel", "Home")
                        .WithMany()
                        .HasForeignKey("HomeId");
                });

            modelBuilder.Entity("HomeSweetHomeServer.Models.UserExpenseModel", b =>
                {
                    b.HasOne("HomeSweetHomeServer.Models.ExpenseModel", "Expense")
                        .WithMany()
                        .HasForeignKey("ExpenseId");

                    b.HasOne("HomeSweetHomeServer.Models.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("HomeSweetHomeServer.Models.UserInformationModel", b =>
                {
                    b.HasOne("HomeSweetHomeServer.Models.InformationModel", "Information")
                        .WithMany()
                        .HasForeignKey("InformationId");

                    b.HasOne("HomeSweetHomeServer.Models.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("HomeSweetHomeServer.Models.UserModel", b =>
                {
                    b.HasOne("HomeSweetHomeServer.Models.HomeModel", "Home")
                        .WithMany("Users")
                        .HasForeignKey("HomeId")
                        .OnDelete(DeleteBehavior.SetNull);
                });
#pragma warning restore 612, 618
        }
    }
}
