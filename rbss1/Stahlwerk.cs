using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rbss1
{
    public class Stahlwerk
    {
        public Spieler Besitzer { get; set; }
        public List<Feld> stahlFlaeche { get; set; }
        public PictureBox textur { get; private set; }
        public Stahlwerk stahlwerk { get; private set; }
        public Feld[,] felder { get; set; }
        public Feld startFeld { get; set; }

        public int StahlEinkommen { get; set; } = 30;

        public Stahlwerk(Feld startFeld, Feld[,] felder)
        {
            this.felder = felder;
            this.startFeld = startFeld;



            stahlFlaeche = new List<Feld>();
            textur = new PictureBox
            {
                Size = new Size(40, 40),
                BackColor = Color.Green,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Properties.Resources.stahlinventory

            };
        }

        public void SetzeStahlwerk(Stadt stadt)
        {
            this.stahlwerk = stahlwerk;
        }
        public void EntferneStahlwerk()
        {
            this.stahlwerk = null;
        }
    }
}
