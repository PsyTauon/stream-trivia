using dotnet_api.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace dotnet_api.Hubs
{
    public class TriviaHub : Hub
    {
        private readonly IHubContext<TriviaHub> _hubContext;
        public TriviaHub(IHubContext<TriviaHub> hubContext)
        {
            _hubContext = hubContext;
        }

        // Gamestates can be stored in Mongo but for now we can do it all in memory for testing.
        public static IDictionary<string, GameState> GameStates = new Dictionary<string, GameState>();
        public async Task JoinGame(string gameId, string playerName)
        {
            GameState gameState = new GameState(gameId);
            gameState = GameStates.First(z => z.Key == gameId).Value;
            if (gameState != null)
            {
                // Create a new player with the name from the UI and the connection ID from the signalR hub
                Player player = new Player()
                {
                    Name = playerName,
                    ConnectionId = this.Context.ConnectionId
                };

                player.Id = gameState.Players.Count;
                gameState.Players.Add(player);


                // Add new connected player to the SignalR group of connected players
                await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());

                // Tell the UI that a new player has been added
                await Clients.Caller.SendAsync("JoinedSuccessful", gameId);
            }
            else
            {
                await Clients.Caller.SendAsync("JoinError", "game not found");
            }
        }
        public async Task CreateGame(string gameId, string playerName)
        {
            Player player = new Player()
            {
                Name = playerName,
                ConnectionId = this.Context.ConnectionId,
                Id = 0,
                IsHost = true
            };

            // Create a new gamestate
            GameState gameState = new GameState(gameId);
            gameState.Players.Add(player);

            GameStates.Add(gameId, gameState);

            await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());
            await Clients.Group(gameId.ToString()).SendAsync("JoinedSuccessful", gameId);
        }
        public async Task EnterRoom(string gameId)
        {
            GameState game = new GameState(gameId);
            game = GameStates.First(z => z.Key == gameId).Value;

            Player player = game.Players.First(p => p.ConnectionId == this.Context.ConnectionId);

            if (player != null)
            {
                await Clients.Group(gameId.ToString()).SendAsync("RoomEnterred", game);
                await Clients.Caller.SendAsync("SetMe", player);
            }
        }
        public async Task AddQuestion(string gameId, string question, string answer)
        {
            GameState game = new GameState(gameId);
            game = GameStates.First(z => z.Key == gameId).Value;
            Player player = game.Players.First(p => p.ConnectionId == this.Context.ConnectionId);
            if (player.IsHost)
            {
                Question newQuestion = new Question()
                {
                    QuestionText = question,
                    Answer = answer
                };
                game.CurrentQuestion = newQuestion;
                game.Status = Status.QUESTION_PHASE;
                await Clients.Group(gameId.ToString()).SendAsync("QuestionAdded", game);

                Timer t = new Timer(SetCountPhase, gameId, 10000, 10000);
            }
            else
            {
                // NOT HOST
            }
        }
        public async Task UpdatePlayerScore(string gameId, int scoreChange, string connectionId)
        {
            GameState game = new GameState(gameId);
            game = GameStates.First(z => z.Key == gameId).Value;

            Player playerToUpdate = game.Players.First(p => p.ConnectionId == connectionId);
            playerToUpdate.Score = playerToUpdate.Score + scoreChange;

            await Clients.Group(gameId.ToString()).SendAsync("ScoreUpdated", game);
        }
        private async void SetCountPhase(Object gameid)
        {
            string gameId = gameid.ToString();
            GameState game = new GameState(gameId);

            game = GameStates.First(z => z.Key == gameId).Value;
            game.Status = Status.COUNT_PHASE;

            await _hubContext.Clients.Group(gameId.ToString()).SendAsync("SetCountPhase", game);
            GC.Collect();
        }
    }
}
