using System;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace HelloSFML
{
    class Program
    {
        const int WIDTH = 640;
        const int HEIGHT = 480;
        const string TITLE = "SHMUP";
        static void Main(string[] args)
        {
            VideoMode mode = new VideoMode(WIDTH, HEIGHT);
            RenderWindow window = new RenderWindow(mode, TITLE);
            CircleShape shape = new CircleShape(50);
            shape.FillColor = new Color(100, 250, 50);

            window.SetVerticalSyncEnabled(true);

            window.Closed += (sender, args) => window.Close();

            while (window.IsOpen)
            {
                window.DispatchEvents();

                window.Clear(Color.Black);
                window.Draw(shape);
                window.Display();
            }
        }
    }
}