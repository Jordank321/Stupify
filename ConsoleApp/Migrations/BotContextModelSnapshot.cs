﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using StupifyConsoleApp.DataModels;
using System;

namespace StupifyConsoleApp.Migrations
{
    [DbContext(typeof(BotContext))]
    partial class BotContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StupifyConsoleApp.DataModels.Quote", b =>
                {
                    b.Property<int>("QuoteId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("QuoteBody");

                    b.Property<int>("ServerUserId");

                    b.HasKey("QuoteId");

                    b.HasIndex("ServerUserId");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("StupifyConsoleApp.DataModels.Server", b =>
                {
                    b.Property<int>("ServerId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("DiscordGuildId");

                    b.HasKey("ServerId");

                    b.ToTable("Servers");
                });

            modelBuilder.Entity("StupifyConsoleApp.DataModels.ServerUser", b =>
                {
                    b.Property<int>("ServerUserId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ServerId");

                    b.Property<int>("UserId");

                    b.HasKey("ServerUserId");

                    b.HasIndex("ServerId");

                    b.HasIndex("UserId");

                    b.ToTable("ServerUsers");
                });

            modelBuilder.Entity("StupifyConsoleApp.DataModels.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("DiscordUserId");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("StupifyConsoleApp.DataModels.Quote", b =>
                {
                    b.HasOne("StupifyConsoleApp.DataModels.ServerUser", "ServerUser")
                        .WithMany("Quotes")
                        .HasForeignKey("ServerUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StupifyConsoleApp.DataModels.ServerUser", b =>
                {
                    b.HasOne("StupifyConsoleApp.DataModels.Server", "Server")
                        .WithMany("ServerUsers")
                        .HasForeignKey("ServerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StupifyConsoleApp.DataModels.User", "User")
                        .WithMany("ServerUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
