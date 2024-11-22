using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rbss1
{
    public class Spieler
    {
        public List<Rescourcen> rescourcenBesitz {  get; set; }
        public int geld {  get; set; }
        public int spielernummer {  get; set; }
        public bool zugbeendet { get; set; }

        public string farbe {  get; set; }

        public Spieler(List<Rescourcen> rescourcenBesitz, int geld, int spielernummer) 
        {
            this.rescourcenBesitz = rescourcenBesitz;
            this.geld = geld;
            this.spielernummer = spielernummer;
            zugbeendet = false;
        }
    }
}
