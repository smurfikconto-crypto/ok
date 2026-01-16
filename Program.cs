using Projekt_ZTP;
using System;
using System.Windows.Forms;

namespace Projekt_ZTP
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
            
            Console.WriteLine("Wybierz rozmiar planszy:"); // Na przyszłość
            Console.WriteLine("1 - 7x7");
            Console.WriteLine("2 - 10x10");
            Console.WriteLine("3 - 15x15");

            var choice = Console.ReadLine();

            Board board = choice switch
            {
                "1" => new Board(BoardType.Small),
                "2" => new Board(BoardType.Medium),
                "3" => new Board(BoardType.Large),
            };
        }
    }
}
