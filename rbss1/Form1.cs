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
        private Feld lastClickedFeld = null;
        private Truppe selectedTruppe = null;
        private Feld[,] felder;
        Random random = new Random();
        Random rescourcen = new Random();
        Random rescourcenMenge = new Random();
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
                    int rescourcenEinteilung = rescourcen.Next(0, 2);
                    int rescourcenAnzahl = rescourcenMenge.Next(1, 25);

                    Feld feld = new Feld();

                    if(rescourcenEinteilung == 1) 
                    {
                        feld.rescourcen = new Eisen(10, rescourcenAnzahl);
                    }

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
                        truppe.Darstellung.Tag = truppe;
                        truppe.Darstellung.Click += new EventHandler(feld_Click);
                        this.Controls.Add(truppe.Darstellung);
                    }
                    this.Controls.Add(feld.textur);
                }
            }
        }
        public void feld_Click(object sender, EventArgs e) 
        {
            var clickedObject = (sender as PictureBox).Tag;


            UIInfo.Show();
            UIInfo.Image = Properties.Resources.UI2;
            if (lastClickedFeld != null)
            {
                lastClickedFeld.textur.BackColor = Color.White;
                lastClickedFeld.textur.Image = Properties.Resources.grass;
            }
            if (clickedObject is Truppe clickedTruppe)
            {
                selectedTruppe = clickedTruppe;
                clickedTruppe.Darstellung.BackColor = Color.LightBlue;
            }
            else if (clickedObject is Feld clickedFeld)
            {
              if (clickedFeld.rescourcen != null && clickedFeld.feldart != "Water") 
              {
                MessageBox.Show($"{felder.rescourcen.ToString()}");
                UIInfo.Image = Properties.Resources.UI2eisen;
                anzahlRes.Show();

                anzahlRes.Text = felder.rescourcen.ToString();

                anzahlRes.BringToFront();
              }
              else 
              {
                anzahlRes.Hide();
              }
                if (clickedFeld.feldart == "Water")
                {
                    feld.BackColor = Color.White;
                    feld.Image = Properties.Resources.grass;
                    UIInfo.Hide();
                    anzahlRes.Hide();
                    return;
                }
                clickedFeld.textur.BackColor = Color.Gray;
                clickedFeld.textur.Image = Properties.Resources.grasstransparent;
                lastClickedFeld = clickedFeld;

                if (selectedTruppe != null && clickedFeld.feldart == "Grass")
                {
                    lastClicked.BackColor = Color.White;
                    lastClicked.Image = Properties.Resources.grass;
                    
                }

                    int startx = selectedTruppe.AktuellesFeld.textur.Location.X / 50;
                    int starty = selectedTruppe.AktuellesFeld.textur.Location.Y / 50;
                    int zielx = clickedFeld.textur.Location.X / 50;
                    int ziely = clickedFeld.textur.Location.Y / 50;
                    
                    int distanz = Math.Abs(startx - zielx) + Math.Abs(starty - ziely);

                    if (distanz > selectedTruppe.Bewegungsreichweite)
                    {
                        MessageBox.Show("Bewegungslimit überschritten");
                        selectedTruppe.Darstellung.BackColor = Color.Blue;
                        selectedTruppe = null;
                        lastClickedFeld.textur.BackColor = Color.White;
                        lastClickedFeld.textur.Image = Properties.Resources.grass;
                        return;
                    }

                    selectedTruppe.AktuellesFeld.EntferneTruppe();
                    clickedFeld.SetzeTruppe(selectedTruppe);

                    selectedTruppe.Darstellung.BackColor = Color.Blue;
                    selectedTruppe = null;
                }
            }
        }
    }
}
