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
        public override void Angreifen(Truppe targetTruppe)
        {
            if (targetTruppe == null || targetTruppe.Besitzer == Besitzer)
                return;
            targetTruppe.NehmeSchaden(Schaden);
        }
        public override void Angreifen(Stadt targetStadt)
        {
            if (targetStadt == null || Besitzer.staedteBesitz.Contains(targetStadt))
                return;
            targetStadt.Leben -= Schaden;
            if (targetStadt.Leben <= 0)
            {
                targetStadt.EntferneStadt();
            }
        }
        public override void Angreifen(Squad targetSquad)
        {
            if (targetSquad == null || Besitzer == targetSquad.Besitzer)
                return;
            targetSquad.NehmeSchaden(Schaden);
        }
        public override string ToString()
        {
            return "Nahkämpfer";
        }
    }
}
