﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_api.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public bool IsHost { get; set; } = false;
        public string ConnectionId { get; set; }
        public int Score { get; set; } = 0;
    }
}
