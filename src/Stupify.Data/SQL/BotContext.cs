﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Stupify.Data.SQL.Models;

namespace Stupify.Data.SQL
{
    internal class BotContext : DbContext
    {
        public BotContext(DbContextOptions<BotContext> options) : base(options)
        {
        }

        public DbSet<Server> Servers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ServerUser> ServerUsers { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<ServerStory> ServerStories { get; set; }
        public DbSet<ServerStoryPart> ServerStoryParts { get; set; }
        public DbSet<Segment> Segments { get; set; }
        public DbSet<SegmentTemplate> SegmentTemplates { get; set; }
        public DbSet<ServerTwitchChannel> ServerTwitchChannels { get; set; }
        public DbSet<CustomCommand> CustomCommands { get; set; }
        public DbSet<ServerSettings> ServerSettings { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
        public DbSet<ExternalAuthentication> ExternalAuthentication { get; set; }
        public DbSet<ServerCustomText> ServerCustomTexts { get; set; }
    }

    internal class BotContextDesign : IDesignTimeDbContextFactory<BotContext>
    {
        public BotContext CreateDbContext(string[] args)
        {
            return new BotContext(new DbContextOptionsBuilder<BotContext>().UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StupifyLocalDev;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False").Options);
        }
    }
}