using Box2DSharp.Collision.Shapes;
using Box2DSharp.Dynamics;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsJointExample
{
    class CircleObject : GameObject
    {
        float b2_radius;
        Box2DSharp.Collision.Shapes.CircleShape b2_circle_shape;
        FixtureDef b2_fixture_def;

        SFML.Graphics.CircleShape sfml_circle_shape;

        public CircleObject(bool is_static, float b2_radius, float b2_position_x, float b2_position_y, float b2_friction, float b2_density, Color color) : base(is_static, b2_position_x, b2_position_y)
        {
            this.b2_radius = b2_radius;
            //richiama i metodi per la creazione degli oggetti SFML e Box2D
            CreateBox2DObject(b2_density, b2_friction);
            CreateSFMLObject(color);
        }

        protected override void CreateBox2DObject(float b2_density, float b2_friction)
        {
            b2_circle_shape = new Box2DSharp.Collision.Shapes.CircleShape();
            b2_circle_shape.Radius = b2_radius;
            b2_fixture_def = new FixtureDef();
            b2_fixture_def.Shape = b2_circle_shape;
            b2_fixture_def.Density = b2_density;
            b2_fixture_def.Friction = b2_friction;
            //attributo body ereditato dalla classe base
            b2_body.CreateFixture(b2_fixture_def);
        }

        protected override void CreateSFMLObject(Color color)
        {
            sfml_circle_shape = new SFML.Graphics.CircleShape();
            sfml_circle_shape.FillColor = color;

            sfml_circle_shape.Radius = b2_radius * GameEngine.PPM;
            //sposto l'origine della figura al centro del rettangolo
            sfml_circle_shape.Origin = new Vector2f(sfml_circle_shape.Radius, sfml_circle_shape.Radius);
            sfml_circle_shape.Position = GameEngine.FromBox2DToSFMLCoordinates(b2_position);
        }

        protected override void UpdateSFMLObject()
        {
            //prende posizione e angolo dal body box2d
            Vector2 box2d_coordinates = b2_body.GetPosition();
            float box2d_angle = b2_body.GetAngle();
            //converte le coordinate e l'angolo per adattarlo ad sfml, tenendo conto della posizione della camera e ppm
            sfml_circle_shape.Radius = b2_radius * GameEngine.PPM;
            sfml_circle_shape.Origin = new Vector2f(sfml_circle_shape.Radius, sfml_circle_shape.Radius); ;
            sfml_circle_shape.Position = GameEngine.FromBox2DToSFMLCoordinates(box2d_coordinates);
            //sfml_circle_shape.Rotation = -box2d_angle * (180.0f / Convert.ToSingle(Math.PI));
        }

        public override void Draw()
        {
            UpdateSFMLObject();
            GameEngine.window.Draw(sfml_circle_shape);
        }
    }
}
