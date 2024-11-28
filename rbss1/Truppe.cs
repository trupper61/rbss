using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rbss1
{
    public class Truppe
    {
        public Spieler Besitzer {  get; set; }
        public int Bewegungsreichweite { get; private set; }
        public PictureBox Darstellung { get; private set; }
        public Feld AktuellesFeld { get; private set; }
        public int Leben { get; private set; } = 100;
        public int Schaden { get; private set; } = 25;

        public int Preis { get; private set; } = 100;
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
        public void Angreifen(Truppe targetTruppe)
        {
            if (targetTruppe == null) return;

            targetTruppe.Leben -= this.Schaden;

            if (targetTruppe.Leben <= 0)
            {
                targetTruppe.EntferneTruppe();
            }
        }
        public void EntferneTruppe()
        {
            if (AktuellesFeld != null)
            {
                AktuellesFeld.EntferneTruppe();
            }
            Darstellung.Hide();
        }
        public override string ToString()
        {
            return "Nahkampftrupp";
        }
    }
}
