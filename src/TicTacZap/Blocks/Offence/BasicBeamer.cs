﻿using System;

namespace TicTacZap.Blocks.Offence
{
    class BasicBeamer : BaseBeamer
    {
        public BasicBeamer(int x, int y, Func<int,int,Direction,Segment,(int x, int y)?> targetLocator = null) : base(x, y, targetLocator)
        {
            BlockType = BlockType.Beamer;
            Upkeep = 0;
            MaxHealth = 50;
            Health = MaxHealth;
            EnergyConsumption = 10;
            BeamPower = 5;
        }
    }
}
