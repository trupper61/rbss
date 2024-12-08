using System;
using System.Collections.Generic;
using System.Linq;
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
            int distanz = Math.Abs(AktuellesFeld.position.X - targetTruppe.AktuellesFeld.position.X) + Math.Abs(AktuellesFeld.position.Y - targetTruppe.AktuellesFeld.position.Y);
            if (distanz <= Reichweite)
            {
                targetTruppe.Leben -= Schaden;

                if (targetTruppe.Leben <= 0)
                {
                    targetTruppe.EntferneTruppe();
                }
            }
        }
        public override void Angreifen(Stadt targetStadt)
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return "Fernkämpfer";
        }
    }
}
