
namespace FlappityFlap
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pbPersonaggio = new System.Windows.Forms.PictureBox();
            this.pbOstacoloTop = new System.Windows.Forms.PictureBox();
            this.pbOstacoloBottom = new System.Windows.Forms.PictureBox();
            this.pbSfondo1 = new System.Windows.Forms.PictureBox();
            this.timerAggiorna = new System.Windows.Forms.Timer(this.components);
            this.lbPunteggio = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbPersonaggio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOstacoloTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOstacoloBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSfondo1)).BeginInit();
            this.SuspendLayout();
            // 
            // pbPersonaggio
            // 
            this.pbPersonaggio.BackColor = System.Drawing.Color.Transparent;
            this.pbPersonaggio.Image = global::FlappityFlap.Properties.Resources.bird;
            this.pbPersonaggio.Location = new System.Drawing.Point(301, 278);
            this.pbPersonaggio.Name = "pbPersonaggio";
            this.pbPersonaggio.Size = new System.Drawing.Size(65, 50);
            this.pbPersonaggio.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPersonaggio.TabIndex = 0;
            this.pbPersonaggio.TabStop = false;
            // 
            // pbOstacoloTop
            // 
            this.pbOstacoloTop.BackColor = System.Drawing.Color.Transparent;
            this.pbOstacoloTop.Image = global::FlappityFlap.Properties.Resources.pipe2;
            this.pbOstacoloTop.Location = new System.Drawing.Point(846, -232);
            this.pbOstacoloTop.Name = "pbOstacoloTop";
            this.pbOstacoloTop.Size = new System.Drawing.Size(150, 430);
            this.pbOstacoloTop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbOstacoloTop.TabIndex = 1;
            this.pbOstacoloTop.TabStop = false;
            // 
            // pbOstacoloBottom
            // 
            this.pbOstacoloBottom.BackColor = System.Drawing.Color.Black;
            this.pbOstacoloBottom.Image = global::FlappityFlap.Properties.Resources.pipe1;
            this.pbOstacoloBottom.Location = new System.Drawing.Point(846, 400);
            this.pbOstacoloBottom.Name = "pbOstacoloBottom";
            this.pbOstacoloBottom.Size = new System.Drawing.Size(150, 430);
            this.pbOstacoloBottom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbOstacoloBottom.TabIndex = 2;
            this.pbOstacoloBottom.TabStop = false;
            // 
            // pbSfondo1
            // 
            this.pbSfondo1.Image = global::FlappityFlap.Properties.Resources.background;
            this.pbSfondo1.Location = new System.Drawing.Point(0, 0);
            this.pbSfondo1.Name = "pbSfondo1";
            this.pbSfondo1.Size = new System.Drawing.Size(1024, 768);
            this.pbSfondo1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSfondo1.TabIndex = 3;
            this.pbSfondo1.TabStop = false;
            // 
            // timerAggiorna
            // 
            this.timerAggiorna.Enabled = true;
            this.timerAggiorna.Interval = 20;
            this.timerAggiorna.Tick += new System.EventHandler(this.timerAggiorna_Tick);
            // 
            // lbPunteggio
            // 
            this.lbPunteggio.AutoSize = true;
            this.lbPunteggio.Font = new System.Drawing.Font("Arial", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbPunteggio.Location = new System.Drawing.Point(666, 0);
            this.lbPunteggio.Name = "lbPunteggio";
            this.lbPunteggio.Size = new System.Drawing.Size(193, 40);
            this.lbPunteggio.TabIndex = 4;
            this.lbPunteggio.Text = "Punteggio:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.lbPunteggio);
            this.Controls.Add(this.pbOstacoloBottom);
            this.Controls.Add(this.pbOstacoloTop);
            this.Controls.Add(this.pbPersonaggio);
            this.Controls.Add(this.pbSfondo1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pbPersonaggio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOstacoloTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOstacoloBottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSfondo1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbPersonaggio;
        private System.Windows.Forms.PictureBox pbOstacoloTop;
        private System.Windows.Forms.PictureBox pbOstacoloBottom;
        private System.Windows.Forms.PictureBox pbSfondo1;
        private System.Windows.Forms.Timer timerAggiorna;
        private System.Windows.Forms.Label lbPunteggio;
    }
}

