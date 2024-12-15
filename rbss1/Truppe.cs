using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rbss1
{
    // Abstrakte Klasse "Truppe" dient als Basisklasse für spezifische Truppentypen
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
        /// <summary>
        /// Setzt das Feld, auf dem sich Truppe befindet, ändert Farbe basierend auf die des Spielers
        /// </summary>
        /// <param name="neuesFeld"></param>
        public void SetzeFeld(Feld neuesFeld)
        {
            AktuellesFeld = neuesFeld;
            textur.BackColor = Besitzer.SpielerFarbe;
        }
        /// <summary>
        /// Berechnet Manhattan-Distanz von aktueller Position zu einem Ziel feld
        /// </summary>
        /// <param name="ziel"></param>
        /// <returns></returns>
        public int BerechneDistanz(Feld ziel)
        {
            return Math.Abs(AktuellesFeld.position.X - ziel.position.X) + Math.Abs(AktuellesFeld.position.Y - ziel.position.Y);
        }
        /// <summary>
        /// Entfernt die Truppen vom aktuellen Feld und versteckt die Grafik
        /// </summary>
        public void EntferneTruppe()
        {
            if (AktuellesFeld != null)
            {
                AktuellesFeld.EntferneTruppe();
            }
            textur.Hide();
        }
        /// <summary>
        /// Reduziert Lebenspunkte der Truppe und entfernt sie wenn sie auf 0 fällt
        /// </summary>
        /// <param name="schaden"></param>
        public void NehmeSchaden(int schaden)
        {
            Leben -= schaden;
            if (Leben <= 0)
                EntferneTruppe();
        }
        public abstract override string ToString();
        public abstract bool Angreifen(Truppe targetTruppe);
        public abstract bool Angreifen(Stadt targetStadt);
        public abstract bool Angreifen(Squad targetSquad);
    }
}
