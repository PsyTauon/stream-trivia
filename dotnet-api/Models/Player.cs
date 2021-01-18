using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_api.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsHost { get; set; } = false;
        public string ConnectionId { get; set; }
        public int Score { get; set; } = 0;
        public string CurrentAnswer { get; set; }
        public bool AnswerSubmitted { get; set; } = false;
    }
}
