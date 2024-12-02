using rbss1.Properties;
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
        public Rescourcen rescourcenBesitz {  get; set; }
        public int geld { get; set; } 
        public int bewegungspunkte { get; set; }
        public int spielernummer {  get; set; }
        public bool zugbeendet { get; set; }
        public List<Stadt> staedteBesitz { get; set; }
        public List<Farm> farmBesitz { get; set; }
        public Color SpielerFarbe { get; private set; }

        public Spieler(Rescourcen rescourcenBesitz, int geld, int bewegungspunkte,int spielernummer, Color SpielerFarbe, List<Stadt> staedteBesitz, List<Farm> farmBesitz) 
        {
            this.rescourcenBesitz = new Rescourcen();
            this.geld = 10000000;
            this.bewegungspunkte = bewegungspunkte;
            this.spielernummer = spielernummer;
            this.SpielerFarbe = SpielerFarbe;
            this.staedteBesitz = new List<Stadt>();
            this.farmBesitz = new List<Farm>();
            zugbeendet = false;
        }

        public void UpdateRessourcen(List<Feld> alleFelder)
        {
            rescourcenBesitz = new Rescourcen();

            foreach (var feld in alleFelder)
            {
                if (feld.besitzer == this && feld.rescourcen != null)
                {
                    rescourcenBesitz.Add(feld.rescourcen);
                }
            }
        }
    }
}
