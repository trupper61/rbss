using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rbss1
{
    // Klasse Squad repräsentiert eine Gruppe von Truppen, die zusammen agieren
    public class Squad
    {
        public Spieler Besitzer { get; set; }
        public List<Truppe> Mitglieder { get; set; }
        public int Gesamtleben { get; set; }
        public int Gesamtschaden { get; set; }
        public int Bewegungsreichweite { get; set; }
        public Feld AktuellesFeld { get; set; }
        public PictureBox textur { get; set; }
        public string Name { get; set; }
        public Squad(Spieler besitzer, Feld startFeld)
        {
            Besitzer = besitzer;
            Mitglieder = new List<Truppe>();
            AktuellesFeld = startFeld;
            textur = new PictureBox
            {
                Size = new Size(50, 50),
                Location = startFeld.textur.Location,
                BackColor = Besitzer.SpielerFarbe,
                Image = Properties.Resources.squad,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackgroundImage = Properties.Resources.grasstransparent
            };
            AktualisiereAttribute();   
        }
        /// <summary>
        /// Setzt Squad auf ein neues Feld
        /// </summary>
        /// <param name="feld"></param>
        public void SetzeFeld(Feld feld)
        {
            AktuellesFeld = feld;
            if (textur != null)
            {
                textur.Location = feld.textur.Location;
                textur.BringToFront();
                textur.Show();
            }
        }
        /// <summary>
        /// Aktualisiert die Attribute  des Squads basierend auf die Mitglieder
        /// </summary>
        public void AktualisiereAttribute()
        {
            Gesamtschaden = 0; // Zurücksetzen Gesamtschaden
            Gesamtleben = 0; // Zurücksetzen Gesamtleben

            if (Mitglieder.Count > 0)
            {
                Bewegungsreichweite = Mitglieder[0].Bewegungsreichweite; // Setzt Bewegungsreichweite auf erste Truppe
            }
            else
            {
                Bewegungsreichweite = 0;
            }
            // Berechnet Gesamtschaden/Gesamtleben des Squads
            foreach (Truppe truppe in Mitglieder)
            {
                Gesamtleben += truppe.Leben;
                Gesamtschaden += truppe.Schaden;
                if (truppe.Bewegungsreichweite < Bewegungsreichweite) // Bewegungsreichweite ist die min. Reichweite aller Truppen
                {
                    Bewegungsreichweite = truppe.Bewegungsreichweite;
                }
            }
        }
        /// <summary>
        /// Bewegt das Squad zu Ziel-Feld
        /// </summary>
        /// <param name="zielFeld"></param>
        public void BewegeZu(Feld zielFeld)
        {
            AktuellesFeld.TruppeAufFeld = null;
            SetzeFeld(zielFeld);
            zielFeld.SquadAufFeld = this;

            textur.Location = zielFeld.textur.Location;
            // Bewegt alle Truppen im Squad zu neuem Feld
            foreach (Truppe truppe in Mitglieder)
            {
                truppe.AktuellesFeld = zielFeld;
            }
        }
        /// <summary>
        /// Fügt einer Truppe zum Squad hinzu
        /// </summary>
        /// <param name="truppe"></param>
        public void TruppeHinzufuegen(Truppe truppe)
        {
            if (truppe.Besitzer == Besitzer)
            {
                Mitglieder.Add(truppe);
                AktualisiereAttribute();
            }
        }
        /// <summary>
        /// Entfernt eine Truppe aus dem Squad
        /// </summary>
        /// <param name="truppe"></param>
        public void EntferneTruppe(Truppe truppe)
        {
            Mitglieder.Remove(truppe);
            AktualisiereAttribute();
            // Wenn Squad keine Mitglieder hat
            if (Mitglieder.Count == 0)
            {
                AktuellesFeld.SquadAufFeld = null; // Squad wir gelöscht
                textur.Hide();
            }
        }
        /// <summary>
        /// Squad nimmt Schaden, der auf Truppen aufgeteilt wird.
        /// </summary>
        /// <param name="schaden"></param>
        public void NehmeSchaden (int schaden)
        {
            int schadenRemaining = schaden;
            List<Truppe> zuEntfernen = new List<Truppe>();

            foreach(Truppe truppe in Mitglieder)
            {
                if (schadenRemaining <= 0)
                    break;
                int originalLeben = truppe.Leben;
                truppe.Leben -= schadenRemaining;
                schadenRemaining -= originalLeben; 
                if (truppe.Leben <= 0)
                    zuEntfernen.Add(truppe); // Wenn die Truppe gestorben ist, wird sie zur List zuentfernender Truppen hinzugefügt
            }
            foreach(Truppe truppe in zuEntfernen)
            {
                EntferneTruppe(truppe); // Entfernt gestorbene Truppe aus Squad
                truppe.EntferneTruppe();
            }
        }
        /// <summary>
        /// Berechnet den tatsächlichen Schaden des Squads auf ein Ziel-Feld
        /// </summary>
        /// <param name="targetFeld"></param>
        /// <returns></returns>
        public int TrueDamage(Feld targetFeld)
        {
            int aktuellerSchaden = Gesamtschaden;
            foreach (Truppe truppe in Mitglieder)
            {
                // Schaden von Nahkämpfer wird reduziert, wenn Ziel nicht Distanz von eins hat
                if (truppe is Nahkaempfer && !(BerechneDistanz(targetFeld) == 1))
                {
                    aktuellerSchaden -= truppe.Schaden;
                }
                // Schaden von Fernkämpfer wird reduziert, wenn Ziel nicht in ihrer Reichweite ist
                else if (truppe is Fernkaempfer fernkampf && !(BerechneDistanz(targetFeld) <= fernkampf.Reichweite))
                {
                    aktuellerSchaden -= truppe.Schaden;
                }
            }
            return aktuellerSchaden;
        }
        /// <summary>
        /// Überladene Methode zum Angreifen eines Squads
        /// </summary>
        /// <param name="gegnerSquad"></param>
        /// <returns></returns>
        public bool Angreifen (Squad gegnerSquad)
        {
            if (gegnerSquad.Besitzer == Besitzer)
                return false;
            int aktuellerSchaden = TrueDamage(gegnerSquad.AktuellesFeld);
            if (aktuellerSchaden <= 0) // Wenn Schaden 0 ist -> Ziel zu weit entfernt
                return false;
            gegnerSquad.NehmeSchaden(aktuellerSchaden);
            return true;
        }
        // Überladene Methode zum Angreifen einer Truppe
        public bool Angreifen (Truppe gegnerTruppe)
        {
            if (gegnerTruppe.Besitzer == Besitzer)
                return false;
            int aktuellerSchaden = TrueDamage(gegnerTruppe.AktuellesFeld);
            if (aktuellerSchaden <= 0)
                return false;
            gegnerTruppe.NehmeSchaden(aktuellerSchaden);
            return true;
        }
        // Überladene Methode zum Angreifen einer Stadt
        public bool Angreifen(Stadt targetStadt)
        {
            if (targetStadt == null || Besitzer.staedteBesitz.Contains(targetStadt))
                return false;
            int aktuellerSchaden = TrueDamage(targetStadt.startFeld);
            if (aktuellerSchaden <= 0)
                return false;
            targetStadt.NehmeSchaden(aktuellerSchaden);
            return true;
        }
        public int BerechneDistanz(Feld ziel)
        {
            //Felder, die der Squad belaufen kann, wird berechnet
            return Math.Abs(AktuellesFeld.position.X - ziel.position.X) + Math.Abs(AktuellesFeld.position.Y - ziel.position.Y);
        }
        public override string ToString()
        {
            return $"Tactical Squad";
        }
    }
}
