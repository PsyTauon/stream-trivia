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

        public Question CurrentQuestion { get; set; }

        public Status Status { get; set; } = Status.WAITING_PHASE;

        public GameState(string gameid)
        {
            this.GameId = gameid;
            this.Players = new List<Player>();
        }
    }
    public enum Status
    {
        WAITING_PHASE = 1,
        QUESTION_PHASE = 2,
        COUNT_PHASE = 3,
        GAMEOVER = 4,
    }
}
