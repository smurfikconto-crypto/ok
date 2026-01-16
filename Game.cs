// Przechowalnia na plansze
using System.Numerics;

namespace Projekt_ZTP
{
    public class Game
    {
        public Player Player { get; }
        public AIPlayer Ai { get; }
        public Board PlayerBoard { get; }
        public Board AiBoard { get; }

        public Game(Player player, AIPlayer ai, Board playerBoard, Board aiBoard)
        {
            Player = player;
            Ai = ai;
            PlayerBoard = playerBoard;
            AiBoard = aiBoard;
        }
    }
}
