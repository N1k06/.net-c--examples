using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulazioneGravita
{
    internal class Vettore
    {
        public float x;
        public float y;

        public Vettore()
        {
            this.x = 0.0f;
            this.y = 0.0f;
        }
        public Vettore(float a)
        {
            this.x = a;
            this.y = a;
        }
        public Vettore(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public static Vettore operator +(Vettore v1, Vettore v2)
        {
            Vettore v3 = new Vettore();
            v3.x = v1.x + v2.x;
            v3.y = v1.y + v2.y;
            return v3;
        }

        public static Vettore operator -(Vettore v1, Vettore v2)
        {
            Vettore v3 = new Vettore();
            v3.x = v1.x - v2.x;
            v3.y = v1.y - v2.y;
            return v3;
        }

        public static Vettore operator -(Vettore v)
        {
            Vettore v2 = new Vettore();
            v2.x = -v.x;
            v2.y = -v.y;

            return v2;
        }
        public static float operator ~(Vettore v)
        {
            float modulo = Convert.ToSingle(Math.Sqrt(v.x * v.x + v.y * v.y));
            return modulo;
        }

        public static Vettore operator *(Vettore v, float scala)
        {
            Vettore v2 = new Vettore();
            v2.x = v.x * scala;
            v2.y = v.y * scala;

            return v2;
        }

        public static float operator *(Vettore v1, Vettore v2)
        {
            float theta1 = v1.Angolo();
            float theta2 = v2.Angolo();
            float theta = theta1 - theta2;

            float prodotto = Convert.ToSingle(Math.Sin(theta)) * (~v1) * (~v2);
            return prodotto;
        }

        public float Angolo()
        {
            float theta = Convert.ToSingle(Math.Acos(x / ~(this)));
            return theta;
        }

    }
}
