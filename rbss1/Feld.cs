using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rbss1
{
    public class Feld
    {
        public PictureBox textur { get; set; }
        public Truppe TruppeAufFeld { get;  set; }
        public Squad SquadAufFeld { get; set; }
        public string feldart {  get; set; }
        public Point position { get; set; }
        public Stadt StadtAufFeld { get; set; }
        public Farm FarmAufFeld { get; set; }
        public bool GehoertZuStadt { get; set; } = false;
        public Rescourcen rescourcen { get; set; }
        public Spieler besitzer {  get; set; }
        public Feld() 
        {
            TruppeAufFeld = null;
            SquadAufFeld = null;
            textur = new PictureBox();
        }
        public void SetzeTruppe(Truppe truppe, Spieler spieler)
        {
            if (truppe.AktuellesFeld != null)
            {
                truppe.AktuellesFeld.EntferneTruppe();
            } 
            truppe.Besitzer = spieler;
            TruppeAufFeld = truppe;
            truppe.SetzeFeld(this);
            int x = (textur.Width - truppe.textur.Width) / 2;
            int y = (textur.Height - truppe.textur.Height) / 2;
            truppe.textur.Location = new Point(textur.Location.X + x, textur.Location.Y + y);

            if (!textur.Parent.Controls.Contains(truppe.textur))
            {
                textur.Parent.Controls.Add(truppe.textur);
            }
            truppe.textur.BringToFront();
        }
        public void EntferneTruppe()
        {
            if (TruppeAufFeld != null)
            {
                if (TruppeAufFeld.textur.Parent != null)
                {
                    TruppeAufFeld.textur.Parent.Controls.Remove(TruppeAufFeld.textur);
                }
                TruppeAufFeld = null;
            }
        }
        public void EntferneStadt()
        {
            if (StadtAufFeld != null)
            {
                if (StadtAufFeld.textur.Parent != null)
                {
                    StadtAufFeld.textur.Parent.Controls.Remove(StadtAufFeld.textur);
                }
            }
        }
    }
}
