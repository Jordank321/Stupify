﻿using System;
using System.Linq;
using System.Threading.Tasks;

namespace Stupify.Data.CustomCommandBuiltIns
{
    class CharDecode : BuiltInCommand
    {
        public CharDecode(IServiceProvider provider) : base(provider)
        {
            Tag = "$charDecode";
            Execute = args =>
            {
                var chars = args.Select(arg => (char) int.Parse(arg));
                return Task.FromResult(new string(chars.ToArray()));
            };
        }
    }
}
