// Gracz, AI, Pozycja, strategie
using Projekt_ZTP;

namespace Projekt_ZTP
{
    public class Player
    {
        private readonly string _name;
        public Board Board { get; }

        public Player(string name, Board board)
        {
            _name = name;
            Board = board;
        }

        public string GetName() => _name;
    }

    public class AIPlayer : Player
    {
        private IShootingStrategy _strategy;

        public AIPlayer(string name, Board board, IShootingStrategy strategy)
            : base(name, board)
        {
            _strategy = strategy;
        }

        public void SetStrategy(IShootingStrategy strategy) => _strategy = strategy;

        public Position Shoot(Board enemyBoard) => _strategy.SelectTarget(enemyBoard);
    }

    public readonly struct Position
    {
        public int Row { get; }
        public int Col { get; }

        public Position(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }

    public interface IShootingStrategy
    {
        Position SelectTarget(Board board);
    }

    public class RandomStrategy : IShootingStrategy
    {
        private readonly Random _rnd = new();

        public Position SelectTarget(Board board)
            => new Position(_rnd.Next(0, 10), _rnd.Next(0, 10));
    }

    public class HuntStrategy : IShootingStrategy
    {
        public Position SelectTarget(Board board)
        {
            // Dodać strzelanie obok trafionych pól
            return new Position(0, 0);
        }
    }
}
