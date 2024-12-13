using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rbss1
{
    public class Rescourcen
    {
        public int wert {  get; set; }
        public int anzahl { get; set; }
        public string name { get; set; }

        public int Eisen { get; set; }
        public int Kohle { get; set; }
        public int Weizen { get; set; }
        public int Stahl { get; set; }
        public Rescourcen(int wert = 0, int anzahl = 0, string name = null) 
        {
            this.wert = wert;
            this.anzahl = anzahl;
            this.name = name;
        }

        public override string ToString()
        {
            return $"Wert: {wert}, Anzahl: {anzahl}";
        }

        public void Add(Rescourcen neueRessourcen)
        {
            if (neueRessourcen != null)
            {
                Eisen += neueRessourcen.Eisen;
                Kohle += neueRessourcen.Kohle;
                Weizen += neueRessourcen.Weizen;
                Stahl += neueRessourcen.Stahl;
            }
        }
    }
}
