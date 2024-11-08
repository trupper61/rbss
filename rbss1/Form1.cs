using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rbss1
{
    public partial class Form1 : Form
    {
        private PictureBox lastClicked = null;
        private Feld[,] felder;
        Random random = new Random();
        public Form1()
        {
            InitializeComponent();
            felder = new Feld[10, 10];
            Feldgenerierung();
    
        }
        public void Feldgenerierung() 
        {
            int x = 0;
            int y = 0;
            int feldgroesse = 50;
            
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    int feldtyp = random.Next(1, 4);
                    Feld feld = new Feld();

                    if(feldtyp > 0 && feldtyp < 3) 
                    {
                        feld.feldart = "Grass";
                    }
                    else if(feldtyp == 3)
                    {
                        feld.feldart = "Water";
                    }
                    feld.textur.Size = new Size(feldgroesse, feldgroesse);
                    feld.textur.Location = new Point(j * feldgroesse, i * feldgroesse);
                    if(feld.feldart == "Grass") 
                    {
                        feld.textur.Image = Properties.Resources.grass;
                    }
                    else if(feld.feldart == "Water")
                    {
                        feld.textur.Image = Properties.Resources.water;
                    }
                    feld.textur.BackColor = Color.White;
                    feld.textur.Tag = feld;
                    feld.textur.Click += new EventHandler(feld_Click);

                    felder[i, j] = feld;
                    if (i == 1 && j == 1)
                    {
                        Truppe truppe = new Truppe();
                        feld.SetzeTruppe(truppe);
                        this.Controls.Add(truppe.Darstellung);

                    }

                    this.Controls.Add(feld.textur);
                }
            }
        }
        public void feld_Click(object sender, EventArgs e) 
        {
            PictureBox feld = sender as PictureBox;
            Feld felder = feld.Tag as Feld;

            if (felder != null && felder.feldart == "Water")
            {
                return;
            }

            if(felder != null && felder.feldart == "Grass") 
            {
                if (feld.BackColor == Color.White)
                {
                    feld.BackColor = Color.Gray;
                    feld.Image = Properties.Resources.grasstransparent;
                }
                else if (feld.BackColor == Color.Gray)
                {
                    feld.BackColor = Color.White;
                    feld.Image = Properties.Resources.grass;
                }

                if (lastClicked != null && lastClicked != feld)
                {
                    lastClicked.BackColor = Color.White;
                    lastClicked.Image = Properties.Resources.grass;
                }

                lastClicked = feld;
            }
            
        }
    }
}
