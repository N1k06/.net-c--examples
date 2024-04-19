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
    class CircleObject
    {
        //oggetto SFML per visualizzare il rettangolo
        protected SFML.Graphics.CircleShape circle_shape;
        RenderWindow window;

        //oggetti Box2D per gestire la fisica
        protected World world;
        protected BodyDef body_def;
        protected Body body;
        protected Box2DSharp.Collision.Shapes.CircleShape dynamic_circle;
        protected FixtureDef fixture_def;

        //posizione dell'oggetto nelle coordinate di gioco
        protected Vector2f pos;
        protected float radius;

        //PointToMeters: fattore di scala per zoommare 
        //le coordinate del mondo fisico. senza di questo
        //la simulazione risulterebbe troppo lenta nei movimenti.
        //infatti, Box2D utilizza dei mondi con scala ridotta, 
        //non adatti alla scala delle normali applicazioni grafiche
        protected int PTM = 100;

        SFML.Graphics.Color color;

        public CircleObject(in World world, in RenderWindow window, Vector2f pos, float radius, BodyType body_type, SFML.Graphics.Color color)
        {
            this.world = world;
            this.window = window;
            this.pos = pos;
            this.radius = radius;
            this.color = color;

            CreateSFMLObject();
            CreateBox2DObject(body_type);
        }

        protected void CreateSFMLObject()
        {
            this.circle_shape = new SFML.Graphics.CircleShape();
            this.circle_shape.Radius = this.radius;
            //sposto l'origine della figura al centro del rettangolo
            this.circle_shape.Origin = new Vector2f(this.radius, this.radius);
            this.circle_shape.Position = this.pos;
            this.circle_shape.FillColor = color;
        }

        protected void CreateBox2DObject(BodyType body_type)
        {
            this.body_def.BodyType = body_type;
            //converto nella posizione dell'engine fisico (scala e inversione asse y)
            this.body_def.Position = new Vector2(this.pos.X / PTM, -this.pos.Y / PTM);
            this.body = world.CreateBody(in this.body_def);
            this.dynamic_circle = new Box2DSharp.Collision.Shapes.CircleShape();
            this.dynamic_circle.Radius = this.radius / (PTM);
            this.fixture_def = new FixtureDef();
            this.fixture_def.Shape = this.dynamic_circle;
            this.fixture_def.Density = 1f;
            this.fixture_def.Friction = 0.3f;
            this.body.CreateFixture(this.fixture_def);
        }

        public void UpdateSFMLObject()
        {
            //ottiene posizione e angolo dall'engine fisico
            //e li uso per aggiornare posizione e angolo dell'oggetto grafico
            //i due sistemi di riferimento (finestra e mondo) hanno Y invertita e fattore di scala PTM
            this.pos = new Vector2f(this.body.GetPosition().X * PTM, -this.body.GetPosition().Y * PTM);
            this.circle_shape.Position = this.pos;
        }

        public void DrawSFMLObject()
        {
            this.window.Draw(this.circle_shape);
        }

        public void ApplyLinearImpulse(Vector2 f)
        {
            this.body.ApplyLinearImpulseToCenter(f, true);
        }
    }
}
