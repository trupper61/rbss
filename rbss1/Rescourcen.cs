using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rbss1
{
    public class Rescourcen
    {
        private int wert {  get; set; }
        private int anzahl { get; set; }
        public Rescourcen(int wert, int anzahl) 
        {
            this.wert = wert;
            this.anzahl = anzahl;
        }

        public override string ToString()
        {
            return $"Wert: {wert}, Anzahl: {anzahl}";
        }
    }
}
