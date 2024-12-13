using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rbss1
{
    public class Nahkaempfer : Truppe   
    {
        public Nahkaempfer()
        {
            Bewegungsreichweite = 2;
            Schaden = 35;
            textur.Image = Properties.Resources.melee_character;

        }
        public override bool Angreifen(Truppe targetTruppe)
        {
            if ((targetTruppe == null || targetTruppe.Besitzer == Besitzer) && (BerechneDistanz(targetTruppe.AktuellesFeld) == 1))
                return false;
            targetTruppe.NehmeSchaden(Schaden);
            return true;
        }
        public override bool Angreifen(Stadt targetStadt)
        {
            if ((targetStadt == null || targetStadt.Besitzer == Besitzer) && (BerechneDistanz(targetStadt.startFeld) == 1))
                return false;
            targetStadt.NehmeSchaden(Schaden);
            return true;
        }
        public override bool Angreifen(Squad targetSquad)
        {
            if ((targetSquad == null || Besitzer == targetSquad.Besitzer) && (BerechneDistanz(targetSquad.AktuellesFeld) == 1))
                return false;
            targetSquad.NehmeSchaden(Schaden);
            return true;
        }
        public override string ToString()
        {
            return "Nahkämpfer";
        }
    }
}
