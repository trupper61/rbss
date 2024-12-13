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
        public void AktualisiereAttribute()
        {
            Gesamtschaden = 0;
            Gesamtleben = 0;

            if (Mitglieder.Count > 0)
            {
                Bewegungsreichweite = Mitglieder[0].Bewegungsreichweite;
            }
            else
            {
                Bewegungsreichweite = 0;
            }
            foreach (Truppe truppe in Mitglieder)
            {
                Gesamtleben += truppe.Leben;
                Gesamtschaden += truppe.Schaden;
                if (truppe.Bewegungsreichweite < Bewegungsreichweite)
                {
                    Bewegungsreichweite = truppe.Bewegungsreichweite;
                }
            }
        }
        public void BewegeZu(Feld zielFeld)
        {
                AktuellesFeld.TruppeAufFeld = null;
                SetzeFeld(zielFeld);
                zielFeld.SquadAufFeld = this;

                textur.Location = zielFeld.textur.Location;
                foreach (Truppe truppe in Mitglieder)
                {
                    truppe.AktuellesFeld = zielFeld;
                }
        }
        public void TruppeHinzufuegen(Truppe truppe)
        {
            if (truppe.Besitzer == Besitzer)
            {
                Mitglieder.Add(truppe);
                AktualisiereAttribute();
            }
        }
        public void Bewegen(Feld zielFeld)
        {
            foreach (Truppe truppe in Mitglieder)
            {
                truppe.SetzeFeld(zielFeld);
            }
        }
        public void EntferneTruppe(Truppe truppe)
        {
            Mitglieder.Remove(truppe);
            AktualisiereAttribute();
            if (Mitglieder.Count == 0)
            {
                AktuellesFeld.SquadAufFeld = null;
                textur.Hide();
            }
        }
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
                    zuEntfernen.Add(truppe);
            }
            foreach(Truppe truppe in zuEntfernen)
            {
                EntferneTruppe(truppe);
                truppe.EntferneTruppe();
            }
        }
        public int TrueDamage(Feld targetFeld)
        {
            int aktuellerSchaden = Gesamtschaden;
            foreach (Truppe truppe in Mitglieder)
            {
                if (truppe is Nahkaempfer && !(BerechneDistanz(targetFeld) == 1))
                {
                    aktuellerSchaden -= truppe.Schaden;
                }
                else if (truppe is Fernkaempfer fernkampf && !(BerechneDistanz(targetFeld) <= fernkampf.Reichweite))
                {
                    aktuellerSchaden -= truppe.Schaden;
                }
            }
            return aktuellerSchaden;
        }
        public bool Angreifen (Squad gegnerSquad)
        {
            if (gegnerSquad.Besitzer == Besitzer)
                return false;
            int aktuellerSchaden = TrueDamage(gegnerSquad.AktuellesFeld);
            if (aktuellerSchaden <= 0)
                return false;
            gegnerSquad.NehmeSchaden(aktuellerSchaden);
            return true;
        }
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
            return Math.Abs(AktuellesFeld.position.X - ziel.position.X) + Math.Abs(AktuellesFeld.position.Y - ziel.position.Y);
        }
        public override string ToString()
        {
            return $"Tactical Squad";
        }
    }
}
