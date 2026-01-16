// Plansza + FlyweightCell + ShotResult
using System;
using System.Collections.Generic;

namespace Projekt_ZTP
{
    public enum ShotResult { MISS, HIT, SUNK }

    public class Board
    {
        private readonly FlyweightCell[,] _cells = new FlyweightCell[10, 10];
        private readonly List<IShip> _ships = new();

        public Board()
        {
            for (int r = 0; r < 10; r++)
                for (int c = 0; c < 10; c++)
                    _cells[r, c] = FlyweightCell.GetCell('~');
        }

        public void PlaceShip(IShip ship, Position pos)
        {
            _ships.Add(ship);
        }

        public ShotResult ReceiveShot(Position pos)
        {
            foreach (var ship in _ships)
            {
                if (ship.Hit(pos))
                {
                    return ship.IsSunk() ? ShotResult.SUNK : ShotResult.HIT;
                }
            }
            return ShotResult.MISS;
        }

        public bool IsGameOver()
        {
            foreach (var s in _ships)
                if (!s.IsSunk()) return false;
            return true;
        }
    }

    public class FlyweightCell
    {
        private static readonly Dictionary<char, FlyweightCell> _pool = new();
        private readonly char _intrinsicState;

        private FlyweightCell(char state)
        {
            _intrinsicState = state;
        }

        public static FlyweightCell GetCell(char type)
        {
            if (!_pool.TryGetValue(type, out var cell))
            {
                cell = new FlyweightCell(type);
                _pool[type] = cell;
            }
            return cell;
        }

        public char GetState() => _intrinsicState;
    }
}
