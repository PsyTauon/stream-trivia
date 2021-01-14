using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_api.Models
{
    public class GameState
    {
        public string GameId { get; set; }

        public List<Player> Players { get; set; }

        public GameState(string gameid)
        {
            this.GameId = gameid;
            this.Players = new List<Player>();
        }
    }
}
