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
        public Rescourcen(int wert, int anzahl, string name) 
        {
            this.wert = wert;
            this.anzahl = anzahl;
            this.name = name;
        }

        public override string ToString()
        {
            return $"Wert: {wert}, Anzahl: {anzahl}";
        }
    }
}
