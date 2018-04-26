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
    [Migration("20180419220338_InformationTable_v1")]
    partial class InformationTable_v1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("HomeSweetHomeServer.Models.InformationModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("InformationName");

                    b.Property<string>("InformationType");

                    b.HasKey("Id");

                    b.ToTable("Information");
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

                    b.Property<bool>("IsVerifiedByEmail");

                    b.Property<string>("Password");

                    b.Property<string>("Token");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("User");
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

            modelBuilder.Entity("HomeSweetHomeServer.Models.UserInformationModel", b =>
                {
                    b.HasOne("HomeSweetHomeServer.Models.InformationModel", "Information")
                        .WithMany()
                        .HasForeignKey("InformationId");

                    b.HasOne("HomeSweetHomeServer.Models.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}