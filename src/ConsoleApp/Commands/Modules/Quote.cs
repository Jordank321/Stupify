﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Microsoft.EntityFrameworkCore;
using StupifyConsoleApp.Client;
using StupifyConsoleApp.DataModels;

namespace StupifyConsoleApp.Commands.Modules
{
    public class Quote : StupifyModuleBase
    {
        [Command("AddQuote")]
        public async Task AddQuoteAsync([Remainder] string quoteBody)
        {
            using (var db = new BotContext())
            {
                await db.Quotes.AddAsync(new DataModels.Quote
                {
                    QuoteBody = quoteBody,
                    ServerUser = await db.GetServerUserAsync((long) Context.User.Id, (long) Context.Guild.Id)
                });
                await db.SaveChangesAsync();
                var reply = ReplyAsync("Done!");
                var log = ClientManager.LogAsync("The following quote has been added!: " + quoteBody);
                await Task.WhenAll(reply, log);
            }
        }

        [Command("RandomQuote")]
        public async Task RandomQuoteAsync()
        {
            using (var db = new BotContext())
            {
                var quote = await db
                    .Quotes
                    .Include(q => q.ServerUser.User)
                    .Include(q => q.ServerUser.Server)
                    .Where(q => (ulong) q.ServerUser.Server.DiscordGuildId == Context.Guild.Id)
                    .OrderBy(r => Guid.NewGuid())
                    .FirstOrDefaultAsync();
                if (quote == null)
                {
                    await ReplyAsync("No quotes were found, try !addquote <quote>");
                    return;
                }

                var message = quote.QuoteBody + " - " + ClientManager.Client.UsernameFromServerUser(quote.ServerUser);
                await ReplyAsync(message);
            }
        }
    }
}