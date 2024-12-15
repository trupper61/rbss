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
        public Stahlwerk StahlwerkAufFeld{ get; set; }
        public bool GehoertZuStadt { get; set; } = false;
        public Rescourcen rescourcen { get; set; }
        public Spieler besitzer {  get; set; }
        public Feld() 
        {
            TruppeAufFeld = null;
            SquadAufFeld = null;
            textur = new PictureBox();
        }
        /// <summary>
        /// Methode zum platzieren einer Truppe auf ein Feld
        /// </summary>
        /// <param name="truppe"></param>
        /// <param name="spieler"></param>
        public void SetzeTruppe(Truppe truppe, Spieler spieler)
        {
            // Entfernt die Truppe von ihrem aktuellen Feld, falls sie bereits auf ein Feld ist
            if (truppe.AktuellesFeld != null)
            {
                truppe.AktuellesFeld.EntferneTruppe();
            } 
            truppe.Besitzer = spieler;
            TruppeAufFeld = truppe;
            truppe.SetzeFeld(this);
            // Berechnet Position der Truppe 
            int x = (textur.Width - truppe.textur.Width) / 2;
            int y = (textur.Height - truppe.textur.Height) / 2;
            truppe.textur.Location = new Point(textur.Location.X + x, textur.Location.Y + y);

            // Fügt Truppengrafik zur UI hinzu, fall sie noch nicht da ist
            if (!textur.Parent.Controls.Contains(truppe.textur))
            {
                textur.Parent.Controls.Add(truppe.textur);
            }
            truppe.textur.BringToFront(); // Bringt Truppe in den Vordergrund
        }
        /// <summary>
        /// Methode zum Entfernen einer Trupp von dem aktuellen Feld
        /// </summary>
        public void EntferneTruppe()
        {
            if (TruppeAufFeld != null)
            {
                // Entfernt Truppe aus UI, falls sie dort existiert
                if (TruppeAufFeld.textur.Parent != null)
                {
                    TruppeAufFeld.textur.Parent.Controls.Remove(TruppeAufFeld.textur);
                }
                TruppeAufFeld = null; // Setzt Truppenreferenz auf null
            }
        }
        /// <summary>
        /// Methode zum Entfernen eines Squads von dem aktuellen Feld
        /// </summary>
        public void EntferneSquad() 
        {
            if (SquadAufFeld != null)
            {
                // Entfernt den Squad aus UI, falls dort vorhanden
                if (SquadAufFeld.textur.Parent != null)
                {
                    SquadAufFeld.textur.Parent.Controls.Remove(SquadAufFeld.textur);
                }
                SquadAufFeld = null; // Setzt Squadreferenz auf null
            }
        }
        /// <summary>
        /// Methode zum Entfernen der Stadt von diesem Feld
        /// </summary>
        public void EntferneStadt()
        {
            if (StadtAufFeld != null)
            {
                // Entfernt Stadt von UI, falls dort vorhanden
                if (StadtAufFeld.textur.Parent != null)
                {
                    StadtAufFeld.textur.Parent.Controls.Remove(StadtAufFeld.textur);
                }
            }
        }
    }
}
