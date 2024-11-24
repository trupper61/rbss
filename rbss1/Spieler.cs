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

        public Color EinflussFarbe { get; private set; }

        public Spieler(List<Rescourcen> rescourcenBesitz, int geld, int spielernummer) 
        {
            this.rescourcenBesitz = rescourcenBesitz;
            this.geld = geld;
            this.spielernummer = spielernummer;

            Random random = new Random();
            this.EinflussFarbe = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));

            zugbeendet = false;
        }
    }
}
