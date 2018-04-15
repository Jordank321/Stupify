﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using Stupify.Data.Repositories;
using TwitchApi;

namespace StupifyConsoleApp.Commands.Modules
{
    public class Twitch : ModuleBase<CommandContext>
    {
        private readonly TwitchClient _twitchClient;
        private readonly ITwitchRepository _twitchRepository;

        public Twitch(TwitchClient twitchClient, ITwitchRepository twitchRepository)
        {
            _twitchClient = twitchClient;
            _twitchRepository = twitchRepository;
        }

        [Command("AreTheyLive")]
        public async Task AreTheyLiveAsync(string username)
        {
            var message = await _twitchClient.IsStreamingAsync(username).ConfigureAwait(false) ? "Yup, they are" : "Uhhh, no?";
            await ReplyAsync(message).ConfigureAwait(false);
        }

        [RequireUserPermission(GuildPermission.ManageGuild)]
        [Command("SetUpdateChannel")]
        public async Task SetUpdateChannelAsync()
        {
            await _twitchRepository.SetUpdateChannelAsync(Context.Channel as ITextChannel).ConfigureAwait(false);
            await ReplyAsync("This channel will now be used to send twitch updates!").ConfigureAwait(false);
        }

        [RequireUserPermission(GuildPermission.ManageGuild)]
        [Command("AddChannelUpdate")]
        public async Task AddTwitchWatchAsync(string twitchLoginName)
        {
            if (await _twitchClient.GetTwitchUserAsync(twitchLoginName).ConfigureAwait(false) == null)
            {
                await ReplyAsync("This user could not be found").ConfigureAwait(false);
                return;
            }
            await _twitchRepository.AddTwitchChannelWatchAsync(Context.Guild, twitchLoginName).ConfigureAwait(false);
            await ReplyAsync("This user has been added!").ConfigureAwait(false);
        }
    }
}
