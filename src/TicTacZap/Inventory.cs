﻿using System;
using System.Collections.Generic;
using TicTacZap.Segment.Blocks;

namespace TicTacZap
{
    public class Inventory
    {
        public Dictionary<BlockType, int> Blocks { get; set; } = new Dictionary<BlockType, int>();

        public Inventory(int startingBlocks)
        {
            AddBlocks(BlockType.BasicEnergy, startingBlocks);
        }

        public void Reset()
        {
            Blocks = new Dictionary<BlockType, int>();
        }

        public string TextRender()
        {
            if (Blocks.Count == 0) return "";

            var str = "Inventory:" + Environment.NewLine;

            foreach (var block in Blocks)
            {
                str += $"`{block.Key} block x{block.Value}`";
            }

            return str;
        }

        public void AddBlocks(BlockType blockType, int quantity)
        {
            if (!Blocks.ContainsKey(blockType))
            {
                Blocks.Add(blockType, quantity);
                return;
            }

            Blocks[blockType] = Blocks[blockType] + quantity;
        }

        public bool RemoveBlocks(BlockType blockType, int quantity)
        {
            if (!Blocks.ContainsKey(blockType) || Blocks[blockType] < quantity) return false;
            Blocks[blockType] -= quantity;
            return true;
        }
    }
}