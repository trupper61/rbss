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
        private Stadt selectedStadt = null;
        private Feld[,] felder;
        public int spielerMax = 2;
        public int truppenMax = 4;

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
        Random randomPlatzierung = new Random();
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

                    //TODO : Implementierung der Spielergenerierung nach der Feldgenerierung
                    if(i != 0 & j!= 0) 
                    {
                        if (truppenMax > 0 && felder[i, j].feldart != "Water" && felder[i - 1, j].feldart != "Water" && felder[i, j - 1].feldart != "Water")
                        {
                            if (random.Next(1, 100) < 10)
                            {
                                TruppenPlatzierung(i, j);
                            }
                        }
                    }
                    
                    //Beispiel Truppenerstellung: Gehört Spieler 1

                    /*if (i == ranomPlatzierung.Next(1, 11) && j == ranomPlatzierung.Next(1, 11))
                    {
                        if (felder[i, j].TruppeAufFeld == null) 
                        {
                            if (felder[i, j].feldart != "Water")
                            {
                                Truppe truppe = new Truppe();
                                feld.SetzeTruppe(truppe, spieler[0]);
                                truppe.Darstellung.Tag = truppe;
                                truppe.Darstellung.Click += new EventHandler(feld_Click);
                                this.Controls.Add(truppe.Darstellung);
                            }
                        } 
                    }
                    */
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
            Stadt stadt = new Stadt(felder[5, 5], felder);
            stadt.textur.Location = new Point(felder[5, 5].textur.Location.X + 5, felder[5, 5].textur.Location.Y + 5);

            stadt.textur.Tag = stadt;
            stadt.SetzeEinflussRadius();
            felder[5, 5].StadtAufFeld = stadt;
            stadt.textur.Click += new EventHandler(feld_Click);
            this.Controls.Add(stadt.textur);
            stadt.textur.BringToFront();
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
                    EntferneBewegungsbereich(null);
                    selectedTruppe.Angreifen(clickedTruppe);
                    selectedTruppe = null;
                    HideUIInfo();
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
                    UpdateUIInfo(clickedTruppe);
                }
                else if (selectedTruppe != null)
                {
                    EntferneBewegungsbereich(null);
                    selectedTruppe = null;
                    clickedTruppe.Darstellung.BackColor = Color.Blue;

                    truppenLebenLB.Visible = false;
                    truppenSchadenLB.Visible = false;
                }
                UpdateGame(selectedTruppe);
            }
            else if (clickedObject is Stadt clickedStadt)
            {
                selectedStadt = clickedStadt;
                UpdateUIInfo(clickedStadt);
                
                clickedStadt.SetzeEinflussRadius();

                selectedTruppe = null;
                
                EntferneBewegungsbereich(null);
                return;
            }
            else if (clickedObject is Feld clickedFeld)
            {
                HideUIInfo();
                EntferneBewegungsbereich(clickedFeld);
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
                }
                if (lastClickedFeld != null && lastClickedFeld != clickedFeld)
                {
                    clickedFeld.textur.BackColor = Color.Gray;
                    clickedFeld.textur.Image = Properties.Resources.grasstransparent;
                    
                    lastClickedFeld.textur.BackColor = Color.White;
                    lastClickedFeld.textur.Image = Properties.Resources.grass;
                    if (clickedFeld.GehoertZuStadt)
                    {
                        EntferneBewegungsbereich(clickedFeld);

                    }
                    else
                    {
                        lastClickedFeld.textur.BackColor = Color.DarkGreen;
                        lastClickedFeld.textur.Image = Properties.Resources.grasstransparent;
                    }
                }
                else if (lastClickedFeld != null && lastClickedFeld == clickedFeld)
                {

                    clickedFeld.textur.BackColor = Color.White;
                    clickedFeld.textur.Image = Properties.Resources.grass;
                    if (clickedFeld.GehoertZuStadt)
                    {
                        clickedFeld.textur.BackColor = Color.DarkGreen;
                        clickedFeld.textur.Image = Properties.Resources.grasstransparent;
                    }
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
                    EntferneBewegungsbereich(null);
                    selectedTruppe = null;
                }
                UpdateGame(selectedTruppe);
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
        public void EntferneBewegungsbereich(object ob)
        {
            Feld stadtFeld = null;
            foreach (var feld in felder)
            {
                if (feld == null)
                    return;
                if (feld.feldart == "Grass")
                {
                    feld.textur.BackColor = Color.White;
                    feld.textur.Image = Properties.Resources.grass;
                }
                if (feld.StadtAufFeld != null)
                    stadtFeld = feld;
            }
            stadtFeld.StadtAufFeld.SetzeEinflussRadius();
            Feld t = ob as Feld;
            if (t != null && t.feldart != "Water" && t.GehoertZuStadt)
            {
                if (t == lastClickedFeld)
                    stadtFeld.StadtAufFeld.SetzeEinflussRadius();
                t.textur.BackColor = Color.Gray;
                t.textur.Image = Properties.Resources.grasstransparent;

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

        private void weiter_Click(object sender, EventArgs e)
        {
            Spielerwechsel();
        }

        //Update, damit Aktionen inmitten der Runde registriert, und darauf reagiert werden kann.
        public void UpdateGame(Truppe selectedTruppe) 
        {
            if(selectedTruppe == null) 
            {
                weiter.Show();
            }
            else 
            {
                weiter.Hide();
            }
        }
        
        public void TruppenPlatzierung(int i, int j) 
        {
            Truppe truppe = new Truppe();
            felder[i, j].SetzeTruppe(truppe, spieler[random.Next(0, 2)]);
            truppe.Darstellung.Tag = truppe;
            truppe.Darstellung.Click += new EventHandler(feld_Click);
            this.Controls.Add(truppe.Darstellung);
            spielerMax--;
        public void UpdateUIInfo(Object o)
        {
            if (o == null)
                return;
            if (o is Truppe truppe)
            {
                ItemPB.Image = Properties.Resources.melee;
                truppenLebenLB.Text = $"Lebel: {truppe.Leben}";
                truppenSchadenLB.Text = $"Schaden: {truppe.Schaden}";
                titelLabel.Text = truppe.ToString();
                truppenSchadenLB.Visible = true;
            }
            else if (o is Stadt stadt)
            {
                ItemPB.Image = Properties.Resources.stadt;
                truppenLebenLB.Text = $"Siedler: {stadt.Einwohner}";
                titelLabel.Text = stadt.Name;
            }
            ItemPB.Visible = true;
            truppenLebenLB.Visible = true;     
            titelLabel.Visible = true;

        }
        public void HideUIInfo()
        {
            ItemPB.Visible = false;
            truppenSchadenLB.Visible = false;
            truppenLebenLB.Visible = false;
            titelLabel.Visible = false;
        }
    }
}

