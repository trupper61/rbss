using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace rbss1
{
    public class Farm
    {
        public Spieler Besitzer { get; set; }
        public List<Feld> farmFlaeche { get; set; }
        public PictureBox textur { get; private set; }
        public Farm farm { get; private set; }
        public Feld[,] felder { get; set; }
        public Feld startFeld { get; set; }

        public int weizenEinkommen { get; set; } = 10;

        public Farm(Feld startFeld, Feld[,] felder)
        {
            this.felder = felder;
            this.startFeld = startFeld;



            farmFlaeche = new List<Feld>();
            textur = new PictureBox
            {
                Size = new Size(40, 40),
                BackColor = Color.Green,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Properties.Resources.wheatinventory

            };
        }

        public void SetzeFarm(Stadt stadt)
        {
            this.farm = farm;
        }
        public void EntferneFarm()
        {
            this.farm = null;
        }
    }
}
