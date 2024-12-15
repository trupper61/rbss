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
        /// <summary>
        /// Methode zum Angreifen einer anderen Truppe
        /// </summary>
        /// <param name="targetTruppe"></param>
        /// <returns></returns>
        public override bool Angreifen(Truppe targetTruppe)
        {
            // Überprüfung, ob Ziel existiert und Besitzer gleich ist
            if (targetTruppe == null && targetTruppe.Besitzer == Besitzer) 
                return false;
            // Überprüfung, ob Ziel sich ein Feld entfernt
            if (BerechneDistanz(targetTruppe.AktuellesFeld) != 1)
                return false;
            targetTruppe.NehmeSchaden(Schaden); // Verursache Schaden
            return true;
        }
        /// <summary>
        /// Methode zum Angreifen einer Stadt
        /// </summary>
        /// <param name="targetStadt"></param>
        /// <returns></returns>
        public override bool Angreifen(Stadt targetStadt)
        {
            if (targetStadt == null || targetStadt.Besitzer == Besitzer)
                return false;
            if ((BerechneDistanz(targetStadt.startFeld) != 1))
                return false;
            targetStadt.NehmeSchaden(Schaden);
            return true;
        }
        /// <summary>
        /// Methode zum Angreifen einer Stadt
        /// </summary>
        /// <param name="targetSquad"></param>
        /// <returns></returns>
        public override bool Angreifen(Squad targetSquad)
        {
            if (targetSquad == null || Besitzer == targetSquad.Besitzer)
                return false;
            if (BerechneDistanz(targetSquad.AktuellesFeld) != 1)
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
