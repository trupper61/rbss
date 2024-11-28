using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rbss1
{
    internal class Weizen : Rescourcen
    {
        public Weizen(int wert, int anzahl)
            : base(wert, anzahl)
        {

        }

        public override string ToString()
        {
            return $"Weizen: {base.ToString()}";
        }
    }
}
