using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rbss1
{
    internal class Spieler
    {
        public List<Rescourcen> rescourcenBesitz;
        public int geld;

        public Spieler(List<Rescourcen> rescourcenBesitz, int geld) 
        {
            this.rescourcenBesitz = rescourcenBesitz;
            this.geld = geld;
        }
    }
}
