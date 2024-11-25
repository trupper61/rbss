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
        public int truppenMax = 4;
        public int spielerMax = 4;

        public static List<Spieler> spieler = new List<Spieler>
        {
        };

        public static int aktuellerSpielerIndex = 0;

        public Spieler aktuellerSpieler;

        Random random = new Random();
        Random rescourcen = new Random();
        Random rescourcenMenge = new Random();
        Random spielAnfaenger = new Random();
        Random randomPlatzierung = new Random();
        public Form1()
        {
            InitializeComponent();

            for (int i = 0; i < spielerMax; i++)
            {
                spieler.Add(new Spieler(null, 0, i + 1, Color.FromArgb(random.Next(256), random.Next(256), random.Next(256))));
            }

            if (spieler.Count > 0)
            {
                aktuellerSpieler = spieler[aktuellerSpielerIndex];
            }

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

                    
                    feld.position = new Point(i, j);

                    this.Controls.Add(feld.textur);
                    
                }
            }

            //Platzierung von Wasser links und über einem Wasserfeld
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

            //Truppenplatzierung
            for(int i = 0; i < felderxMax; i++) 
            {
                for(int j = 0; j < felderyMax; j++) 
                {
                    if(random.Next(0, 100) < 15) 
                    {
                        if (felder[i, j].feldart != "Water")
                        {
                            Truppe truppe = new Truppe();
                            felder[i, j].SetzeTruppe(truppe, spieler[random.Next(0, spielerMax)]);
                            truppe.Darstellung.Tag = truppe;
                            truppe.Darstellung.Click += new EventHandler(feld_Click);
                        }
                    }
                    
                }
            }

            GeneriereStaedte(felderxMax, felderyMax);

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
                    einnehmen.Show();
                }
                else if (selectedTruppe != null)
                {
                    EntferneBewegungsbereich(null);
                    selectedTruppe = null;
                    clickedTruppe.Darstellung.BackColor = Color.Blue;

                    UpdateUIInfo(clickedTruppe);
                    truppenLebenLB.Hide();
                    truppenSchadenLB.Hide();
                    einnehmen.Hide();
                    ItemPB.Hide();
                    titelLabel.Hide();
                }
                UpdateGame(selectedTruppe);
                lastClickedFeld = clickedTruppe.AktuellesFeld;
            }
            else if (clickedObject is Stadt clickedStadt)
            {
                selectedStadt = clickedStadt;
                if(clickedStadt != null && clickedStadt.Besitzer == aktuellerSpieler) 
                {
                    UpdateUIInfo(clickedStadt);

                    //clickedStadt.SetzeEinflussRadius(spieler, aktuellerSpielerIndex);

                    selectedTruppe = null;
                    EntferneBewegungsbereich(null);
                    return;
                }
                MessageBox.Show("Das ist nicht deine Stadt!");
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
                    truppenLebenLB.Hide();
                    truppenSchadenLB.Hide();
                    clickedFeld.textur.BackColor = Color.White;
                    clickedFeld.textur.Image = Properties.Resources.grass;
                    UIInfo.Hide();
                    anzahlRes.Hide();
                    HideUIInfo();
                }
                if (lastClickedFeld != null && lastClickedFeld != clickedFeld)
                {
                    clickedFeld.textur.BackColor = Color.Gray;
                    clickedFeld.textur.Image = Properties.Resources.grasstransparent;

                    if(!lastClickedFeld.GehoertZuStadt) 
                    {
                        lastClickedFeld.textur.BackColor = Color.White;
                        lastClickedFeld.textur.Image = Properties.Resources.grass;
                    }
                    if (clickedFeld.GehoertZuStadt)
                    {
                        EntferneBewegungsbereich(clickedFeld);
                    }
                }
                else if (lastClickedFeld != null && lastClickedFeld == clickedFeld)
                {
                    
                    if (!clickedFeld.GehoertZuStadt) 
                    {
                        if (clickedFeld.textur.BackColor == Color.White)
                        {
                            clickedFeld.textur.BackColor = Color.Gray;
                            clickedFeld.textur.Image = Properties.Resources.grasstransparent;
                            UIInfo.Show();
                        }
                        else if (clickedFeld.textur.BackColor == Color.Gray)
                        {
                            clickedFeld.textur.BackColor = Color.White;
                            clickedFeld.textur.Image = Properties.Resources.grass;
                            UIInfo.Hide();
                            anzahlRes.Hide();
                        }
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
                    einnehmen.Hide();
                }
                UpdateGame(selectedTruppe);
            }


        }
        public void MakiereBewegungsreichweite(Truppe truppe)
        {
            int startX = truppe.AktuellesFeld.position.X;
            int startY = truppe.AktuellesFeld.position.Y;
            lastClickedFeld = null;
            for (int i = 0; i < 10; i++)
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
            foreach (var feld in felder)
            {
                if (feld == null)
                    return;
                if (feld.feldart == "Grass")
                {
                    feld.textur.BackColor = Color.White;
                    feld.textur.Image = Properties.Resources.grass;
                    
                }
            }
            foreach (var feld in felder) 
            {
                if (feld.StadtAufFeld != null)
                {
                    feld.StadtAufFeld.SetzeEinflussRadius(spieler, aktuellerSpielerIndex);
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

        private void weiter_Click(object sender, EventArgs e)
        {
            Spielerwechsel();
        }

        //Update, damit Aktionen inmitten der Runde registriert, und darauf reagiert werden kann.
        public void UpdateGame(Truppe selectedTruppe)
        {
            if (selectedTruppe == null)
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
        }
        public void UpdateUIInfo(Object o)
        {
            if (o == null) 
            {
                
                return;
            }
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
            ItemPB.Show();
            truppenLebenLB.Show();
            titelLabel.Show();
        }
        public void HideUIInfo()
        {
            ItemPB.Hide();
            truppenSchadenLB.Hide();
            truppenLebenLB.Hide();
            titelLabel.Hide();
        }

        public void GeneriereStaedte(int felderxMax, int felderyMax)
        {
            int stadtabstand = 5;
            List<Point> platzierteStadtPositionen = new List<Point>();

            for (int spielerIndex = 0; spielerIndex < spielerMax; spielerIndex++)
            {
                bool stadtPlatziert = false;

                while (!stadtPlatziert)
                {
                    int x = random.Next(0, felderxMax);
                    int y = random.Next(0, felderyMax);

                    if (felder[x, y].feldart == "Grass" && KeineStadtImUmkreis(x, y, stadtabstand, platzierteStadtPositionen))
                    {

                        Stadt neueStadt = new Stadt(felder[x, y], felder);
                        neueStadt.Besitzer = spieler[spielerIndex];
                        felder[x, y].StadtAufFeld = neueStadt;

                        neueStadt.textur.Location = new Point(felder[x, y].textur.Location.X + 5, felder[x, y].textur.Location.Y + 5);
                        neueStadt.textur.Tag = neueStadt;
                        neueStadt.textur.Click += new EventHandler(feld_Click);
                        neueStadt.SetzeEinflussRadius(spieler, spielerIndex);

                        this.Controls.Add(neueStadt.textur);
                        neueStadt.textur.BringToFront();

                        platzierteStadtPositionen.Add(new Point(x, y));
                        stadtPlatziert = true;
                    }
                }
            }
        }

        private bool KeineStadtImUmkreis(int x, int y, int abstand, List<Point> platziertePositionen)
        {
            foreach (var position in platziertePositionen)
            {
                int distanz = Math.Abs(position.X - x) + Math.Abs(position.Y - y);
                if (distanz < abstand)
                {
                    return false;
                }
            }
            return true;
        }

        private void construction_MouseEnter(object sender, EventArgs e)
        {
            construction.BackgroundImage = Properties.Resources.constructionglow;
        }

        private void construction_MouseLeave(object sender, EventArgs e)
        {
            construction.BackgroundImage = Properties.Resources.construction;
        }

        private void einnehmen_Click(object sender, EventArgs e)
        {
            if(lastClickedFeld != null && !lastClickedFeld.GehoertZuStadt) 
            {
                lastClickedFeld.textur.BackColor = Color.Red;
                lastClickedFeld.besitzer = spieler[aktuellerSpielerIndex];
                MessageBox.Show("Auf diesem Feld kann nun eine Stadt gebaut werden!");
                return;
            }
            MessageBox.Show("Hier geht das nicht!");
            return;
        }
    }
}

