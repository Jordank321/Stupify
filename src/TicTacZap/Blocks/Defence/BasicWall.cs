﻿namespace TicTacZap.Blocks.Defence
{
    public class BasicWall : BaseBlock
    {

        public BasicWall()
        {
            BlockType = BlockType.BasicWall;
            MaxHealth = 200;
            Health = MaxHealth;
            Upkeep = 0;
        }
    }
}
