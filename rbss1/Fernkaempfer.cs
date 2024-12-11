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
        public override void Angreifen(Truppe targetTruppe)
        {
            if (targetTruppe != null || targetTruppe.Besitzer == Besitzer)
                return;
            int distanz = BerechneDistanz(targetTruppe.AktuellesFeld);
            if (distanz <= Reichweite)
            {
                targetTruppe.NehmeSchaden(Schaden);
            }
        }
        public override void Angreifen(Stadt targetStadt)
        {
            if (targetStadt != null || targetStadt.Besitzer == Besitzer)
                return;
            int distanz = BerechneDistanz(targetStadt.startFeld);
            if (distanz <= Reichweite)
            {
                targetStadt.Leben -= Schaden;
                if (targetStadt.Leben <= 0)
                    targetStadt.EntferneStadt();
            }
        }
        public override void Angreifen(Squad targetSquad)
        {
            if (targetSquad != null || targetSquad.Besitzer == Besitzer)
                return;
        }
        public override string ToString()
        {
            return "Fernkämpfer";
        }
    }
}
