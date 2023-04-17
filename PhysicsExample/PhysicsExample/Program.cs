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
        const int HEIGHT = 480;
        const string TITLE = "Hello Box2D+SFML!";
        static Vector2 gravity = new(0f, -10f);
        static World world = new World(gravity);

        static VideoMode mode = new VideoMode(WIDTH, HEIGHT);
        static RenderWindow window = new RenderWindow(mode, TITLE);

        static Vector2 force = new Vector2(0, 0);
        static List<GameObject> list = new List<GameObject>();

        static private void OnKeyPressed(object sender, KeyEventArgs e)
        {
            float v = 1f;

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
                    list.Add(new GameObject(in world, in window, new Vector2f(WIDTH / 2, HEIGHT / 2), new Vector2f(10, 10), BodyType.DynamicBody, new SFML.Graphics.Color(250, 250, 0)));
                    break;
                case Keyboard.Key.Enter:
                    list.Add(new GameObject(in world, in window, new Vector2f(WIDTH / 2, HEIGHT / 2), new Vector2f(20, 20), BodyType.DynamicBody, new SFML.Graphics.Color(250, 0, 250)));
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

            GameObject g = new GameObject(in world, in window, new Vector2f(WIDTH / 2, HEIGHT / 2), new Vector2f(40, 40), BodyType.DynamicBody, new SFML.Graphics.Color(250, 250, 250));
            GameObject f1 = new GameObject(in world, in window, new Vector2f(WIDTH / 2, HEIGHT - 10), new Vector2f(1000, 10), BodyType.StaticBody, new SFML.Graphics.Color(200, 200, 200));
            GameObject f2 = new GameObject(in world, in window, new Vector2f(WIDTH / 2, 10), new Vector2f(1000, 10), BodyType.StaticBody, new SFML.Graphics.Color(200, 200, 200));
            GameObject f3 = new GameObject(in world, in window, new Vector2f(10, HEIGHT / 2), new Vector2f(10, 1000), BodyType.StaticBody, new SFML.Graphics.Color(200, 200, 200));
            GameObject f4 = new GameObject(in world, in window, new Vector2f(WIDTH - 10, HEIGHT / 2), new Vector2f(10, 1000), BodyType.StaticBody, new SFML.Graphics.Color(200, 200, 200));

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

                world.Step(time_step, velocity_iterations, position_iterations);

                g.ApplyLinearImpulse(force);
                g.UpdateSFMLObject();

                foreach (var temp in list)
                {
                    temp.UpdateSFMLObject();
                    temp.DrawSFMLObject();
                }

                //disegna la figura
                g.DrawSFMLObject();
                f1.DrawSFMLObject();
                f2.DrawSFMLObject();
                f3.DrawSFMLObject();
                f4.DrawSFMLObject();

                //visualizza i contenuti disegnati
                window.Display();
            }
        }
    }
}
