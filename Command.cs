// Command + GameState (Memento hook)
using Projekt_ZTP;
using System;

namespace Projekt_ZTP
{
    public abstract class Command
    {
        public abstract void Execute();
        public abstract void Undo();
    }

    public class PlaceShipCommand : Command
    {
        private readonly Board _board;
        private readonly IShip _ship;
        private readonly Position _position;

        public PlaceShipCommand(Board board, IShip ship, Position position)
        {
            _board = board;
            _ship = ship;
            _position = position;
        }

        public override void Execute()
        {
            _board.PlaceShip(_ship, _position);
        }

        public override void Undo()
        {
            // Dodać usuwanie statku z planszy
        }
    }

    public class ShotCommand : Command
    {
        private readonly Board _board;
        private readonly Position _position;
        private ShotResult _result;

        public ShotCommand(Board board, Position position)
        {
            _board = board;
            _position = position;
        }

        public override void Execute()
        {
            _result = _board.ReceiveShot(_position);
        }

        public override void Undo()
        {
            // Dodać cofnięcie strzału
        }
    }

    public class GameState
    {
        public Player? Turn { get; set; }
        public Board? PlayerBoard { get; set; }
        public Board? AiBoard { get; set; }

        public object GetState()
        {
            // Dodać zwracanie serializowalnego obiektu
            return new { };
        }

        public void RestoreState(object state)
        {
            // Dodać odtwórzanie stanu
        }
    }
}
