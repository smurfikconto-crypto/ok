// Singleton + Command/Memento hooki
using System;
using System.Numerics;

namespace Projekt_ZTP
{
    public sealed class GameManager
    {
        private static readonly Lazy<GameManager> _instance =
            new(() => new GameManager());

        public static GameManager Instance => _instance.Value;

        private GameManager() { }

        private Player[] _players = Array.Empty<Player>();
        private GameState _currentState = new GameState();
        public Game? CurrentGame { get; private set; }

        public Player? CurrentPlayer => _currentState.Turn;

        public void StartGame()
        {
            var board1 = new Board();
            var board2 = new Board();

            var factory = new ConcreteStandardFactory();
            var p1 = new Player("Player", board1);
            var ai = new AIPlayer("AI", board2, new RandomStrategy());

            _players = new[] { p1, ai };

            CurrentGame = new Game(p1, ai, board1, board2);
            _currentState = new GameState
            {
                Turn = p1,
                PlayerBoard = board1,
                AiBoard = board2
            };
        }

        public void EndGame()
        {
            CurrentGame = null;
        }

        public void SaveGameState()
        {
            // Dodać Memento – serializacja GameState
        }
    }
}
