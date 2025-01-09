using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulazioneGravita
{
    internal class Pianeta
    {
        public float massa;

        public Vettore a; // a0 = f0/m
        public Vettore v; // v1 = v0 + a1*Dt 
        public Vettore p; // p1 = p0 + v1*Dt

        public Pianeta(Vettore p, float massa)
        {
            a = new Vettore(0);
            v = new Vettore(0);
            this.p = p;
            this.massa = massa;
        }
        public Vettore CalcolaAccerazione(Vettore f)
        {
            return f * (1 / massa);
        }

        public void CalcolaVelocita(float delta_t)
        {
            v = v + a * delta_t;
        }

        public void CalcolaPosizione(float delta_t)
        {
            p = p + v * delta_t;
        }

        public void ApplicaForza(Vettore forza)
        {
            //converti forza in accelerazione e somma all'accelerazione totale
            Vettore a = CalcolaAccerazione(forza);
            this.a += a;
        }

        public void Aggiorna(float dt)
        { 
            //calcola la nuova posizione del pianeta a partire dall'accelerazione, passando dalla velocità
        }
    }
}
