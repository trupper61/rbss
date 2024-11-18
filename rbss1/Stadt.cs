using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace rbss1
{
    public class Stadt
    {
        public Spieler Besitzer { get; set; }
        public List<Feld> stadtFlaeche { get; set; }
        public PictureBox textur { get; private set; }
        public int Einwohner { get; set; }
        public string Name { get; set; }
        public Stadt stadt { get; private set; }
        public Feld[,] felder { get; set; }
        public Feld startFeld { get; set; }
        public Stadt(Feld startFeld, Feld[,] felder)
        {
            Name = "Bremen";
            Einwohner = 100;
            this.felder = felder;
            this.startFeld = startFeld;
            stadtFlaeche = new List<Feld>();
            textur = new PictureBox
            {
                Size = new Size(40, 40),
                BackColor = Color.Green,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Properties.Resources.rathaus

            };
        }

        public void SetzeEinflussRadius()
        {
            if (this.textur == null || felder == null)
            {
                return; 
            }

            int startX = this.startFeld.position.X; 
            int startY = this.startFeld.position.Y; 
            int radius = 2; 

            stadtFlaeche.Clear();

            for (int i = 0; i < felder.GetLength(0); i++)
            {
                for (int j = 0; j < felder.GetLength(1); j++)
                {
                    int distanz = Math.Abs(startX - i) + Math.Abs(startY - j);

                    if (distanz <= radius)
                    {
                        if (felder[i, j] != null && felder[i, j].textur != null && felder[i, j].feldart != "Water")
                        {
                            if (!stadtFlaeche.Contains(felder[i, j]))
                            {
                                stadtFlaeche.Add(felder[i, j]);
                            }

                            felder[i, j].textur.BackColor = Color.DarkGreen; 
                            felder[i, j].textur.Image = Properties.Resources.grasstransparent;
                            felder[i, j].GehoertZuStadt = true;
                        }
                    }
                }
            }
        }
        public void SetzeStadt(Stadt stadt)
        {
            this.stadt = stadt;
        }
        public void EntferneStadt()
        {
            this.stadt = null;
        }
    }
}
