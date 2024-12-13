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

        public int einkommen { get; set; } = 75;
        public int Leben { get; set; }
        
        public Stadt(Feld startFeld, Feld[,] felder)
        {
            Name = "Bremen";
            Einwohner = 100;
            this.felder = felder;
            this.startFeld = startFeld;

            Leben = 200;

            stadtFlaeche = new List<Feld>();
            textur = new PictureBox
            {
                Size = new Size(40, 40),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Properties.Resources.rathaus,
                BackgroundImage = Properties.Resources.grasstransparent

            };
        }

        public void SetzeEinflussRadius(List<Spieler> spieler, int spielerIndex)
        {
            
            if (this.textur == null || felder == null)
            {
                return; 
            }

            int startX = this.startFeld.position.X; 
            int startY = this.startFeld.position.Y; 
            int radius = 2;

            stadtFlaeche.Clear();
            Console.WriteLine($"Startfeld: {startFeld.position.X}, {startFeld.position.Y}");
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
                                Console.WriteLine($"Feld hinzugefügt: {i}, {j}");
                                stadtFlaeche.Add(felder[i, j]);
                            }
                            if (Besitzer == null)
                                return;
                            felder[i, j].textur.BackColor = Besitzer.SpielerFarbe;
                            felder[i, j].besitzer = Besitzer;
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
            if (stadt != null)
            {
                startFeld.EntferneStadt();
            }
            Besitzer = null;
            this.textur.Hide();
            this.stadt = null;
        }
        public void NehmeSchaden(int schaden)
        {
            Leben -= schaden;
            if (Leben <= 0)
            {
                EntferneStadt();
            }
        }
    }
}
