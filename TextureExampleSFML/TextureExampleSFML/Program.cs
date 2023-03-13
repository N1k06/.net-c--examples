using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;


namespace TextureExampleSFML
{
    class Program
    {
        //altezza e larghezza della finestra
        const int WIDTH = 640;
        const int HEIGHT = 480;
        const string TITLE ="Sprite animation test!";

        //grafica 2d presa da: https://opengameart.org/content/zelda-like-tilesets-and-sprites
        //creo la texture che contiene l'immagine con le animazioni del personaggio (spritesheet)
        //definisco le coordinate x,y che voglio usare della texture (ad esempio non carico i margini)
        static IntRect rect = new IntRect(0, 5, 250, 250);
        static Texture texture = new Texture(@"..\..\..\gfx\character.png", rect);
        //creo uno sprite associando la texture appena caricata
        static Sprite sprite = new Sprite(texture);

        //variabili per la gestione delle animazioni
        static int animation = 0;
        static int movement = 0;

        static private void OnKeyPressed(object sender, KeyEventArgs e)
        {
            //stampo il tasto premuto
            Console.WriteLine(e);

            //in base al tasto premuto scelgo cosa fare
            switch (e.Code)
            {
                case Keyboard.Key.A:
                    movement = 3;
                    animation += 1;
                    animation %= 4;
                    break;
                case Keyboard.Key.D:
                    movement = 1;
                    animation += 1;
                    animation %= 4;
                    break;
                case Keyboard.Key.W:
                    movement = 2;
                    animation += 1;
                    animation %= 4;
                    break;
                case Keyboard.Key.S:
                    movement = 0;
                    animation += 1;
                    animation %= 4;
                    break;
            }
            
            //aggiorno il rettangolo della texture associata allo sprite
            //ottenendo l'effetto di animazione
            sprite.TextureRect = new IntRect(animation*16, movement*32, 15, 22);

        }

        static void Main(string[] args)
        {
            //impostazioni finestra
            VideoMode mode = new VideoMode(WIDTH, HEIGHT);
            RenderWindow window = new RenderWindow(mode, TITLE);
            window.SetVerticalSyncEnabled(true);

            //setto il fattore di scala e posizione dello sprite
            sprite.Scale = new Vector2f(10.0f, 10.0f);
            sprite.Position = new Vector2f(320-80, 240-100);
            sprite.TextureRect = new IntRect(animation * 16, movement * 32, 15, 22);

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

                //disegna lo sprite
                window.Draw(sprite);

                //visualizza i contenuti disegnati
                window.Display();
            }
        }
    }
}
