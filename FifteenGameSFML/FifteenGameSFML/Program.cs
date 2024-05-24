using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace FifteenGameSFML
{
    class Program
    {
        static void OnKeyPressed(object sender, KeyEventArgs e)
        {

            //logica gestione evento
            Console.WriteLine(e.Code);
        }
        static RenderWindow CreaFinestra(uint width, uint height)
        {
            VideoMode mode = new VideoMode(width, height);
            RenderWindow window = new RenderWindow(mode, "Fifteen Game");
            window.SetVerticalSyncEnabled(true);
            return window;
        }

        static void InizializzaGriglia(int[,] g)
        {
            int k = 0;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    //senza k si poteva fare:
                    //g[i,j] = i*4 + j;
                    g[i, j] = k; 
                    k++;
                }
        }

        static void VisualizzaGriglia(int[,] g, RenderWindow window)
        {
            int distanza = 10;
            int quadrato_w = 50;
            int quadrato_h = 50;
            int pos_x = 100;
            int pos_y = 60;

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    //i e j sembrano invertite, invece no!
                    int coordinata_x = pos_x + j * (distanza + quadrato_w);
                    int coordinata_y = pos_y + i * (distanza + quadrato_h);

                    //Console.WriteLine("x:{0} y:{1}, v:{2}", coordinata_x, coordinata_y, g[i,j]);
                    //disegno il quadrato alle coordinate specificate
                    RectangleShape r = new RectangleShape();
                    r.Position = new Vector2f(coordinata_x, coordinata_y);
                    r.Size = new Vector2f(quadrato_w, quadrato_h);
                    r.FillColor = new Color(0,0,Convert.ToByte((g[i,j]+10)*10));
                    window.Draw(r);
                }
        }
        static void Main(string[] args)
        {
            int[,] griglia = new int[4, 4];

            InizializzaGriglia(griglia);
            RenderWindow finestra = CreaFinestra(640, 480);
            //VisualizzaGriglia(griglia, finestra);

            finestra.KeyPressed += OnKeyPressed;

            while (finestra.IsOpen)
            {
                //gestione eventi
                finestra.DispatchEvents();

                //pulizia finestra

                //disegnare le figure
                VisualizzaGriglia(griglia, finestra);

                //visualizza a video i contenuti
                finestra.Display();

            }
        }
    }
}
