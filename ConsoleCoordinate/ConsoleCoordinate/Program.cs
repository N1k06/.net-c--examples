using System;

namespace ConsoleCoordinate
{
    class Program
    {
        //non blocking readkey https://stackoverflow.com/questions/5620603/non-blocking-read-from-standard-i-o-in-c-sharp
        static void PrintCoord(int x, int y, string s, ConsoleColor foreground, ConsoleColor background)
        {
            //ConsoleColor old_foreground = Console.BackgroundColor;
            //ConsoleColor old_background = Console.ForegroundColor;
            Console.BackgroundColor = background;
            Console.ForegroundColor = foreground;
            Console.SetCursorPosition(x, y);
            Console.WriteLine(s);
        }

        static void Disegna()
        {
            ConsoleKey k;
            int x = 0;
            int y = 0;
            int cols = 200;
            int rows = 40;
           // int cols = Console.LargestWindowWidth;
            //int rows = Console.LargestWindowHeight;
            int fc = Convert.ToInt32(Console.ForegroundColor);
            int bc = Convert.ToInt32(Console.BackgroundColor);
            Console.SetBufferSize(cols+10,rows+10);
            Console.SetWindowSize(cols,rows);
            //ciclo finchè non si preme ESC
            do
            {
                //in base al valore letto da tastiera sposto la coordinata
                k = Console.ReadKey(true).Key;
                switch (k)
                {
                    case ConsoleKey.LeftArrow:
                        x--;
                        break;
                    case ConsoleKey.RightArrow:
                        x++;
                        break;
                    case ConsoleKey.UpArrow:
                        y--;
                        break;
                    case ConsoleKey.DownArrow:
                        y++;
                        break;
                    case ConsoleKey.F1:
                        ConsoleColor old_foreground = Console.BackgroundColor;
                        fc = Convert.ToInt32(old_foreground) + 1;
                        Console.ForegroundColor = (ConsoleColor)fc;
                        break;
                    case ConsoleKey.F2:
                        ConsoleColor old_background = Console.ForegroundColor;
                        bc = Convert.ToInt32(old_background) + 1;
                        Console.BackgroundColor = (ConsoleColor)bc;
                        break;
                }
                //Faccio in modo di non uscire dalla griglia
                x = x % cols;
                y = y % rows;
                //impedisce che le coordinate diventino negative
                if (x < 0)
                    x = cols - 1;
                if (y < 0)
                    y = rows - 1;
                //stampo il carattere alla posizione desiderata coi colori desiderati
                PrintCoord(x, y, "O", (ConsoleColor)fc, (ConsoleColor)bc);

            } while (k != ConsoleKey.Escape);
        }
        static void Main(string[] args)
        {
            Disegna();
        }
    }
}
