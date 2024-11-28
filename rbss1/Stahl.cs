using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rbss1
{
    internal class Stahl : Rescourcen
    {
        public Stahl(int wert, int anzahl)
            : base(wert, anzahl)
        {

        }

        public override string ToString()
        {
            return $"Stahl: {base.ToString()}";
        }
    }
}
