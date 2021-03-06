﻿using System;

namespace BotDataGraph.MessageAnalyser.Models
{
    public class Alias
    {
        private string time;

        public string Name { get; set; }

        public DateTime Time
        {
            get => DateTime.Parse(time);
            set => time = value.ToString("s");
        }
    }
}