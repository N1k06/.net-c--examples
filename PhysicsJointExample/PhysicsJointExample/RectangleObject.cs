using Box2DSharp.Collision.Shapes;
using Box2DSharp.Dynamics;
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
    class RectangleObject : GameObject
    {
        Vector2 b2_size;
        float b2_angle;
        PolygonShape b2_polygon_shape;
        FixtureDef b2_fixture_def;

        RectangleShape sfml_rectangle_shape;

        public RectangleObject(bool is_static, float b2_width, float b2_height, float b2_position_x, float b2_position_y, float b2_angle, float b2_friction, float b2_density, Color color):base(is_static, b2_position_x, b2_position_y)
        {
            this.b2_size = new Vector2(b2_width, b2_height);
            this.b2_angle = b2_angle;
            //richiama i metodi per la creazione degli oggetti SFML e Box2D
            CreateBox2DObject(b2_density, b2_friction);
            CreateSFMLObject(color);
        }

        protected override void CreateBox2DObject(float b2_density, float b2_friction)
        {
            b2_polygon_shape = new PolygonShape();
            b2_polygon_shape.SetAsBox(b2_size.X / 2, b2_size.Y / 2);
            b2_fixture_def = new FixtureDef();
            b2_fixture_def.Shape = b2_polygon_shape;
            b2_fixture_def.Density = b2_density;
            b2_fixture_def.Friction = b2_friction;
            //attributo body ereditato dalla classe base
            b2_body.CreateFixture(b2_fixture_def);
            b2_body.SetTransform(b2_body.GetPosition(), b2_angle);
        }

        protected override void CreateSFMLObject(Color color)
        {
            sfml_rectangle_shape = new RectangleShape();
            sfml_rectangle_shape.FillColor = color;

            sfml_rectangle_shape.Size = new Vector2f(b2_size.X, b2_size.Y)*GameEngine.PPM;
            //sposto l'origine della figura al centro del rettangolo
            sfml_rectangle_shape.Origin = sfml_rectangle_shape.Size / 2;
            sfml_rectangle_shape.Position = GameEngine.FromBox2DToSFMLCoordinates(b2_position);
            sfml_rectangle_shape.Rotation = -b2_body.GetAngle() * (180.0f / Convert.ToSingle(Math.PI));
        }

        protected override void UpdateSFMLObject()
        {
            //prende posizione e angolo dal body box2d
            Vector2 box2d_coordinates = b2_body.GetPosition();
            float box2d_angle = b2_body.GetAngle();
            //converte le coordinate e l'angolo per adattarlo ad sfml, tenendo conto della posizione della camera e ppm
            sfml_rectangle_shape.Size = new Vector2f(b2_size.X, b2_size.Y) * GameEngine.PPM;
            sfml_rectangle_shape.Origin = sfml_rectangle_shape.Size / 2;
            sfml_rectangle_shape.Position = GameEngine.FromBox2DToSFMLCoordinates(box2d_coordinates);
            sfml_rectangle_shape.Rotation = - box2d_angle * (180.0f / Convert.ToSingle(Math.PI));
        }

        public override void Draw()
        {
            UpdateSFMLObject();
            GameEngine.window.Draw(sfml_rectangle_shape);
        }
    }
}
