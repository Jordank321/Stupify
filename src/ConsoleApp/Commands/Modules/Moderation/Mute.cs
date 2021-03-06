﻿using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Stupify.Data.Repositories;
using StupifyConsoleApp.Commands.Conditions;

namespace StupifyConsoleApp.Commands.Modules.Moderation
{
    [RequireUserPermission(ChannelPermission.ManageMessages)]
    public class Mute : ModuleBase<CommandContext>
    {
        private readonly IUserRepository _userRepository;

        public Mute(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [Command("Mute")]
        [Moderator]
        public async Task MuteAsync(IGuildUser user)
        {
            await _userRepository.MuteAsync(user).ConfigureAwait(false);
            await ReplyAsync($"{user.Username} is now muted").ConfigureAwait(false);
        }

        [Command("UnMute")]
        [Moderator]
        public async Task UnMuteAsync(IGuildUser user)
        {
            string message;
            if (await _userRepository.UnMuteAsync(user).ConfigureAwait(false))
            {
                message = $"{user.Username} is no longer muted!";
            }
            else
            {
                message = $"{user.Username} isn't muted!";
            }

            await ReplyAsync(message).ConfigureAwait(false);
        }
    }
}