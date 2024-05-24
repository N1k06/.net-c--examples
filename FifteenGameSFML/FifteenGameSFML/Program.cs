using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace FifteenGameSFML
{
    class Program
    {
        static int[,] griglia;
        static void OnKeyPressed(object sender, KeyEventArgs e)
        {

            //logica gestione evento
            Console.WriteLine(e.Code);

            switch (e.Code)
            {
                case Keyboard.Key.Up:
                    SpostaVuoto(griglia, 1, 0);
                    break;
                case Keyboard.Key.Down:
                    SpostaVuoto(griglia, -1, 0);
                    break;
                case Keyboard.Key.Left:
                    SpostaVuoto(griglia, 0, 1);
                    break;
                case Keyboard.Key.Right:
                    SpostaVuoto(griglia, 0, -1);
                    break;
            }

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

                    //disegno il quadrato alle coordinate specificate
                    RectangleShape r = new RectangleShape();
                    r.Size = new Vector2f(quadrato_w, quadrato_h);
                    r.Position = new Vector2f(coordinata_x, coordinata_y);
                    r.FillColor = new Color(0,0,Convert.ToByte(10*(g[i,j]+10)));
                    window.Draw(r);
                }
        }

        static void SpostaVuoto(int[,] g, int i_spost, int j_spost)
        {
            int i_vuoto = 0,j_vuoto = 0;
            bool trovato = false;

            //cerca lo spazio dentro alla griglia
            for (int i = 0; i < 4 && !trovato; i++)
                for (int j = 0; j < 4 && !trovato; j++)
                    if (g[i, j] == 0)
                    {
                        trovato = true;
                        i_vuoto = i;
                        j_vuoto = j;
                    }
            //calcolo i nuovi indici dello spazio vuoto
            int i_nuova_vuoto = i_vuoto + i_spost;
            int j_nuova_vuoto = j_vuoto + j_spost;

            //verifico che le nuove coordinate dello spazio non escano dalla matrice
            if (i_nuova_vuoto >= 0 && i_nuova_vuoto <= 3 && j_nuova_vuoto >= 0 && j_nuova_vuoto <= 3)
            {
                int aux = g[i_vuoto, j_vuoto];
                g[i_vuoto, j_vuoto] = g[i_nuova_vuoto, j_nuova_vuoto];
                g[i_nuova_vuoto, j_nuova_vuoto] = aux;
            }
        }
        static void Main(string[] args)
        {
            griglia = new int[4, 4];

            InizializzaGriglia(griglia);
            RenderWindow finestra = CreaFinestra(640, 480);

            finestra.KeyPressed += OnKeyPressed;

            //ciclo principale di gioco
            while (finestra.IsOpen)
            {
                //gestione eventi
                finestra.DispatchEvents();

                //pulizia finestra
                finestra.Clear(Color.Black);

                //disegnare le figure
                VisualizzaGriglia(griglia, finestra);

                //visualizza a video i contenuti
                finestra.Display();
            }
        }
    }
}
