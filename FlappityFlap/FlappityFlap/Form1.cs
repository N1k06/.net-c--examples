using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlappityFlap
{
    public partial class Form1 : Form
    {
        //parametri di gioco
        int obstacleSpeed = 8;
        int gravity = 15;
        int score = 0;

        public Form1()
        {
            InitializeComponent();
        }

        //impostazioni per la corretta visualizzazione delle immagini
        private void Form1_Load(object sender, EventArgs e)
        {
            //setta il colore di sfondo a trasparente
            //agisce sull'alpha channel delle immagini
            pbOstacoloTop.BackColor = Color.Transparent;
            pbOstacoloBottom.BackColor = Color.Transparent;
            pbPersonaggio.BackColor = Color.Transparent;

            //i pixel trasparenti mostrano l'immagine impostata come parent
            pbOstacoloTop.Parent = pbSfondo1;
            pbOstacoloBottom.Parent = pbSfondo1;
            pbPersonaggio.Parent = pbSfondo1;

            //imposta la trasparenza per la label che mostra il punteggio
            lbPunteggio.Parent = pbSfondo1;
            lbPunteggio.BackColor = Color.Transparent;
        }

        //rileva la pressione di un tasto della tastiera
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // rileva la pressione del tasto "spazio"
            if (e.KeyCode == Keys.Space)
            {
                // si inverte la gravità, il personaggio sale invece di cadere
                gravity = -15;
            }
        }
        
        //rileva quando un tasto della tastiera è rilasciato
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            // rileva il rilascio del tasto "spazio"
            if (e.KeyCode == Keys.Space)
            {
                // la gravità torna normale, il personaggio torna a scendere
                gravity = +15;
            }
        }

        private void AggiornaPersonaggio()
        {
            //sposto il personaggio su o giù in base alla gravità
            pbPersonaggio.Top += gravity;

            //aggiorno il punteggio
            lbPunteggio.Text = "Punteggio: " + score;
        }

        private void AggiornaOstacoli()
        {
            //genero un numero casuale non generare i tubi sempre al centro
            Random rnd = new Random();
            int variazioneOstacoli = rnd.Next(0, 200) - 100;

            //sposto gli ostacoli verso sinistra 
            pbOstacoloTop.Left -= obstacleSpeed;
            pbOstacoloBottom.Left -= obstacleSpeed;

            //se uno dei due ostacoli supera il bordo sinistro dello schermo
            if (pbOstacoloTop.Left < -150)
            {
                //resetta la posizione dell'ostacolo a destra dello schermo
                pbOstacoloTop.Left = 1024;
                pbOstacoloTop.Top = -230 + variazioneOstacoli; 

                pbOstacoloBottom.Left = 1024;
                pbOstacoloBottom.Top = 400 + variazioneOstacoli;

                //aumento il punteggio e la velocità degli ostacoli
                score++;
                obstacleSpeed++;
            }
        }
        
        //rileva se il personaggio esce dallo schermo oppure sbatte con uno degli ostacoli
        private void RilevaCollisioni()
        {
            if (pbPersonaggio.Bounds.IntersectsWith(pbOstacoloBottom.Bounds) ||
                pbPersonaggio.Bounds.IntersectsWith(pbOstacoloTop.Bounds) ||
                pbPersonaggio.Top < 0 || pbPersonaggio.Top > 1024)
            {
                GameOver();
            }

        }

        //termina il gioco mostrando un messaggio sul punteggio
        private void GameOver()
        {
            timerAggiorna.Stop();
            lbPunteggio.Text += "\nGame over!";
        }

        //ogni 20ms aggiorna gli oggetti di gioco
        private void timerAggiorna_Tick(object sender, EventArgs e)
        {
            AggiornaPersonaggio();
            AggiornaOstacoli();
            RilevaCollisioni();
        }
    }
}
