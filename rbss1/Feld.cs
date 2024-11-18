﻿using System;
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
        public Truppe TruppeAufFeld { get; private set; }

        public string feldart {  get; set; }
        public Point position { get; set; }
        public Stadt StadtAufFeld { get; set; }
        public bool GehoertZuStadt { get; set; } = false;

        public Rescourcen rescourcen { get; set; }
        public Feld() 
        {
            textur = new PictureBox();
        }
        public void SetzeTruppe(Truppe truppe, Spieler spieler1)
        {
            if (truppe.AktuellesFeld != null)
            {
                truppe.AktuellesFeld.EntferneTruppe();
            }
            truppe.Besitzer = spieler1;
            TruppeAufFeld = truppe;
            truppe.SetzeFeld(this);
            int x = (textur.Width - truppe.Darstellung.Width) / 2;
            int y = (textur.Height - truppe.Darstellung.Height) / 2;
            truppe.Darstellung.Location = new Point(textur.Location.X + x, textur.Location.Y + y);
            truppe.Darstellung.BringToFront();
        }
        public void EntferneTruppe()
        {
            TruppeAufFeld = null;
        }
    }
}
