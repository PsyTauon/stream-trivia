using dotnet_api.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_api.Hubs
{
    public class TriviaHub : Hub
    {
		// Gamestates can be stored in Mongo but for now we can do it all in memory for testing.
		public static IDictionary<string, GameState> GameStates = new Dictionary<string, GameState>();
		public async Task JoinGame(string gameId, string playerName)
		{
			// Create a new player with the name from the UI and the connection ID from the signalR hub
			Player player = new Player()
			{
				PlayerName = playerName,
				ConnectionId = this.Context.ConnectionId
			};

			// Create a new gamestate
			GameState gameState = new GameState(gameId);

			if (GameStates.Where(x => x.Value.GameId.ToString() == gameId).Count() < 1)
			{
				// TODO: Make the first player the host.
				player.PlayerId = 0;
				gameState.Players.Add(player);
				GameStates.Add(gameId, gameState);
			}
			else
			{
				gameState = GameStates.First(x => x.Value.GameId.ToString() == gameId).Value;
				player.PlayerId = gameState.Players.Count;
				gameState.Players.Add(player);
			}

			// Add new connected player to the SignalR group of connected players
			await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());

			// Tell the UI that a new player has been added
			await Clients.Group(gameId.ToString()).SendAsync("PlayerJoined", playerName);
		}
	}
}
