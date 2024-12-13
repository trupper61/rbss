using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace rbss1
{
    public class Fernkaempfer : Truppe
    {
        public int Reichweite { get; set; }

        public Fernkaempfer()
        {
            Leben = 80;
            Schaden = 20;
            Reichweite = 3;
            Bewegungsreichweite = 2;
            textur.Image = Properties.Resources.ranged_character;
        }
        public override bool Angreifen(Truppe targetTruppe)
        {
            if ((targetTruppe != null || targetTruppe.Besitzer == Besitzer) && (BerechneDistanz(targetTruppe.AktuellesFeld) <= Reichweite))
                return false;
            targetTruppe.NehmeSchaden(Schaden);
            return true;
        }
        public override bool Angreifen(Stadt targetStadt)
        {
            if ((targetStadt != null || targetStadt.Besitzer == Besitzer) && (BerechneDistanz(targetStadt.startFeld) <= Reichweite))
                return false;
            targetStadt.NehmeSchaden(Schaden);
            return true;
        }
        public override bool Angreifen(Squad targetSquad)
        {
            if ((targetSquad != null || targetSquad.Besitzer == Besitzer) && (BerechneDistanz(targetSquad.AktuellesFeld) <= Reichweite))
                return false;
            targetSquad.NehmeSchaden(Schaden);
            return true;
        }
        public override string ToString()
        {
            return "Fernkämpfer";
        }
    }
}
