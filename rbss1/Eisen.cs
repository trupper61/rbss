using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rbss1
{
    internal class Eisen : Rescourcen
    {
        public Eisen(int wert, int anzahl) 
            : base(wert, anzahl, "Eisen")
        {

        }

        public override string ToString()
        {
            return $"Eisen: {base.ToString()}";
        }
    }
}
