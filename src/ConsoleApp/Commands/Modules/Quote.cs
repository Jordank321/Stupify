﻿using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Microsoft.Extensions.Logging;

using Stupify.Data.Models;
using Stupify.Data.Repositories;

namespace StupifyConsoleApp.Commands.Modules
{
    public class Quotes : ModuleBase<CommandContext>
    {
        private readonly ILogger<Quote> _logger;
        private readonly IQuoteRepository _quoteRepository;

        public Quotes(ILogger<Quote> logger, IQuoteRepository quoteRepository)
        {
            _logger = logger;
            _quoteRepository = quoteRepository;
        }

        [Command("AddQuote")]
        public async Task AddQuoteAsync([Remainder]string quoteBody)
        {
            if (Context.User is IGuildUser guildUser)
            {
                await _quoteRepository.AddQuoteAsync(new Quote
                {
                    Author = guildUser,
                    Content = quoteBody
                }, Context.Guild).ConfigureAwait(false);
                _logger.LogInformation("Added quote from {User} in {Guild}", Context.User.Username, Context.Guild.Name);
                await ReplyAsync("Done!").ConfigureAwait(false);
            }
        }

        [Command("RandomQuote")]
        public async Task RandomQuoteAsync()
        {
            var quote = await _quoteRepository.RandomQuoteAsync(Context.Guild).ConfigureAwait(false);

            if (quote == null)
            {
                await ReplyAsync("No quotes were found, try !addquote <quote>").ConfigureAwait(false);
                return;
            }

            var message = $"{quote.Content} - {quote.Author.Username.Replace("*", "\\*")}";
            await ReplyAsync(message).ConfigureAwait(false);
        }
    }
}