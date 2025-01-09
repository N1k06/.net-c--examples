using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulazioneGravita
{
    internal class Universo
    {
        float G = 5;

        public List<Pianeta> pianeti = new List<Pianeta>();

        private Vettore CalcolaForza(Pianeta p1, Pianeta p2)
        {
            // gravità newtoniana in forma vettoriale
            Vettore r21 = p2.p - p1.p;
            float num = - G * (p1.massa * p2.massa);
            float den = (float) Math.Pow((float)~r21, 3);
            Vettore f =  r21 * (num / den);

            return f;
        }

        public void CalcolaGravitaPianeti()
        {
            //scorre la lista e calcola la forza tra tutte le coppie di pianeti
            for (int i = 0; i < pianeti.Count; i++)
                for (int j = 0; j < pianeti.Count; j++)
                    if (i != j)
                    {
                        Vettore forza = CalcolaForza(pianeti[i], pianeti[j]);
                        pianeti[i].ApplicaForza(forza);
                    }
           //aggiorna posizione e velocità dei pianeti
        }

    }
}
