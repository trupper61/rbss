using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rbss1
{
    public class Stadt
    {
        public Spieler Besitzer {  get; set; }
        public List<Feld> stadtFlaeche { get; set; }
        public Feld[,] worldMap { get; set; }
        public PictureBox textur { get; private set; }

        public Stadt(Feld startFeld, Feld[,] worldMap)
        {
            stadtFlaeche = new List<Feld>();
            stadtFlaeche.Add(startFeld);
            this.worldMap = worldMap;
            textur = new PictureBox
            {
                Size = new Size(40, 40),
                BackColor = Color.Green
            };
            InitializeStadt();
        }
        public void InitializeStadt()
        {
            Feld startFeld = stadtFlaeche[0];
            int x = (startFeld.textur.Height - textur.Height) / 2;
            int y = (startFeld.textur.Width - textur.Width) / 2;
            textur.Location = new Point(startFeld.textur.Location.X + x, startFeld.textur.Location.Y + y);
            textur.Image = Properties.Resources.rathaus;
            textur.BringToFront();
        }
    }
}
