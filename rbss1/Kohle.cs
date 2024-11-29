using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rbss1
{
    internal class Kohle : Rescourcen
    {
        public Kohle(int wert, int anzahl)
            : base(wert, anzahl, "Kohle")
        {

        }

        public override string ToString()
        {
            return $"Kohle: {base.ToString()}";
        }
    }
}
