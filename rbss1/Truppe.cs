using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rbss1
{
    public abstract class Truppe
    {
        public Spieler Besitzer {  get; set; }
        public int Preis { get; private set; } = 100;
        public int Bewegungsreichweite { get;  set; }
        public PictureBox textur { get;  set; }
        public Feld AktuellesFeld { get; set; }
        public int Leben { get; set; }
        public int Schaden { get; set; }
        public Truppe()
        {
            Leben = 100;
            textur = new PictureBox
            {
                Size = new Size(50, 50),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackgroundImage = Properties.Resources.grasstransparent
            };
        }
        public void SetzeFeld(Feld neuesFeld)
        {
            AktuellesFeld = neuesFeld;
            textur.BackColor = Besitzer.SpielerFarbe;
        }
        public void EntferneTruppe()
        {
            if (AktuellesFeld != null)
            {
                AktuellesFeld.EntferneTruppe();
            }
            textur.Hide();
        }
        public abstract override string ToString();
        public abstract void Angreifen(Truppe targetTruppe);
        public abstract void Angreifen(Stadt targetStadt);
    }
}
