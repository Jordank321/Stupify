﻿using System;
using System.Collections.Generic;
using TicTacZap.Blocks;
using TicTacZap.Blocks.Defence;
using TicTacZap.Blocks.Offence;
using TicTacZap.Blocks.Production.Energy;

namespace TicTacZap
{
    public static class TicTacZapExtensions
    {
        public static decimal ResourcePerTick(this Segment segment, Resource resource)
        {
            return segment.ResourceOutput.ContainsKey(resource) ? segment.ResourceOutput[resource] : 0;
        }

        public static void SetResources(this Segment segment, Dictionary<Resource, decimal> resources)
        {
            segment.ResourceOutput = new Dictionary<Resource, decimal>();
            foreach (var resource in resources)
            {
                if (resource.Value == 0) continue;

                segment.ResourceOutput.Add(resource.Key, resource.Value);
            }
        }

        public static Dictionary<Resource, decimal> ResourcePerTick(this Segment segment)
        {
            var resources = new Dictionary<Resource, decimal>();
            foreach (Resource resource in Enum.GetValues(typeof(Resource)))
                resources.Add(resource, ResourcePerTick(segment, resource));

            return resources;
        }

        public static IBlock NewBlock(BlockType blockType, int x, int y)
        {
            IBlock block;

            switch (blockType)
            {
                case BlockType.Controller:
                    block = new SegmentControllerBlock();
                    break;
                case BlockType.Energy:
                    block = new BasicEnergyBlock();
                    break;
                case BlockType.Wall:
                    block = new BasicWall();
                    break;
                case BlockType.Beamer:
                    block = new BasicBeamer(x, y);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(blockType), blockType, null);
            }

            return block;
        }

        public static Direction Opposite(this Direction direction)
        {
            Direction opposite;
            switch (direction)
            {
                case Direction.Up:
                    opposite = Direction.Down;
                    break;
                case Direction.Down:
                    opposite = Direction.Up;
                    break;
                case Direction.Left:
                    opposite = Direction.Right;
                    break;
                case Direction.Right:
                    opposite = Direction.Left;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            return opposite;
        }

        public static string GetUnicodeHealth(this IHealth healthObject)
        {
            var health = (decimal) healthObject.Health / healthObject.MaxHealth;

            if (health == 1) return "💚";

            return health < 0.5m ? "❤" : "💛";
        }
    }
}