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
        private List<Feld> alleFelder = new List<Feld>();
        public bool rekrutiermodus = false;
        public int truppenMax = 4;
        public int spielerMax = 4;
        public string truppeZumErstellen;

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
                spieler.Add(new Spieler(null, 150, 3, i + 1, Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)), null, null));
            }

            if (spieler.Count > 0)
            {
                aktuellerSpieler = spieler[aktuellerSpielerIndex];
            }

            Feldgenerierung();
            InitialisiereComboBox();

            MessageBox.Show($"Aktueller Spieler: {aktuellerSpieler.spielernummer}");
            UIAktualisierung();
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

                    int rescourcenEinteilung = rescourcen.Next(0, 6);
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
                        feld.rescourcen.Eisen = rescourcenAnzahl;
                    }
                    if (rescourcenEinteilung == 2 && feld.feldart == "Grass")
                    {
                        feld.rescourcen = new Kohle(10, rescourcenAnzahl);
                        feld.rescourcen.Kohle = rescourcenAnzahl;
                    }
                    if (rescourcenEinteilung == 3 && feld.feldart == "Grass")
                    {
                        feld.rescourcen = new Weizen(10, rescourcenAnzahl);
                        feld.rescourcen.Weizen = rescourcenAnzahl;
                    }

                    feld.textur.Size = new Size(feldgroesse, feldgroesse);
                    feld.textur.Location = new Point(j * feldgroesse, i * feldgroesse);
                    feld.textur.BackColor = Color.White;
                    feld.textur.Tag = feld;
                    feld.textur.Click += new EventHandler(feld_Click);

                    felder[i, j] = feld;
                    alleFelder.Add(feld);

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

            GeneriereStaedte(felderxMax, felderyMax);

        }
        public void feld_Click(object sender, EventArgs e)
        {
            var clickedObject = (sender as PictureBox).Tag;
            
            UIInfo.Show();
            UIInfo.Image = Properties.Resources.UI2;
            

            if (clickedObject is Truppe clickedTruppe)
            {
                if(rekrutiermodus == true) 
                {
                    return;
                }
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

                    switch (clickedFeld.rescourcen.name)
                    {
                        case "Eisen":
                            UIInfo.Image = Properties.Resources.UI2eisen;
                            break;
                        case "Kohle":
                            UIInfo.Image = Properties.Resources.UI2kohle;
                            break;
                        case "Weizen":
                            UIInfo.Image = Properties.Resources.UI2weizen;
                            break;
                        default:
                            UIInfo.Image = Properties.Resources.UI2;
                            break;
                    }
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
                    if (spieler[aktuellerSpielerIndex].bewegungspunkte > 0) 
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

                        spieler[aktuellerSpielerIndex].bewegungspunkte -= 1;
                        UIAktualisierung();

                        selectedTruppe.Darstellung.BackColor = Color.Blue;
                        EntferneBewegungsbereich(null);
                        selectedTruppe = null;
                        einnehmen.Hide();
                    }
                    else 
                    {
                        MessageBox.Show("Nicht genügend Bewegungspunkte!");
                        selectedTruppe.Darstellung.BackColor = Color.Blue;
                        selectedTruppe = null;
                        EntferneBewegungsbereich(null);
                    }
                    UpdateGame(selectedTruppe);
                    selectedTruppe.AktuellesFeld.EntferneTruppe();
                    clickedFeld.SetzeTruppe(selectedTruppe, aktuellerSpieler);

                    EntferneBewegungsbereich(null);
                    selectedTruppe = null;
                    einnehmen.Hide();
                }
                

            }
            if(rekrutiermodus == true) 
            {
                Truppe truppe = new Truppe();
                if (spieler[aktuellerSpielerIndex].geld >= truppe.Preis) 
                {
 
                  if (lastClickedFeld.besitzer == spieler[aktuellerSpielerIndex] && lastClickedFeld.TruppeAufFeld == null && truppeZumErstellen != null)
                  {
                      if (truppeZumErstellen == "Nahkämpfer")
                      {
                        Nahkaempfer truppe = new Nahkaempfer();
                        lastClickedFeld.SetzeTruppe(truppe, spieler[aktuellerSpielerIndex]);
                        truppe.textur.Tag = truppe;
                        spieler[aktuellerSpielerIndex].geld -= truppe.Preis;
                        truppe.textur.Click += new EventHandler(feld_Click);
                        UIAktualisierung();
                      }
                      else if (truppeZumErstellen == "Fernkämpfer")
                      {
                        Fernkaempfer truppe = new Fernkaempfer();
                        lastClickedFeld.SetzeTruppe(truppe, spieler[aktuellerSpielerIndex]);
                        truppe.textur.Tag = truppe;
                        spieler[aktuellerSpielerIndex].geld -= truppe.Preis;
                        truppe.textur.Click += new EventHandler(feld_Click);
                        UIAktualisierung();
                    }
                }
                else 
                {
                    truppe = null;
                    MessageBox.Show("Nicht genügend Geld!");
                }
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

                //Für jede Stadt, die ein Spieler besitzt, gibt es Einkommen

                foreach (var spieler in spieler)
                {
                    spieler.bewegungspunkte = 3;

                    // Für jede Stadt des Spielers wird Einkommen hinzugefügt
                    foreach (Stadt stadt in spieler.staedteBesitz)
                    {
                        spieler.geld += stadt.einkommen;
                    }
                    foreach (Farm farm in spieler.farmBesitz) 
                    {
                        spieler.rescourcenBesitz.Weizen += farm.weizenEinkommen;
                    } 
                }
            }

            aktuellerSpieler = spieler[aktuellerSpielerIndex];
            MessageBox.Show($"Spieler {aktuellerSpieler.spielernummer} ist dran");

            UIAktualisierung();
            spieler[aktuellerSpielerIndex].UpdateRessourcen(alleFelder);
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
            Nahkaempfer truppe = new Nahkaempfer();
            felder[i, j].SetzeTruppe(truppe, spieler[random.Next(0, 2)]);
            truppe.textur.Tag = truppe;
            
            truppe.textur.Click += new EventHandler(feld_Click);
            this.Controls.Add(truppe.textur);
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
                if (truppe is Nahkaempfer)
                    ItemPB.Image = Properties.Resources.melee;
                else if (truppe is Fernkaempfer)
                    ItemPB.Image = Properties.Resources.ranged;
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
                        spieler[spielerIndex].staedteBesitz.Add(neueStadt);
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

        

        private void einnehmen_Click(object sender, EventArgs e)
        {
            if(lastClickedFeld != null && !lastClickedFeld.GehoertZuStadt && lastClickedFeld.besitzer != spieler[aktuellerSpielerIndex]) 
            {
                lastClickedFeld.textur.BackColor = Color.Red;
                lastClickedFeld.besitzer = spieler[aktuellerSpielerIndex];
                MessageBox.Show("Auf diesem Feld kann nun eine Stadt gebaut werden!");
                return;
            }
            MessageBox.Show("Hier geht das nicht!");
            return;
        }

        private void construction_Click(object sender, EventArgs e)
        {
            if(lastClickedFeld == null) 
            {
                MessageBox.Show("Wähle Zunächst ein Feld aus!");
                return;
            }
            
            else if (lastClickedFeld.besitzer != spieler[aktuellerSpielerIndex])
            {
                if (stadtbauen.Visible == true)
                {
                    stadtbauen.Hide();
                    farmbauen.Hide();
                }
                else
                {
                    MessageBox.Show("Dieses Feld gehört dir nicht!");
                }
                return;
            }
            if(stadtbauen.Visible == false) 
            {
                stadtbauen.Show();
                farmbauen.Show();
            }
            else if(stadtbauen.Visible == true) 
            {
                stadtbauen.Hide();
                farmbauen.Hide();
            }
            return;
        }

        private void stadtbauen_Click(object sender, EventArgs e)
        {
            if (spieler[aktuellerSpielerIndex].bewegungspunkte > 1 && spieler[aktuellerSpielerIndex].geld >= 200 && felder[lastClickedFeld.position.X, lastClickedFeld.position.Y].TruppeAufFeld == null) 
            {
                if (lastClickedFeld.besitzer != spieler[aktuellerSpielerIndex])
                {
                    MessageBox.Show("Dieses Feld gehört dir nicht!");
                    return;
                }
                if (lastClickedFeld.besitzer == spieler[aktuellerSpielerIndex] && lastClickedFeld.textur.BackColor == spieler[aktuellerSpielerIndex].SpielerFarbe)
                {
                    MessageBox.Show("Innerhalb eigender Gebiete kann keine weitere Stadt errichtet werden!");
                    return;
                }
                List<Point> platzierteStadtPositionen = new List<Point>();
                int stadtabstand = 5;

                int x = lastClickedFeld.position.X;
                int y = lastClickedFeld.position.Y;

                if (lastClickedFeld.feldart == "Grass" && KeineStadtImUmkreis(x, y, stadtabstand, platzierteStadtPositionen))
                {
                    Stadt neueStadt = new Stadt(lastClickedFeld, felder);
                    neueStadt.Besitzer = spieler[aktuellerSpielerIndex];
                    spieler[aktuellerSpielerIndex].staedteBesitz.Add(neueStadt);

                    

                    lastClickedFeld.StadtAufFeld = neueStadt;

                    neueStadt.textur.Location = new Point(lastClickedFeld.textur.Location.X + 5, lastClickedFeld.textur.Location.Y + 5);
                    neueStadt.textur.Tag = neueStadt;
                    neueStadt.textur.Click += new EventHandler(feld_Click);
                    neueStadt.SetzeEinflussRadius(spieler, aktuellerSpielerIndex);

                    this.Controls.Add(neueStadt.textur);
                    neueStadt.textur.BringToFront();

                    platzierteStadtPositionen.Add(new Point(x, y));

                    spieler[aktuellerSpielerIndex].bewegungspunkte -= 2;
                    spieler[aktuellerSpielerIndex].geld -= 200;
                    UIAktualisierung();
                }
                return;
            }
            else if(spieler[aktuellerSpielerIndex].geld < 200)
            {
                MessageBox.Show("Nicht genügend Geld!");
            }
            else if(spieler[aktuellerSpielerIndex].bewegungspunkte <= 1)
            {
                MessageBox.Show("Nicht genügend Bewegungspunkte!");
            }
            else 
            {
                MessageBox.Show("Man kann kein Stadt auf dem selben Feld bauen, auf dem eine Truppe steht!");
            }
        }

        private void farmbauen_Click(object sender, EventArgs e)
        {
            if (spieler[aktuellerSpielerIndex].bewegungspunkte > 0 && spieler[aktuellerSpielerIndex].geld >= 100 && felder[lastClickedFeld.position.X, lastClickedFeld.position.Y].TruppeAufFeld == null)
            {
                if (lastClickedFeld.besitzer != spieler[aktuellerSpielerIndex])
                {
                    MessageBox.Show("Dieses Feld gehört dir nicht!");
                    return;
                }
                List<Point> platzierteFarmPositionen = new List<Point>();

                int x = lastClickedFeld.position.X;
                int y = lastClickedFeld.position.Y;

                if (lastClickedFeld.feldart == "Grass")
                {
                    Farm neueFarm = new Farm(lastClickedFeld, felder);
                    neueFarm.Besitzer = spieler[aktuellerSpielerIndex];
                    spieler[aktuellerSpielerIndex].farmBesitz.Add(neueFarm);



                    lastClickedFeld.FarmAufFeld = neueFarm;

                    neueFarm.textur.Location = new Point(lastClickedFeld.textur.Location.X + 5, lastClickedFeld.textur.Location.Y + 5);
                    neueFarm.textur.Tag = neueFarm;

                    this.Controls.Add(neueFarm.textur);
                    neueFarm.textur.BringToFront();

                    platzierteFarmPositionen.Add(new Point(x, y));

                    spieler[aktuellerSpielerIndex].bewegungspunkte -= 1;
                    spieler[aktuellerSpielerIndex].geld -= 100;
                    UIAktualisierung();
                }
                return;
            }
            else if (spieler[aktuellerSpielerIndex].geld < 100)
            {
                MessageBox.Show("Nicht genügend Geld!");
            }
            else if (spieler[aktuellerSpielerIndex].bewegungspunkte < 1)
            {
                MessageBox.Show("Nicht genügend Bewegungspunkte!");
            }
            else
            {
                MessageBox.Show("Man kann keine Farm auf dem selben Feld bauen, auf dem eine Truppe steht!");
            }

        }
        private void recruitSoldiers_MouseEnter(object sender, EventArgs e)
        {
            recruitSoldiers.BackgroundImage = Properties.Resources.recruitglow;
        }

        private void recruitSoldiers_MouseLeave(object sender, EventArgs e)
        {
            recruitSoldiers.BackgroundImage = Properties.Resources.recruit;
        }

        private void construction_MouseEnter(object sender, EventArgs e)
        {
            construction.BackgroundImage = Properties.Resources.constructionglow;
        }

        private void construction_MouseLeave(object sender, EventArgs e)
        {
            construction.BackgroundImage = Properties.Resources.construction;
        }

        private void recruitSoldiers_Click(object sender, EventArgs e)
        {
            if(rekrutiermodus == false) 
            {
                truppeComboBox.Visible = true;
                rekrutiermodus = true;
                MessageBox.Show("Du kannst nun ein Feld auswählen, um darin Truppen zu Platzieren!");
            }
            else if(rekrutiermodus == true) 
            {
                truppeComboBox.Visible = false;
                rekrutiermodus = false;
                MessageBox.Show("Rekrutiermodus ist aus!");
            }
            
        }
        //Aktualiseren der UI-Elemente, welche das Geld und die Bewegungspunkte anzeigen.
        public void UIAktualisierung() 
        {
            
            geldanzeige.Text = spieler[aktuellerSpielerIndex].geld.ToString();
            bewpunktanzeige.Text = spieler[aktuellerSpielerIndex].bewegungspunkte.ToString();
            momentanerSpieler.Text = $"Spieler {aktuellerSpielerIndex + 1}";

            if(rescourceinventory.Visible == true) 
            {
                spieler[aktuellerSpielerIndex].UpdateRessourcen(alleFelder);

                rescourceinventory.Show();
                rescourcenlabel.Show();
                rescourcenlabel.BringToFront();

                eisenInventory.Show(); eisenInventory.BringToFront();
                eisenAnzahl.Show(); eisenAnzahl.BringToFront();

                coalInventory.Show(); coalInventory.BringToFront();
                coalAnzahl.Show(); coalAnzahl.BringToFront();

                steelInventory.Show(); steelInventory.BringToFront();
                steelAnzahl.Show(); steelAnzahl.BringToFront();

                wheatInventory.Show(); wheatInventory.BringToFront();
                wheatAnzahl.Show(); wheatAnzahl.BringToFront();
            }
            eisenAnzahl.Text = $"{spieler[aktuellerSpielerIndex].rescourcenBesitz.Eisen}";
            coalAnzahl.Text = $"{spieler[aktuellerSpielerIndex].rescourcenBesitz.Kohle}";
            steelAnzahl.Text = $"{spieler[aktuellerSpielerIndex].rescourcenBesitz.Stahl}";
            wheatAnzahl.Text = $"{spieler[aktuellerSpielerIndex].rescourcenBesitz.Weizen}";
        }

        private void rescourcenFenster_Click(object sender, EventArgs e)
        {
            UIAktualisierung();
            if (rescourceinventory.Visible == true)
            {
                rescourceinventory.Hide();
                rescourcenlabel.Hide();

                eisenInventory.Hide();
                eisenAnzahl.Hide();
                coalInventory.Hide();
                coalAnzahl.Hide();
                steelInventory.Hide();
                steelAnzahl.Hide();
                wheatInventory.Hide();
                wheatAnzahl.Hide();
            }
            else 
            {
                spieler[aktuellerSpielerIndex].UpdateRessourcen(alleFelder);

                rescourceinventory.Show();
                rescourcenlabel.Show();
                rescourcenlabel.BringToFront();

                eisenInventory.Show(); eisenInventory.BringToFront();
                eisenAnzahl.Show(); eisenAnzahl.BringToFront();

                coalInventory.Show(); coalInventory.BringToFront();
                coalAnzahl.Show(); coalAnzahl.BringToFront();

                steelInventory.Show(); steelInventory.BringToFront();
                steelAnzahl.Show(); steelAnzahl.BringToFront();

                wheatInventory.Show(); wheatInventory.BringToFront();
                wheatAnzahl.Show(); wheatAnzahl.BringToFront();
            }
            eisenAnzahl.Text = $"{spieler[aktuellerSpielerIndex].rescourcenBesitz.Eisen}";
            coalAnzahl.Text = $"{spieler[aktuellerSpielerIndex].rescourcenBesitz.Kohle}";
            steelAnzahl.Text = $"{spieler[aktuellerSpielerIndex].rescourcenBesitz.Stahl}";
            wheatAnzahl.Text = $"{spieler[aktuellerSpielerIndex].rescourcenBesitz.Weizen}";
        }
        public void InitialisiereComboBox()
       {     
            truppeComboBox.Items.Add(new { Typ = "Nahkämpfer" });
            truppeComboBox.Items.Add(new { Typ = "Fernkämpfer" });
            truppeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            truppeComboBox.Size = new Size(150, 30);
            truppeComboBox.Location = new Point(10, 50);
            truppeComboBox.SelectedIndexChanged += TruppenComboBox_SelectedIndexChanged;
        }
        public void TruppenComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combobox = sender as ComboBox;
            if (combobox.SelectedItem != null)
            {
                var selectedTruppe = combobox.SelectedItem as dynamic;
                string truppeTyp = selectedTruppe.Typ;
                truppeZumErstellen = truppeTyp;
            }
             
        }
    }
}

