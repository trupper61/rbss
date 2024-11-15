using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
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
        public int spielerMax = 2;

        public static List<Spieler> spieler = new List<Spieler>
        {
            new Spieler(null, 0, 1),
            new Spieler(null, 0, 2)
        };

        public static int aktuellerSpielerIndex = 0;

        Spieler aktuellerSpieler = spieler[aktuellerSpielerIndex];

        Random random = new Random();
        Random rescourcen = new Random();
        Random rescourcenMenge = new Random();
        Random spielAnfaenger = new Random();
        public Form1()
        {
            InitializeComponent();
            Feldgenerierung();
            MessageBox.Show($"Aktueller Spieler: {aktuellerSpieler.spielernummer}");
        }
        public void Feldgenerierung() 
        {
            bool flag = false;
            int felderxMax = 10;
            int felderyMax = 10;
            felder = new Feld[felderxMax, felderyMax];
            
            int wasserMax = (felderxMax * felderyMax) / 2;
            int feldgroesse = 50;
            int durchlauefe = 0;

            for (int i = 0; i < felderxMax; i++)
            {
                for (int j = 0; j < felderyMax; j++)
                {
                    Feld feld = new Feld();

                    int rescourcenEinteilung = rescourcen.Next(0, 2);
                    int rescourcenAnzahl = rescourcenMenge.Next(1, 25);

                    if (wasserMax > 0 && random.Next(1, 100) < 10 || durchlauefe > 0)
                    {
                        if (j != felderyMax && i != felderxMax && j != 0 && i != 0) 
                        {
                            feld.feldart = "Water";
                            feld.textur.Image = Properties.Resources.water;
                            wasserMax--;
                            durchlauefe--;
                        }
                        else
                        {
                            feld.feldart = "Grass";
                            feld.textur.Image = Properties.Resources.grass;
                        }
                    }
                    else if (durchlauefe <= 0)
                    {
                        feld.feldart = "Grass";
                        feld.textur.Image = Properties.Resources.grass;
                    }
                    if (feld.feldart == "Water" && durchlauefe <= 0)
                    {
                        if (flag != true)
                        {
                            durchlauefe = 1;
                        }
                        flag = true;
                        if (durchlauefe == 0)
                        {
                            flag = false;
                        }
                    }

                    if (rescourcenEinteilung == 1 && feld.feldart == "Grass")
                    {
                        feld.rescourcen = new Eisen(10, rescourcenAnzahl);
                    }

                    feld.textur.Size = new Size(feldgroesse, feldgroesse);
                    feld.textur.Location = new Point(j * feldgroesse, i * feldgroesse);
                    feld.textur.BackColor = Color.White;
                    feld.textur.Tag = feld;
                    feld.textur.Click += new EventHandler(feld_Click);

                    felder[i, j] = feld;

                    //Beispiel Truppenerstellung: Gehört Spieler 2
                    if ((i == 1 && j == 1) || (i == 4 && j == 4))
                    {
                        Truppe truppe = new Truppe();
                        feld.SetzeTruppe(truppe, spieler[1]);
                        truppe.Darstellung.Tag = truppe;
                        truppe.Darstellung.Click += new EventHandler(feld_Click);
                        this.Controls.Add(truppe.Darstellung);
                    }
                    //Beispiel Truppenerstellung: Gehört Spieler 1
                    if (i == 3 && j == 3)
                    {
                        Truppe truppe = new Truppe();
                        feld.SetzeTruppe(truppe, spieler[0]);
                        truppe.Darstellung.Tag = truppe;
                        truppe.Darstellung.Click += new EventHandler(feld_Click);
                        this.Controls.Add(truppe.Darstellung);
                    }
                    if (i == 5 && j == 5)
                    {
                        Stadt stadt = new Stadt(felder[i,j], felder);
                        this.Controls.Add(stadt.textur);
                    }
                    feld.position = new Point(i, j);

                    this.Controls.Add(feld.textur);
                }
            }
            for (int i = 1; i < felderxMax - 1; i++)
            {
                for (int j = 1; j < felderyMax - 1; j++)
                {
                    if (felder[i, j].feldart == "Water")
                    {
                        felder[i - 1, j].feldart = "Water";
                        felder[i - 1, j].textur.Image = Properties.Resources.water;
                        felder[i, j - 1].feldart = "Water";
                        felder[i, j - 1].textur.Image = Properties.Resources.water;
                    }
                }
            }
        }
        public void feld_Click(object sender, EventArgs e)
        {
            var clickedObject = (sender as PictureBox).Tag;

            UIInfo.Show();
            UIInfo.Image = Properties.Resources.UI2;


            if (clickedObject is Truppe clickedTruppe)
            {
                if (selectedTruppe != null && selectedTruppe != clickedTruppe)
                {
                    EntferneBewegungsbereich();
                    selectedTruppe.Angreifen(clickedTruppe);
                    selectedTruppe = null;
                    UpdateTruppenLabels(null);
                    return;
                }
                if (clickedTruppe != null && clickedTruppe.Besitzer != aktuellerSpieler)
                {
                    MessageBox.Show("Das ist nicht deine Truppe!");
                    return;
                }
                if (selectedTruppe == null)
                {
                    selectedTruppe = clickedTruppe;
                    clickedTruppe.Darstellung.BackColor = Color.LightBlue;
                    MakiereBewegungsreichweite(selectedTruppe);

                    if (lastClickedFeld != null)
                    {
                        lastClickedFeld.textur.BackColor = Color.White;
                        lastClickedFeld.textur.Image = Properties.Resources.grass;
                    }
                    UpdateTruppenLabels(clickedTruppe);
                }
                else if (selectedTruppe != null)
                {
                    EntferneBewegungsbereich();
                    selectedTruppe = null;
                    clickedTruppe.Darstellung.BackColor = Color.Blue;

                    truppenLebenLB.Visible = false;
                    truppenSchadenLB.Visible = false;
                }
            }
            else if (clickedObject is Feld clickedFeld)
            {
                UpdateTruppenLabels(null);
                EntferneBewegungsbereich();
                if (clickedFeld.rescourcen != null && clickedFeld.feldart != "Water")
                {
                    UIInfo.Image = Properties.Resources.UI2eisen;
                    anzahlRes.Show();
                    anzahlRes.Text = clickedFeld.rescourcen.ToString();
                    anzahlRes.BringToFront();
                }
                else
                {
                    anzahlRes.Hide();
                }
                if (clickedFeld.feldart == "Water")
                {
                    UIInfo.Hide();
                    anzahlRes.Hide();
                    return;
                }
                if (clickedFeld.textur.BackColor == Color.White)
                {
                    clickedFeld.textur.BackColor = Color.Gray;
                    clickedFeld.textur.Image = Properties.Resources.grasstransparent;

                }
                else if (clickedFeld.textur.BackColor == Color.Gray)
                {
                    truppenLebenLB.Visible = false;
                    truppenSchadenLB.Visible = false;
                    clickedFeld.textur.BackColor = Color.White;
                    clickedFeld.textur.Image = Properties.Resources.grass;

                    UIInfo.Hide();
                    anzahlRes.Hide();

                    if (selectedTruppe != null)
                    {
                        selectedTruppe.Darstellung.BackColor = Color.Blue;
                        selectedTruppe = null;
                    }
                }
                if (lastClickedFeld != null && lastClickedFeld != clickedFeld)
                {
                    lastClickedFeld.textur.BackColor = Color.White;
                    lastClickedFeld.textur.Image = Properties.Resources.grass;
                }

                lastClickedFeld = clickedFeld;

                if (selectedTruppe != null && clickedFeld.feldart == "Grass")
                {
                    int startx = selectedTruppe.AktuellesFeld.textur.Location.X / 50;
                    int starty = selectedTruppe.AktuellesFeld.textur.Location.Y / 50;
                    int zielx = clickedFeld.textur.Location.X / 50;
                    int ziely = clickedFeld.textur.Location.Y / 50;

                    int distanz = Math.Abs(startx - zielx) + Math.Abs(starty - ziely);

                    if (distanz > selectedTruppe.Bewegungsreichweite)
                    {
                        selectedTruppe.Darstellung.BackColor = Color.Blue;
                        selectedTruppe = null;
                        lastClickedFeld.textur.BackColor = Color.White;
                        lastClickedFeld.textur.Image = Properties.Resources.grass;
                        clickedFeld.textur.BackColor = Color.Gray;
                        clickedFeld.textur.Image = Properties.Resources.grasstransparent;
                        return;
                    }
                    selectedTruppe.AktuellesFeld.EntferneTruppe();
                    clickedFeld.SetzeTruppe(selectedTruppe, aktuellerSpieler);

                    selectedTruppe.Darstellung.BackColor = Color.Blue;
                    selectedTruppe = null;
                }

            }

        }
        public void UpdateTruppenLabels (Truppe truppe)
        {
            if (truppe != null)
            {
                truppenLebenLB.Text = $"Leben: {truppe.Leben}";
                truppenSchadenLB.Text = $"Schaden: {truppe.Schaden}";
                truppenLebenLB.Visible = true;
                truppenSchadenLB.Visible = true;
            }
            else
            {
                truppenLebenLB.Visible = false;
                truppenSchadenLB.Visible= false;
            }
        }

        public void MakiereBewegungsreichweite(Truppe truppe) 
        {
            int startX = truppe.AktuellesFeld.position.X;
            int startY = truppe.AktuellesFeld.position.Y;

            for (int i= 0; i < 10;  i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    int distanz = Math.Abs(startX - i) + Math.Abs(startY - j);
                    if (distanz <= truppe.Bewegungsreichweite)
                    {
                        if (felder[i, j].feldart == "Grass")
                        {
                            felder[i, j].textur.Image = Properties.Resources.grasstransparent;
                            felder[i, j].textur.BackColor = Color.LightGreen;
                        }
                    }
                }
            }
        }
        public void EntferneBewegungsbereich()
        {
            foreach (var feld in felder)
            {
                if (feld.feldart == "Grass")
                {
                    feld.textur.BackColor = Color.White;
                    feld.textur.Image = Properties.Resources.grass;
                }
            }
        }

        public void Spielerwechsel()
        {
            aktuellerSpielerIndex++;

            if (aktuellerSpielerIndex >= spieler.Count)
            {
                aktuellerSpielerIndex = 0;
                MessageBox.Show("Neue Runde beginnt");
            }

            aktuellerSpieler = spieler[aktuellerSpielerIndex];
            MessageBox.Show($"Spieler {aktuellerSpieler.spielernummer} ist dran");
        }
    }
}

