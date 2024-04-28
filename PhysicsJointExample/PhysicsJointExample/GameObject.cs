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
    abstract class GameObject
    {
        protected Vector2 b2_position;
        protected BodyDef b2_body_def = new BodyDef();
        public Body b2_body;

        public GameObject(bool b2_is_static, float b2_position_x, float b2_position_y)
        {
            b2_position = new Vector2(b2_position_x, b2_position_y);
            b2_body_def.BodyType = b2_is_static ? BodyType.StaticBody : BodyType.DynamicBody;
            b2_body_def.Position = new Vector2(b2_position_x, b2_position_y);
            b2_body = GameEngine.b2_world.CreateBody(in b2_body_def);
        }

        //allinea gli oggetti grafici con quelli del mondo fisico
        protected abstract void UpdateSFMLObject();

        //istanzia gli attributi necessari al motore fisico Box2D
        protected abstract void CreateBox2DObject(float b2_density, float b2_friction);

        //istanzia gli attributi necessari al rendering 2d con SFML
        protected abstract void CreateSFMLObject(Color color);

        //disegna la figura sfml
        public abstract void Draw();

    }
}
