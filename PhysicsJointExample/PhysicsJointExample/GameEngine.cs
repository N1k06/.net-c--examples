using Box2DSharp.Collision.Shapes;
using Box2DSharp.Dynamics;
using Box2DSharp.Dynamics.Joints;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsJointExample
{
    class GameEngine
    {
        //mondo di Box2D
        static public World b2_world = null;
        //finestra SFML
        static public RenderWindow window = null;
        //pixel per meter (conversione coordinate da Box2D a SFML e viceversa)
        static public uint PPM = 1;
        //posizione della camera (top-right corner)
        static protected Vector2 camera_position;

        //parametri per la simulazione fisica
        //diverse combinazioni di valori posso avere effetto sulla velocità e l'accuratezza della simulazione
        //vedi documentazione box2d
        static float time_step = (1.0f / 60.0f);
        static int velocity_iterations = 6;
        static int position_iterations = 2;

        static protected List<GameObject> game_objects = new List<GameObject>();
        static protected List<Joint> joints = new List<Joint>();
        public void CreateWindow(string title, uint window_width, uint window_height, float gravity_x, float gravity_y, uint PPM, float camera_x, float camera_y)
        {
            VideoMode mode = new VideoMode(window_width, window_height);
            window = new RenderWindow(mode, title);
            window.SetVerticalSyncEnabled(true);
            window.Closed += (sender, args) => window.Close();

            b2_world = new World(new Vector2(gravity_x, gravity_y));

            GameEngine.PPM = PPM;
            camera_position = new Vector2(camera_x, camera_y);
        }

        public GameObject CreateRectangleObject(bool is_static, float width, float height, float position_x, float position_y, float rad_angle, float friction, float density, Color color)
        {
            RectangleObject r = new RectangleObject(is_static, width, height, position_x, position_y, rad_angle, friction, density, color);
            game_objects.Add(r);
            return r;
        }

        public GameObject CreateCircleObject(bool is_static, float radius, float position_x, float position_y, float friction, float density, Color color)
        {
            CircleObject r = new CircleObject(is_static, radius, position_x, position_y, friction, density, color);
            game_objects.Add(r);
            return r;
        }

        public void HandleEvents()
        {
            window.DispatchEvents();
        }
        
        public void DrawFrame()
        {
            //pulizia della finestra
            window.Clear(Color.Black);

            //simula la fisica negli oggetti di gioco (forze, collisioni, ecc) 
            b2_world.Step(time_step, velocity_iterations, position_iterations);

            foreach (GameObject g in game_objects)
            {
                g.Draw();
            }

            window.Display();
        }

        static public Vector2f FromBox2DToSFMLCoordinates(float x, float y)
        {          
            return FromBox2DToSFMLCoordinates(new Vector2(x, y));
        }

        static public Vector2f FromBox2DToSFMLCoordinates(Vector2 coordinate)
        {
            coordinate -= camera_position;
            Vector2f sfml_coordinates = new Vector2f(coordinate.X*PPM, coordinate.Y*PPM);
            sfml_coordinates.Y = window.Size.Y - sfml_coordinates.Y;
            return sfml_coordinates;
        }

        public static void MoveCameraAbs(float delta_x, float delta_y)
        {
            Vector2 delta = new Vector2(delta_x, delta_y);
            camera_position = camera_position + delta;
        }

        public static void MoveCameraPPM(float delta_x, float delta_y)
        {
            Vector2 delta = new Vector2(delta_x, delta_y);
            camera_position = camera_position + delta/((float)PPM);
        }

        public RevoluteJoint CreateRevoluteJoint(GameObject a, GameObject b, Vector2 local_anchor_a, Vector2 local_anchor_b, bool enable_motor, float motor_speed, float lower_angle, float upper_angle, bool enable_limit, float max_torque, bool collide_connected)
        {
            RevoluteJointDef jd = new RevoluteJointDef();
            jd.BodyA = a.b2_body;
            jd.BodyB = b.b2_body;
            jd.CollideConnected = collide_connected;
            jd.LocalAnchorA = local_anchor_a;
            jd.LocalAnchorB = local_anchor_b;
            jd.MotorSpeed = motor_speed;
            //se non si setta questo attributo il motore non parte!
            jd.MaxMotorTorque = max_torque;
            jd.EnableMotor = enable_motor;
            jd.LowerAngle = lower_angle * Convert.ToSingle(Math.PI);
            jd.UpperAngle = upper_angle * Convert.ToSingle(Math.PI);
            jd.EnableLimit = enable_limit;

            RevoluteJoint j = (RevoluteJoint) b2_world.CreateJoint(jd);
            joints.Add(j);
            return j;
        }

        public WheelJoint CreateWheelJoint(GameObject a, GameObject b, Vector2 local_anchor_a, Vector2 local_anchor_b, Vector2 local_axis_a, bool enable_motor, float motor_speed, float lower_translation, float upper_translation, bool enable_limit, float max_torque, float stiffness, float damping, bool collide_connected)
        {
            WheelJointDef jd = new WheelJointDef();
            jd.BodyA = a.b2_body;
            jd.BodyB = b.b2_body;
            jd.LocalAxisA = local_axis_a;
            jd.CollideConnected = collide_connected;
            jd.LocalAnchorA = local_anchor_a;
            jd.LocalAnchorB = local_anchor_b;
            jd.MotorSpeed = motor_speed;
            //se non si setta questo attributo il motore non parte!
            jd.MaxMotorTorque = max_torque;
            jd.EnableMotor = enable_motor;
            jd.Stiffness = stiffness;
            jd.Damping = damping;
            jd.LowerTranslation = lower_translation;
            jd.UpperTranslation = upper_translation;
            jd.EnableLimit = enable_limit;

            WheelJoint j = (WheelJoint)b2_world.CreateJoint(jd);
            joints.Add(j);
            return j;
        }
    }
}