using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;


namespace HelloSFML
{
    class Program
    {
        //altezza e larghezza della finestra
        const int WIDTH = 640;
        const int HEIGHT = 480;
        const string TITLE = "Hello SFML!";

        //creazione forma circolare
        static CircleShape circle1 = new CircleShape(50);
        static RectangleShape rectangleShape = new RectangleShape(new Vector2f(50,50));

        static private void OnKeyPressed(object sender, KeyEventArgs e)
        {
            //stampo il tasto premuto
            Console.WriteLine(e);

            //prendo la posizione corrente della forma
            Vector2f pos = circle1.Position;

            //in base al tasto premuto scelgo cosa fare
            switch (e.Code)
            {
                case Keyboard.Key.A:
                    pos.X -= 10;
                    circle1.Position = pos;
                    break;
                case Keyboard.Key.D:
                    pos.X += 10;
                    circle1.Position = pos;
                    break;
                case Keyboard.Key.W:
                    pos.Y -= 10;
                    circle1.Position = pos;
                    break;
                case Keyboard.Key.S:
                    pos.Y += 10;
                    circle1.Position = pos;
                    break;
            }
        }

        static void Main(string[] args)
        {
            //impostazioni finestra
            VideoMode mode = new VideoMode(WIDTH, HEIGHT);
            RenderWindow window = new RenderWindow(mode, TITLE);
            window.SetVerticalSyncEnabled(true);

            //creazione figura
            circle1.FillColor = new Color(100, 250, 50);

            //evento chiusura della finestra
            window.Closed += (sender, args) => window.Close();

            //gestione eventi della tastiera
            window.KeyPressed += OnKeyPressed;

            //loop principale
            while (window.IsOpen)
            {
                //gestione degli eventi
                window.DispatchEvents();

                //pulizia della finestra
                window.Clear(Color.Black);

                //disegna la figura
                window.Draw(circle1);

                //visualizza i contenuti disegnati
                window.Display();
            }
        }
    }
}
