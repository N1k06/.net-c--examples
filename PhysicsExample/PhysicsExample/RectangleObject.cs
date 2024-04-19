using System;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;
using System.Collections.Generic;

using SFML;
using SFML.Window;
using SFML.System;
using SFML.Graphics;

using Box2DSharp;
using Box2DSharp.Common;
using Box2DSharp.Dynamics;
using Box2DSharp.Collision;
using Box2DSharp.Collision.Shapes;

namespace PhysicsExample
{
    class RectangleObject
    {
        //oggetto SFML per visualizzare il rettangolo
        protected RectangleShape rectangle_shape;
        RenderWindow window;

        //oggetti Box2D per gestire la fisica
        protected World world;
        protected BodyDef body_def;
        protected Body body;
        protected PolygonShape dynamic_box;
        protected FixtureDef fixture_def;

        //posizione dell'oggetto nelle coordinate di gioco
        protected Vector2f pos;
        protected Vector2f dim;
        protected float angle;

        //PointToMeters: fattore di scala per zoommare 
        //le coordinate del mondo fisico. senza di questo
        //la simulazione risulterebbe troppo lenta nei movimenti.
        //infatti, Box2D utilizza dei mondi con scala ridotta, 
        //non adatti alla scala delle normali applicazioni grafiche
        protected int PTM = 100;

        SFML.Graphics.Color color;

        public RectangleObject(in World world, in RenderWindow window, Vector2f pos, Vector2f dim, BodyType body_type, SFML.Graphics.Color color)
        {
            this.world = world;
            this.window = window;
            this.pos = pos;
            this.dim = dim;
            this.angle = 0.0f;
            this.color = color;

            CreateSFMLObject();
            CreateBox2DObject(body_type);
        }

        protected void CreateSFMLObject()
        {
            this.rectangle_shape = new RectangleShape();
            this.rectangle_shape.Size = this.dim;
            //sposto l'origine della figura al centro del rettangolo
            this.rectangle_shape.Origin = new Vector2f(this.dim.X/2, this.dim.Y/2);
            this.rectangle_shape.Position = this.pos;
            this.rectangle_shape.FillColor = color;
        }

        protected void CreateBox2DObject(BodyType body_type)
        {
            this.body_def.BodyType = body_type;
            //converto nella posizione dell'engine fisico (scala e inversione asse y)
            this.body_def.Position = new Vector2(this.pos.X/PTM, -this.pos.Y/PTM);
            this.body = world.CreateBody(in this.body_def);
            this.dynamic_box = new PolygonShape();
            this.dynamic_box.SetAsBox(this.dim.X/(2*PTM), this.dim.Y/(2*PTM));
            this.fixture_def = new FixtureDef();
            this.fixture_def.Shape = this.dynamic_box;
            this.fixture_def.Density = 1f;
            this.fixture_def.Friction = 0.3f;
            this.body.CreateFixture(this.fixture_def);
        }

        public void UpdateSFMLObject()
        {
            //ottiene posizione e angolo dall'engine fisico
            //e li uso per aggiornare posizione e angolo dell'oggetto grafico
            //i due sistemi di riferimento (finestra e mondo) hanno Y invertita e fattore di scala PTM
            this.pos = new Vector2f(this.body.GetPosition().X*PTM, -this.body.GetPosition().Y*PTM);
            this.rectangle_shape.Position = this.pos;

            //sfml usa gradi, box2d radianti. devo convertire!
            this.angle = this.body.GetAngle() * (180.0f / Convert.ToSingle(Math.PI));
            this.rectangle_shape.Rotation = this.angle;
        }

        public void DrawSFMLObject()
        {
            this.window.Draw(this.rectangle_shape);
        }

        public void ApplyLinearImpulse(Vector2 f)
        {
            this.body.ApplyLinearImpulseToCenter(f, true);
        }
    }
}
