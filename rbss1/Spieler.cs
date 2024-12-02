using System;
using System.Collections.Generic;
using System.Drawing;
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
        public List<Truppe> truppen { get; set; }

        public Color SpielerFarbe { get; private set; }

        public Spieler(List<Rescourcen> rescourcenBesitz, int geld, int spielernummer, Color SpielerFarbe) 
        {
            this.rescourcenBesitz = rescourcenBesitz;
            this.geld = geld;
            this.spielernummer = spielernummer;
            this.SpielerFarbe = SpielerFarbe;
            zugbeendet = false;
            truppen = new List<Truppe>();
        }

        
    }
}
