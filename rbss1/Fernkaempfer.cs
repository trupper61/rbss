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
        public int Reichweite { get; set; } // Neue Variable, zur Bestimmung der max. Reichweite

        public Fernkaempfer()
        {
            Leben = 80;
            Schaden = 20;
            Reichweite = 3;
            Bewegungsreichweite = 2;
            textur.Image = Properties.Resources.ranged_character;
        }
        /// <summary>
        /// Überladene Methode zum Angrefen Truppe
        /// </summary>
        /// <param name="targetTruppe"></param>
        /// <returns></returns>
        public override bool Angreifen(Truppe targetTruppe)
        {
            if (targetTruppe == null || targetTruppe.Besitzer == Besitzer)
                return false;
            if (BerechneDistanz(targetTruppe.AktuellesFeld) > Reichweite)
                return false;
            targetTruppe.NehmeSchaden(Schaden);
            return true;
        }
        /// <summary>
        /// Überladene Methode zum Angreifen Stadt
        /// </summary>
        /// <param name="targetStadt"></param>
        /// <returns></returns>
        public override bool Angreifen(Stadt targetStadt)
        {
            if (targetStadt == null || targetStadt.Besitzer == Besitzer)
                return false;
            if (BerechneDistanz(targetStadt.startFeld) > Reichweite)
                return false;
            targetStadt.NehmeSchaden(Schaden);
            return true;
        }
        /// <summary>
        /// Überladene Methode zum Angreifen Squad
        /// </summary>
        /// <param name="targetSquad"></param>
        /// <returns></returns>
        public override bool Angreifen(Squad targetSquad)
        {
            if (targetSquad == null || targetSquad.Besitzer == Besitzer)
                return false;
            if (BerechneDistanz(targetSquad.AktuellesFeld) > Reichweite)
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
