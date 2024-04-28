using Box2DSharp.Dynamics.Joints;
using SFML.Window;
using System;
using System.Numerics;

namespace PhysicsJointExample
{
    class Program
    {
        static GameEngine engine;
        static bool run = false;
        static RevoluteJoint wheel_joint;
        static private void OnKeyPressed(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.A:
                    GameEngine.MoveCameraPPM(-4f, 0);
                    break;
                case Keyboard.Key.D:
                    GameEngine.MoveCameraPPM(4f, 0);
                    break;
                case Keyboard.Key.W:
                    GameEngine.MoveCameraPPM(0, 4f);
                    break;
                case Keyboard.Key.S:
                    GameEngine.MoveCameraPPM(0, -4f);
                    break;
                case Keyboard.Key.Z:
                      GameEngine.PPM++;
                    break;
                case Keyboard.Key.X:
                    GameEngine.PPM--;
                    break;
                case Keyboard.Key.Enter:
                    engine.CreateCircleObject(false, 0.3f, 5f, 5f, 0.3f, 1f, SFML.Graphics.Color.Magenta);
                    break;
                case Keyboard.Key.Space:
                    if (!run)
                        engine.DrawFrame();
                    break;
                case Keyboard.Key.LAlt:
                    run = !run;
                    break;
                case Keyboard.Key.Right:
                    wheel_joint.SetMotorSpeed(wheel_joint.GetMotorSpeed() - 1.0f);
                    break;
                case Keyboard.Key.Left:
                    wheel_joint.SetMotorSpeed(wheel_joint.GetMotorSpeed() + 1.0f);
                    break;
            }

        }

        static private void OnKeyReleased(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.A:
                    break;
                case Keyboard.Key.D:
                    break;
                case Keyboard.Key.W:
                    break;
                case Keyboard.Key.S:
                    break;
            }
        }

        static void Main(string[] args)
        {
            engine = new GameEngine();
            engine.CreateWindow("SFML+Box2D Game Engine", 1000, 800, 0f, -10f, 100, 0f, 0f);
            GameEngine.window.KeyPressed += OnKeyPressed;

            //pareti
            engine.CreateRectangleObject(true, 10f, 0.2f, 5f, 0f, 0.1f, 0.3f, 1f, SFML.Graphics.Color.Green);
            engine.CreateRectangleObject(true, 10f, 0.2f, 5f, 8f, 0.1f, 0.3f, 1f, SFML.Graphics.Color.Green);
            engine.CreateRectangleObject(true, 0.2f, 8, 0f, 4f, 0.1f, 0.3f, 1f, SFML.Graphics.Color.Green);
            engine.CreateRectangleObject(true, 0.2f, 8f, 10f, 4f, 0.1f, 0.3f, 1f, SFML.Graphics.Color.Green);
            
            RectangleObject support = (RectangleObject) engine.CreateRectangleObject(true, 0.2f, 1f, 4f, 4f, 0f, 0.3f, 1f, SFML.Graphics.Color.Yellow);
            RectangleObject fan = (RectangleObject) engine.CreateRectangleObject(false, 2f, 0.2f, 3f, 4.5f, 0f, 0.3f, 1f, SFML.Graphics.Color.White);
            engine.CreateRevoluteJoint(support, fan, new Vector2(0, 0), new Vector2(0, 0), true, -1, 0, 100, false, 10000f, false);

            RectangleObject chassis = (RectangleObject)engine.CreateRectangleObject(false, 2f, 1f, 5f, 5f,  0f, 0.3f, 3f, SFML.Graphics.Color.Yellow);
            RectangleObject arm = (RectangleObject)engine.CreateRectangleObject(false, 1.5f, 0.5f, 4f, 4f, 0f, 0.3f, 3f, SFML.Graphics.Color.White);
            CircleObject rear_wheel = (CircleObject) engine.CreateCircleObject(false, 0.6f, 4f, 2f, 0.3f, 1f, SFML.Graphics.Color.Red);
            CircleObject front_wheel = (CircleObject)engine.CreateCircleObject(false, 0.55f, 6f, 2f, 0.3f, 1f, SFML.Graphics.Color.Blue);
            //giunto chassis-braccio
            engine.CreateRevoluteJoint(chassis, arm, new Vector2(-0.5f, -0.25f), new Vector2(0.5f, 0), false, 0, 0.15f, 0.3f, true, 0, false);
            //giunto braccio-ruota posteriore
            wheel_joint = engine.CreateRevoluteJoint(arm, rear_wheel, new Vector2(-0.65f, 0f), new Vector2(0f, 0), true, -1.0f, 0f, 0f, false, 10000f, false);
            //giunto chassis-ruota anteriore
            engine.CreateWheelJoint(chassis, front_wheel, new Vector2(1.5f, -0.75f), new Vector2(0, 0), new Vector2(1.5f, -1f), false, 0f, -0.1f, 0.5f, true, 0f, 2.1f, 4.9f, false);

            //loop principale
            while (GameEngine.window.IsOpen)
            {
                //gestione degli eventi SFML
                engine.HandleEvents();
                if (run == true)
                    engine.DrawFrame();
            }
        }
    }
}