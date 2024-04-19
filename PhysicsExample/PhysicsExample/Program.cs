using System;
using System.Numerics;

using SFML;
using SFML.Window;
using SFML.System;
using SFML.Graphics;

using Box2DSharp;
using Box2DSharp.Common;
using Box2DSharp.Dynamics;
using Box2DSharp.Collision;
using Box2DSharp.Collision.Shapes;
using System.Collections.Generic;
namespace PhysicsExample
{
    class Program
    {
        //altezza e larghezza della finestra
        const int WIDTH = 640;
        const int HEIGHT = 640;
        const string TITLE = "Hello Box2D+SFML!";
        static Vector2 gravity = new(0f, -10f);
        static World world = new World(gravity);

        static VideoMode mode = new VideoMode(WIDTH, HEIGHT);
        static RenderWindow window = new RenderWindow(mode, TITLE);

        static Vector2 force = new Vector2(0, 0);

        static List<RectangleObject> rectangles_list = new List<RectangleObject>();
        static List<CircleObject> circles_list = new List<CircleObject>();


        static private void OnKeyPressed(object sender, KeyEventArgs e)
        {
            float v = 1.0f;

            //in base al tasto premuto scelgo cosa fare
            switch (e.Code)
            {
                case Keyboard.Key.A:
                    force.X = -v;
                    break;
                case Keyboard.Key.D:
                    force.X = v;
                    break;
                case Keyboard.Key.W:
                    force.Y = v;
                    break;
                case Keyboard.Key.S:
                    force.Y = -v;
                    break;
                case Keyboard.Key.Space:
                    CircleObject circle_aux = new CircleObject(in world, in window, new Vector2f(WIDTH / 2, HEIGHT / 2), 10.0f, BodyType.DynamicBody, new SFML.Graphics.Color(250, 250, 0));
                    circles_list.Add(circle_aux);
                    break;
                case Keyboard.Key.Enter:
                    RectangleObject rectangle_aux = new RectangleObject(in world, in window, new Vector2f(WIDTH / 2, HEIGHT / 2), new Vector2f(20, 20), BodyType.DynamicBody, new SFML.Graphics.Color(250, 0, 250));
                    rectangles_list.Add(rectangle_aux);
                    break;
            }
        }

        static private void OnKeyReleased(object sender, KeyEventArgs e)
        {
            //stampo il tasto premuto
            Console.WriteLine(e);
            //in base al tasto premuto scelgo cosa fare
            switch (e.Code)
            {
                case Keyboard.Key.A:
                    force.X = 0;
                    break;
                case Keyboard.Key.D:
                    force.X = 0;
                    break;
                case Keyboard.Key.W:
                    force.Y = 0;
                    break;
                case Keyboard.Key.S:
                    force.Y = 0;
                    break;
            }
        }

        static void Main(string[] args)
        {
            window.SetVerticalSyncEnabled(true);
            window.Closed += (sender, args) => window.Close();
            //gestione eventi della tastiera
            window.KeyPressed += OnKeyPressed;
            window.KeyReleased += OnKeyReleased;

            //oggetti che si muovono alla pressione di WASD
            RectangleObject g = new RectangleObject(in world, in window, new Vector2f(WIDTH / 2, HEIGHT / 2), new Vector2f(40, 40), BodyType.DynamicBody, new SFML.Graphics.Color(250, 250, 250));
            CircleObject c = new CircleObject(in world, in window, new Vector2f(WIDTH / 4, HEIGHT / 4), 20, BodyType.DynamicBody, new SFML.Graphics.Color(250, 250, 250));

            //muri che delimitano l'area di gioco (StaticBody invece che DynamicBody). non subiscono forze ma interagiscono per le collisioni
            RectangleObject f1 = new RectangleObject(in world, in window, new Vector2f(WIDTH / 2, HEIGHT - 10), new Vector2f(1000, 10), BodyType.StaticBody, new SFML.Graphics.Color(200, 200, 200));
            RectangleObject f2 = new RectangleObject(in world, in window, new Vector2f(WIDTH / 2, 10), new Vector2f(1000, 10), BodyType.StaticBody, new SFML.Graphics.Color(200, 200, 200));
            RectangleObject f3 = new RectangleObject(in world, in window, new Vector2f(10, HEIGHT / 2), new Vector2f(10, 1000), BodyType.StaticBody, new SFML.Graphics.Color(200, 200, 200));
            RectangleObject f4 = new RectangleObject(in world, in window, new Vector2f(WIDTH - 10, HEIGHT / 2), new Vector2f(10, 1000), BodyType.StaticBody, new SFML.Graphics.Color(200, 200, 200));

            //parametri per la simulazione fisica
            //diverse combinazioni di valori posso avere effetto sulla velocità e l'accuratezza della simulazione
            //vedi documentazione box2d
            float time_step = (1.0f / 60.0f);
            int velocity_iterations = 6;
            int position_iterations = 2;

            //loop principale
            while (window.IsOpen)
            {
                //pulizia della finestra
                window.Clear(SFML.Graphics.Color.Black);
                //gestione degli eventi
                window.DispatchEvents();

                //simula la fisica negli oggetti di gioco (forze, collisioni, ecc) 
                world.Step(time_step, velocity_iterations, position_iterations);

                //applica il vettore forza (controllato con WASD) agli oggetti controllati dal giocatore
                g.ApplyLinearImpulse(force);
                g.UpdateSFMLObject();
                //l'oggetto c si muove in maniera opposta al precedente
                c.ApplyLinearImpulse(-force);
                c.UpdateSFMLObject();

                //disegna gli oggetti generati dinamicamente (quadrati e cerchi)
                foreach (var temp in rectangles_list)
                {
                    temp.UpdateSFMLObject();
                    temp.DrawSFMLObject();
                }
                foreach (var temp in circles_list)
                {
                    temp.UpdateSFMLObject();
                    temp.DrawSFMLObject();
                }

                //disegna le figure sulla finestra
                g.DrawSFMLObject();
                c.DrawSFMLObject();
                f1.DrawSFMLObject();
                f2.DrawSFMLObject();
                f3.DrawSFMLObject();
                f4.DrawSFMLObject();

                //visualizza i contenuti disegnati sulla finestra
                window.Display();
            }
        }
    }
}
