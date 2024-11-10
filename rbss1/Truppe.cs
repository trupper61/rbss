using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rbss1
{
    internal class Truppe
    {
        public int Bewegungsreichweite { get; private set; }
        public PictureBox Darstellung { get; private set; }
        public Feld AktuellesFeld { get; private set; }

        public Truppe()
        {
            Bewegungsreichweite = 2;
            Darstellung = new PictureBox
            {
                Size = new Size(40, 40),
                BackColor = Color.Blue
            };
        }
        public void SetzeFeld(Feld neuesFeld)
        {
            AktuellesFeld = neuesFeld;
        }
    }
}
