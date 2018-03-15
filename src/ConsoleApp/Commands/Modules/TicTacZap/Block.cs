﻿using System;
using System.Threading.Tasks;
using Discord.Commands;
using StupifyConsoleApp.TicTacZapManagement;
using TicTacZap.Blocks;

namespace StupifyConsoleApp.Commands.Modules.TicTacZap
{
    public class Block : StupifyModuleBase
    {
        [Command("AddBlock")]
        public async Task AddBlockCommand(int x, int y, string type)
        {
            var segmentSelectionId = TicTacZapController.GetUserSegmentSelection((await this.GetUserAsync()).UserId);
            if (segmentSelectionId != null)
            {
                await AddBlockCommand((int) segmentSelectionId, x, y, type);
                return;
            }

            await ReplyAsync(Responses.SelectSegmentMessage);
        }

        private async Task AddBlockCommand(int segmentId, int x, int y, string type)
        {
            var blockType = Enum.Parse<BlockType>(type);
            if (await Inventories.RemoveFromInventoryAsync(blockType, 1, (await this.GetUserAsync()).UserId))
            {
                await TicTacZapManagement.Segments.AddBlockAsync(segmentId, x - 1, y - 1, blockType);
                await this.UpdateDbSegmentOutput(segmentId);
                await this.ShowSegmentAsync(segmentId);
                return;
            }

            await ReplyAsync(Responses.ShopAdvisoryMessage);
        }

        [Command("RemoveBlock")]
        public async Task RemoveBlockCommand(int segmentId, int x, int y)
        {
            if (await this.UserHasSegmentAsync(segmentId))
            {
                var blockType = await TicTacZapManagement.Segments.DeleteBlockAsync(segmentId, x - 1, y - 1);

                if (blockType != null)
                    await Inventories.AddToInventoryAsync(blockType.Value, 1, (await this.GetUserAsync()).UserId);
                await this.UpdateDbSegmentOutput(segmentId);
                await this.ShowSegmentAsync(segmentId);
                return;
            }

            await ReplyAsync(Responses.SegmentOwnershipProblem);
        }

        [Command("RemoveBlock")]
        public async Task RemoveBlockCommand(int x, int y)
        {
            var segmentSelectionId = TicTacZapController.GetUserSegmentSelection((await this.GetUserAsync()).UserId);
            if (segmentSelectionId != null)
            {
                await RemoveBlockCommand((int) segmentSelectionId, x, y);
                return;
            }

            await ReplyAsync(Responses.SelectSegmentMessage);
        }
    }
}