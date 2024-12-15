using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
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
        private Squad selectedSquad = null;
        private Feld[,] felder;
        private List<Feld> alleFelder = new List<Feld>();
        public bool rekrutiermodus = false;
        public int spielerMax = 4;
        public string truppeZumErstellen;
        private Panel squadPanel;
        private ListBox squadTruppenLB;
        private Panel squadErstellenPanel;

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
            ErstelleSquadPanel();
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
            squadPanelBtn.Visible = false;

            if (clickedObject is Truppe clickedTruppe)
            {
                if (rekrutiermodus == true)
                {
                    return;
                }
                if (selectedTruppe != null && selectedTruppe != clickedTruppe)
                {
                    EntferneBewegungsbereich(null);
                    if (selectedTruppe.Angreifen(clickedTruppe))
                        ZeigeSchaden(selectedTruppe.textur, selectedTruppe.Schaden);
                    selectedTruppe = null;
                    HideUIInfo();
                    return;
                }
                if (selectedSquad != null)
                {
                    EntferneBewegungsbereich(null);
                    if(selectedSquad.Angreifen(clickedTruppe))
                        ZeigeSchaden(selectedTruppe.textur, selectedTruppe.Schaden);
                    selectedSquad = null;
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
            else if (clickedObject is Squad clickedSquad)
            {
                if (rekrutiermodus)
                    return;
                if (selectedSquad != null && selectedSquad != clickedSquad)
                {
                    EntferneBewegungsbereich(null);
                    if(selectedSquad.Angreifen(clickedSquad))
                        ZeigeSchaden(selectedSquad.textur, selectedSquad.TrueDamage(clickedSquad.AktuellesFeld));
                    selectedSquad = null;
                    HideUIInfo();
                    return;
                }
                else if (selectedTruppe != null)
                {
                    EntferneBewegungsbereich(null);
                    if (selectedTruppe.Angreifen(clickedSquad))
                        ZeigeSchaden(clickedSquad.textur, clickedSquad.Gesamtschaden);
                    selectedTruppe = null;
                    HideUIInfo();
                    return;
                }
                if (clickedSquad.Besitzer != aktuellerSpieler)
                {
                    MessageBox.Show("Das ist nicht dein Squad!");
                    return;
                }
                if (selectedSquad == null)
                {
                    selectedSquad = clickedSquad;
                    squadPanelBtn.Visible = true;
                    MakiereBewegungsreichweite(selectedSquad);

                    UpdateUIInfo(clickedSquad);
                    einnehmen.Show();
                }
                else
                {
                    EntferneBewegungsbereich(null);
                    selectedSquad = null;
                    truppenLebenLB.Hide();
                    truppenSchadenLB.Hide();
                    einnehmen.Hide();
                    ItemPB.Hide();
                    titelLabel.Hide();
                }
            }

            else if (clickedObject is Stadt clickedStadt)
            {
                selectedStadt = clickedStadt;
                if (clickedStadt != null && Gewinnueberpruefung() != true)
                {
                    if (clickedStadt.Besitzer == aktuellerSpieler)
                    {
                        UpdateUIInfo(clickedStadt);

                        selectedTruppe = null;
                        EntferneBewegungsbereich(null);
                        return;
                    }
                    if (selectedTruppe != null && spieler[aktuellerSpielerIndex].bewegungspunkte != 0)
                    {
                        EntferneBewegungsbereich(null);
                        if (selectedTruppe.Angreifen(clickedStadt))
                            ZeigeSchaden(selectedTruppe.textur, selectedTruppe.Schaden);
                        selectedTruppe = null;
                        HideUIInfo();
                        EntferneBewegungsbereich(null);
                        aktuellerSpieler.bewegungspunkte -= 1;
                        UIAktualisierung();
                        return;
                    }
                    else if (selectedSquad != null && spieler[aktuellerSpielerIndex].bewegungspunkte != 0)
                    {
                        if (selectedSquad.Angreifen(clickedStadt))
                            ZeigeSchaden(selectedSquad.textur, selectedSquad.TrueDamage(clickedStadt.startFeld));
                        selectedSquad = null;
                        HideUIInfo();
                        EntferneBewegungsbereich(null);
                        aktuellerSpieler.bewegungspunkte -= 1;
                        UIAktualisierung();
                        return;
                    }
                    else if (selectedTruppe != null || selectedSquad != null & spieler[aktuellerSpielerIndex].bewegungspunkte == 0)
                    {
                        MessageBox.Show("Nicht genügend Bewegungspunkte!");
                        return;
                    }
                    MessageBox.Show("Das ist nicht deine Stadt!");
                    return;
                }

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
                if (lastClickedFeld == clickedFeld)
                {
                    if (clickedFeld.GehoertZuStadt && clickedFeld.besitzer != null || clickedFeld.besitzer != null)
                    {
                        clickedFeld.textur.BackColor = clickedFeld.besitzer.SpielerFarbe;
                        clickedFeld.textur.Image = Properties.Resources.grasstransparent;
                    }
                    else
                    {
                        clickedFeld.textur.BackColor = Color.White;
                        clickedFeld.textur.Image = Properties.Resources.grass;
                    }

                    UIInfo.Hide();
                    anzahlRes.Hide();

                    lastClickedFeld = null;
                    return;
                }

                if (lastClickedFeld != null)
                {
                    if (lastClickedFeld.GehoertZuStadt && lastClickedFeld.besitzer != null || lastClickedFeld.besitzer != null)
                    {
                        lastClickedFeld.textur.BackColor = lastClickedFeld.besitzer.SpielerFarbe;
                        lastClickedFeld.textur.Image = Properties.Resources.grasstransparent;
                    }
                    else
                    {
                        lastClickedFeld.textur.BackColor = Color.White;
                        lastClickedFeld.textur.Image = Properties.Resources.grass;
                    }
                }

                clickedFeld.textur.BackColor = Color.Gray;
                clickedFeld.textur.Image = Properties.Resources.grasstransparent;

                UIInfo.Show();

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
                        EntferneBewegungsbereich(null);
                        selectedTruppe = null;
                        einnehmen.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Nicht genügend Bewegungspunkte!");
                        einnehmen.Hide();
                        selectedTruppe = null;
                        EntferneBewegungsbereich(null);
                    }
                    UpdateGame(selectedTruppe);
                }
                else if (selectedSquad != null && clickedFeld.feldart == "Grass")
                {
                    if (aktuellerSpieler.bewegungspunkte > 0)
                    {
                        if (selectedSquad.BerechneDistanz(clickedFeld) <= selectedSquad.Bewegungsreichweite)
                        {
                            selectedSquad.BewegeZu(clickedFeld);
                            EntferneBewegungsbereich(null);
                            selectedSquad = null;
                            einnehmen.Hide();
                            aktuellerSpieler.bewegungspunkte -= 1;
                            UIAktualisierung();
                        }
                        else
                        {
                            selectedSquad = null;
                            //lastClickedFeld.textur.BackColor = Color.White;
                            //lastClickedFeld.textur.Image = Properties.Resources.grass;
                            clickedFeld.textur.BackColor = Color.Gray;
                            clickedFeld.textur.Image = Properties.Resources.grasstransparent;
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nicht genügend Bewegungspunkte!");
                        selectedTruppe = null;
                        EntferneBewegungsbereich(null);
                    }
                }
                lastClickedFeld = clickedFeld;

            }
            if (rekrutiermodus == true && Gewinnueberpruefung() != true)
            {
                if (lastClickedFeld.besitzer == spieler[aktuellerSpielerIndex] && lastClickedFeld.TruppeAufFeld == null && truppeZumErstellen != null)
                {
                    if (truppeZumErstellen == "Nahkämpfer")
                    {
                        Nahkaempfer truppe = new Nahkaempfer();
                        if (!(spieler[aktuellerSpielerIndex].geld >= truppe.Preis))
                            return;
                        lastClickedFeld.SetzeTruppe(truppe, spieler[aktuellerSpielerIndex]);
                        truppe.textur.Tag = truppe;
                        spieler[aktuellerSpielerIndex].geld -= truppe.Preis;
                        truppe.textur.Click += new EventHandler(feld_Click);
                        UIAktualisierung();
                    }
                    else if (truppeZumErstellen == "Fernkämpfer")
                    {
                        Fernkaempfer truppe = new Fernkaempfer();
                        if (!(spieler[aktuellerSpielerIndex].geld >= truppe.Preis))
                            return;
                        lastClickedFeld.SetzeTruppe(truppe, spieler[aktuellerSpielerIndex]);
                        truppe.textur.Tag = truppe;
                        spieler[aktuellerSpielerIndex].geld -= truppe.Preis;
                        truppe.textur.Click += new EventHandler(feld_Click);
                        UIAktualisierung();
                    }
                }
                else if(lastClickedFeld.besitzer != spieler[aktuellerSpielerIndex]) 
                {
                    MessageBox.Show("Das ist nicht dein Feld!");
                }
                else
                {
                    MessageBox.Show("Nicht genügend Geld!");
                }
            }
        }
        public void MakiereBewegungsreichweite(object o)
        {
            if (o == null)
                return;
            Feld aktuellesFeld = null;
            int bewegungsreichweite = 0;
            if (o is Truppe truppe)
            {
                aktuellesFeld = truppe.AktuellesFeld;
                bewegungsreichweite = truppe.Bewegungsreichweite;
            }
            else if (o is Squad squad)
            {
                aktuellesFeld = squad.AktuellesFeld;
                bewegungsreichweite = squad.Bewegungsreichweite;
            }
            if (aktuellesFeld == null)
                return;

            int startX = aktuellesFeld.position.X;
            int startY = aktuellesFeld.position.Y;

            lastClickedFeld = null;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    int distanz = Math.Abs(startX - i) + Math.Abs(startY - j);
                    if (distanz <= bewegungsreichweite)

                    {
                        if (felder[i, j].feldart != "Water") 
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
                if(feld.besitzer != null && !feld.GehoertZuStadt) 
                {
                    feld.textur.BackColor = feld.besitzer.SpielerFarbe;
                    feld.textur.Image = Properties.Resources.grasstransparent;
                }
            }

        }

        public void Spielerwechsel()
        {
            if (aktuellerSpieler.rescourcenBesitz.Weizen < 0)
            {
                //MessageBox.Show("Rescourcendefizit! Sorge für Weizenproduktion, oder deine Zivilisation stirbt aus!");
                SpielstandUpdate();
            }
            Gewinnueberpruefung();
            aktuellerSpielerIndex++;
            

            if (aktuellerSpielerIndex >= spieler.Count)
            {
                if (Gewinnueberpruefung() == true)
                {
                    return;
                }
                aktuellerSpielerIndex = 0;
                aktuellerSpieler = spieler[aktuellerSpielerIndex];
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
                }
                foreach (var spieler in spieler) 
                {
                    foreach (var feld in alleFelder)
                    {

                        if (feld.FarmAufFeld != null && feld.FarmAufFeld.Besitzer == spieler)
                        {
                            feld.rescourcen.Weizen += feld.FarmAufFeld.weizenEinkommen;
                        }
                        if (feld.StahlwerkAufFeld != null && feld.StahlwerkAufFeld.Besitzer == spieler)
                        {
                            if (spieler.rescourcenBesitz.Eisen > 0 && spieler.rescourcenBesitz.Kohle > 0)
                            {
                                if (feld.rescourcen == null)
                                {
                                    feld.rescourcen = new Stahl(40, feld.StahlwerkAufFeld.StahlEinkommen);
                                    feld.rescourcen.Stahl += feld.StahlwerkAufFeld.StahlEinkommen;
                                }
                                else
                                {
                                    feld.rescourcen.Stahl += feld.StahlwerkAufFeld.StahlEinkommen;
                                }
                            }
                        }
                    }
                }
                foreach (var spieler in spieler)
                {
                    int abziehbarResVar = spieler.stahlwerkBesitz.Count;

                    int abziehbarVar = spieler.staedteBesitz.Count;

                    foreach (var feld in alleFelder)
                    {
                        if (feld.besitzer == spieler)
                        {

                            if (feld.rescourcen != null)
                            {
                                if (abziehbarVar != 0)
                                {
                                    if(spieler.rescourcenBesitz.Weizen >= 0) 
                                    {
                                        feld.rescourcen.Weizen -= 10;
                                        abziehbarVar--;
                                    }
                                }
                            }
                            if(spieler.rescourcenBesitz.Eisen >= 10 && spieler.rescourcenBesitz.Kohle >= 10) 
                            {
                                if (feld.rescourcen != null)
                                {
                                    if (abziehbarResVar != 0)
                                    {
                                        feld.rescourcen.Kohle -= 10;
                                        feld.rescourcen.Eisen -= 10;
                                        abziehbarResVar--;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            aktuellerSpieler = spieler[aktuellerSpielerIndex];
            MessageBox.Show($"Spieler {aktuellerSpieler.spielernummer} ist dran");

            if(aktuellerSpieler.stahlwerkBesitz.Count != 0 && aktuellerSpieler.rescourcenBesitz.Kohle < 10 | aktuellerSpieler.rescourcenBesitz.Eisen < 10) 
            {
                MessageBox.Show("Eisen und Kohlemagnel, Stahlwerke können nicht Arbeiten!");
            }

            spieler[aktuellerSpielerIndex].UpdateRessourcen(alleFelder);
            UIAktualisierung();
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
                ItemPB.BackgroundImage = Properties.Resources.ui_wood;
                truppenLebenLB.Text = $"Siedler: {stadt.Einwohner}";
                titelLabel.Text = stadt.Name;
            }
            else if (o is Squad squad)
            {
                truppenLebenLB.Text = $"Leben: {squad.Gesamtleben}";
                truppenSchadenLB.Text = $"Schaden: {squad.Gesamtschaden}";
                truppenSchadenLB.Visible = true;
                ItemPB.Image = Properties.Resources.squad_wappen;
                titelLabel.Text = squad.ToString();
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

                        neueStadt.textur.Location = new Point(felder[x, y].textur.Location.X, felder[x, y].textur.Location.Y);
                        neueStadt.textur.Tag = neueStadt;
                        neueStadt.textur.BackColor = neueStadt.Besitzer.SpielerFarbe;
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
            if (lastClickedFeld != null && !lastClickedFeld.GehoertZuStadt && lastClickedFeld.besitzer != spieler[aktuellerSpielerIndex])
            {
                lastClickedFeld.textur.Image = Properties.Resources.grasstransparent;
                lastClickedFeld.textur.BackColor = spieler[aktuellerSpielerIndex].SpielerFarbe;
                lastClickedFeld.besitzer = spieler[aktuellerSpielerIndex];

                if(lastClickedFeld.FarmAufFeld != null) 
                {
                    lastClickedFeld.FarmAufFeld.Besitzer = spieler[aktuellerSpielerIndex];
                    spieler[aktuellerSpielerIndex].farmBesitz.Add(lastClickedFeld.FarmAufFeld);
                }
                if(lastClickedFeld.StahlwerkAufFeld != null) 
                {
                    lastClickedFeld.StahlwerkAufFeld.Besitzer = spieler[aktuellerSpielerIndex];
                    spieler[aktuellerSpielerIndex].stahlwerkBesitz.Add(lastClickedFeld.StahlwerkAufFeld);
                }

                MessageBox.Show("Dieses Feld gehört nun Dir!");
                return;
            }
            MessageBox.Show("Hier geht das nicht!");
            return;
        }

        private void construction_Click(object sender, EventArgs e)
        {
            if (Gewinnueberpruefung() == true) { return; }
            if (lastClickedFeld == null)
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
                    stahlwerkbauen.Hide();
                }
                else
                {
                    MessageBox.Show("Dieses Feld gehört dir nicht!");
                }
                return;
            }
            if (stadtbauen.Visible == false)
            {
                stadtbauen.Show();
                farmbauen.Show();
                stahlwerkbauen.Show();
            }
            else if (stadtbauen.Visible == true)
            {
                stadtbauen.Hide();
                farmbauen.Hide();
                stahlwerkbauen.Hide();
            }
            return;
        }

        private void stadtbauen_Click(object sender, EventArgs e)
        {
            if (Gewinnueberpruefung() == true) { return; }
            if (lastClickedFeld == null) 
            {
                MessageBox.Show("Wähle zunächst ein Feld aus!");
                return;
            }
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
                if(lastClickedFeld.StahlwerkAufFeld != null || lastClickedFeld.FarmAufFeld != null || lastClickedFeld.StadtAufFeld != null) 
                {
                    MessageBox.Show("Man kann keine Gebäude auf andere Bauen!");
                    return;
                }
                if (spieler[aktuellerSpielerIndex].rescourcenBesitz.Stahl < 30) 
                {
                    MessageBox.Show("Nicht genügend Stahl!");
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


                    bool abziehbarRes = true;
                    foreach (var feld in felder) 
                    {
                        if(abziehbarRes == true) 
                        {
                            if (feld.besitzer == spieler[aktuellerSpielerIndex])
                            {
                                if (feld.StahlwerkAufFeld != null)
                                {
                                    feld.rescourcen.Stahl -= 30;
                                    abziehbarRes = false;
                                }
                            }
                        }
                    }
                    UIAktualisierung();
                }
                return;
            }
            else if (spieler[aktuellerSpielerIndex].geld < 200)
            {
                MessageBox.Show("Nicht genügend Geld!");
            }
            else if (spieler[aktuellerSpielerIndex].bewegungspunkte <= 1)
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
            if (Gewinnueberpruefung() == true) { return; }
            if (lastClickedFeld == null)
            {
                MessageBox.Show("Wähle zunächst ein Feld aus!");
                return;
            }
            if (lastClickedFeld.rescourcen == null)
            {
                MessageBox.Show("Man kann nur Farms auf Felder Bauen, die Weizen enthalten!");
                return;
            }
            else if (lastClickedFeld.rescourcen.Weizen == 0)
            {
                MessageBox.Show("Man kann nur Farms auf Felder Bauen, die Weizen enthalten!");
                return;
            }
            if(lastClickedFeld.FarmAufFeld != null) 
            {
                MessageBox.Show("Man kann keine Gebäude auf andere Bauen!");
                return;
            }
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

        private void stahlwerkbauen_Click(object sender, EventArgs e)
        {
            if (Gewinnueberpruefung() == true) { return; }
            if (lastClickedFeld == null)
            {
                MessageBox.Show("Wähle zunächst ein Feld aus!");
                return;
            }
            if (lastClickedFeld.rescourcen != null)
            {
                MessageBox.Show("Man kann nur Stahlwerke auf Felder Bauen, die keine Rescourcen enthalten!");
                return;
            }
            if (lastClickedFeld.StahlwerkAufFeld != null)
            {
                MessageBox.Show("Man kann keine Gebäude auf andere Bauen!");
                return;
            }
            if (spieler[aktuellerSpielerIndex].bewegungspunkte > 0 && spieler[aktuellerSpielerIndex].geld >= 100 && felder[lastClickedFeld.position.X, lastClickedFeld.position.Y].TruppeAufFeld == null)
            {
                if (lastClickedFeld.besitzer != spieler[aktuellerSpielerIndex])
                {
                    MessageBox.Show("Dieses Feld gehört dir nicht!");
                    return;
                }
                List<Point> platzierteStahlPositionen = new List<Point>();

                int x = lastClickedFeld.position.X;
                int y = lastClickedFeld.position.Y;

                if (lastClickedFeld.feldart == "Grass")
                {
                    Stahlwerk neueStahlwerke = new Stahlwerk(lastClickedFeld, felder);
                    neueStahlwerke.Besitzer = spieler[aktuellerSpielerIndex];
                    spieler[aktuellerSpielerIndex].stahlwerkBesitz.Add(neueStahlwerke);



                    lastClickedFeld.StahlwerkAufFeld = neueStahlwerke;

                    neueStahlwerke.textur.Location = new Point(lastClickedFeld.textur.Location.X + 5, lastClickedFeld.textur.Location.Y + 5);
                    neueStahlwerke.textur.Tag = neueStahlwerke;

                    this.Controls.Add(neueStahlwerke.textur);
                    neueStahlwerke.textur.BringToFront();

                    platzierteStahlPositionen.Add(new Point(x, y));

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
            if (Gewinnueberpruefung() == true) { return; }
            if (rekrutiermodus == false)
            {
                truppeComboBox.Visible = true;
                rekrutiermodus = true;
                MessageBox.Show("Du kannst nun ein Feld auswählen, um darin Truppen zu Platzieren!");
            }
            else if (rekrutiermodus == true)
            {
                truppeComboBox.Visible = false;
                rekrutiermodus = false;
                MessageBox.Show("Rekrutiermodus ist aus!");
            }

        }
        //Aktualiseren der UI-Elemente, welche das Geld und die Bewegungspunkte anzeigen.
        public void UIAktualisierung()
        {
            if(Gewinnueberpruefung() == true) { return; }
            geldanzeige.Text = spieler[aktuellerSpielerIndex].geld.ToString();
            bewpunktanzeige.Text = spieler[aktuellerSpielerIndex].bewegungspunkte.ToString();
            momentanerSpieler.Text = $"Spieler {spieler[aktuellerSpielerIndex]}";
            aktuellerSpielerFarbe.BackColor = spieler[aktuellerSpielerIndex].SpielerFarbe;
            aktuellerSpielerFarbe.BackgroundImage = Properties.Resources.grasstransparent;

            if (rescourceinventory.Visible == true)
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
            if (Gewinnueberpruefung() == true) { return; }
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

        public bool Gewinnueberpruefung()
        {
            foreach (var Spieler in spieler)
            {
                Console.WriteLine($"{Spieler.staedteBesitz.Count}");
                if (Spieler.staedteBesitz.Count == 0)
                {
                    MessageBox.Show("Spieler ist Raus!");
                    spieler.Remove(Spieler);
                    foreach(var feld in felder) 
                    {
                        if(feld.TruppeAufFeld != null) 
                        {
                            if(feld.TruppeAufFeld.Besitzer == Spieler) 
                            {
                                feld.EntferneTruppe();
                            }
                        }
                        if(feld.SquadAufFeld != null) 
                        {
                            if(feld.SquadAufFeld.Besitzer == Spieler) 
                            {
                                feld.EntferneSquad();
                            }
                        }
                    }
                    Gewinnueberpruefung();
                    return false;
                }
                if (spieler.Count == 1)
                {
                    MessageBox.Show($"Spieler {Spieler.ToString()} hat gewonnen!");
                    momentanerSpieler.Text = $"Spieler {Spieler.ToString()}";
                    momentanerSpieler.BackColor = Spieler.SpielerFarbe;
                    return true;
                }
            }
            return false;
        }
        public void ZeigeSchaden(PictureBox textur, int schaden)
        {
            if (textur == null || textur.Parent == null)
                return;
            Label schadenLabel = new Label
            {
                Text = $"-{schaden}",
                ForeColor = Color.Red,
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.Transparent,
                AutoSize = true
            };
            schadenLabel.Location = new Point(textur.Location.X + textur.Width / 2 - schadenLabel.Width / 2, textur.Location.Y - 20);
            textur.Parent.Controls.Add(schadenLabel);
            schadenLabel.BringToFront();
            Timer timer = new Timer
            {
                Interval = 1000
            };
            timer.Tick += (sender, e) =>
            {
                textur.Parent.Controls.Remove(schadenLabel);
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }
        private void recruitSquad_Click(object sender, EventArgs e)
        {
            if (squadErstellenPanel != null && squadErstellenPanel.Visible)
            {
                return;
            }
            if (Gewinnueberpruefung() == true) { return; }
            squadErstellenPanel = new Panel
            {
                Size = new Size(300, 400),
                Location = new Point(750, 100),
                BorderStyle = BorderStyle.FixedSingle,
                BackgroundImage = Properties.Resources.ui_wood,
                Visible = false
            };
            Label infoLabel = new Label
            {
                Text = "Wähle Truppen:",
                BackColor = ColorTranslator.FromHtml("#7A5A3F"),
                Location = new Point(10, 10),
                Size = new Size(280, 20),
                ForeColor = Color.White
            };
            squadErstellenPanel.Controls.Add(infoLabel);
            ListBox truppenAnzeige = new ListBox
            {
                Location = new Point(10, 40),
                Size = new Size(280, 200),
                BackColor = ColorTranslator.FromHtml("#8C5028"),
                ForeColor = Color.White
            };
            squadErstellenPanel.Controls.Add(truppenAnzeige);

            Button nahkaempferButton = new Button
            {
                Text = "Nahkämpfer + ",
                Location = new Point(10, 260),
                Size = new Size(120, 30),
                BackColor = ColorTranslator.FromHtml("#7A5A3F"),
                ForeColor = Color.White
            };
            nahkaempferButton.Click += (btnSender, btnE) =>
            {
                Truppe neueTruppe = new Nahkaempfer();
                truppenAnzeige.Items.Add(neueTruppe.ToString());
            };

            squadErstellenPanel.Controls.Add(nahkaempferButton);
            Button fernkaempferButton = new Button
            {
                Text = "Fernkämpfer +",
                Location = new Point(150, 260),
                Size = new Size(120, 30),
                BackColor = ColorTranslator.FromHtml("#7A5A3F"),
                ForeColor = Color.White
            };
            fernkaempferButton.Click += (btnSender, btnE) =>
            {
                Truppe neueTruppe = new Fernkaempfer();
                truppenAnzeige.Items.Add(neueTruppe.ToString());
            };
            squadErstellenPanel.Controls.Add(fernkaempferButton);

            Button confirmBtn = new Button
            {
                Text = "Erstellen",
                Location = new Point(10, 310),
                Size = new Size(120, 30),
                BackColor = ColorTranslator.FromHtml("#7A5A3F"),
                ForeColor = Color.White
            };
            confirmBtn.Click += (btnSender, btnE) =>
            {
                int preis = 0;
                if (preis > aktuellerSpieler.geld)
                {
                    MessageBox.Show("Du hast nicht genug Geld");
                    CancelButton.PerformClick();
                    return;
                }
                List<Feld> ziel = aktuellerSpieler.staedteBesitz[0].stadtFlaeche;
                Feld freiesFeld = new Feld();
                foreach (Feld feld in ziel)
                {
                    if (feld != null && feld.feldart == "Grass" && feld.TruppeAufFeld == null && feld.SquadAufFeld == null)
                    {
                        freiesFeld = feld;
                        break;
                    }
                }
                if (freiesFeld == null || truppenAnzeige.Items.Count == 0)
                    return;
                Squad neuesSquad = new Squad(aktuellerSpieler, freiesFeld);
                foreach (var item in truppenAnzeige.Items)
                {
                    Truppe neueTruppe;

                    if (item.ToString() == "Nahkämpfer")
                    {
                        neueTruppe = new Nahkaempfer();
                        preis += neueTruppe.Preis;
                    }
                    else if (item.ToString() == "Fernkämpfer")
                    {
                        neueTruppe = new Fernkaempfer();
                        preis += neueTruppe.Preis;
                    }
                    else
                        continue;
                    neueTruppe.Besitzer = aktuellerSpieler;
                    neuesSquad.TruppeHinzufuegen(neueTruppe);
                }
                if (preis > aktuellerSpieler.geld)
                {
                    MessageBox.Show("Du hast nicht genügend Geld");
                    return;
                }
                Controls.Add(neuesSquad.textur);
                neuesSquad.textur.Click += new EventHandler(feld_Click);
                neuesSquad.textur.BringToFront();
                freiesFeld.SquadAufFeld = neuesSquad;
                aktuellerSpieler.geld -= preis;
                UIAktualisierung();
                neuesSquad.textur.Tag = neuesSquad;
                squadErstellenPanel.Hide();
            };
            squadErstellenPanel.Visible = true;
            squadErstellenPanel.Controls.Add(confirmBtn);

            Button cancelBtn = new Button
            {
                Text = "Abbrechen",
                Location = new Point(150, 310),
                Size = new Size(120, 30),
                BackColor = ColorTranslator.FromHtml("#7A5A3F"),
                ForeColor = Color.White
            };
            cancelBtn.Click += (btnSender, btnE) =>
            {
                squadErstellenPanel.Hide();
            };
            squadErstellenPanel.Controls.Add(cancelBtn);
            Controls.Add(squadErstellenPanel);
            squadErstellenPanel.BringToFront();
        }
        public void ErstelleSquadPanel()
        {
            squadPanel = new Panel
            {
                Size = new Size(400, 300),
                Location = new Point(750, 70),
                BackgroundImage = Properties.Resources.ui_wood,
                Visible = false
            };
            squadTruppenLB = new ListBox
            {
                Location = new Point(10, 10),
                Size = new Size(180, 200),
                BackColor = ColorTranslator.FromHtml("#8C5028"),
                ForeColor = Color.White
            };
            squadPanel.Controls.Add(squadTruppenLB);
            Button btnRemoveSelected = new Button
            {
                Text = "Truppe entfernen",
                Location = new Point(200, 10),
                Size = new Size(150, 30),
                BackColor = ColorTranslator.FromHtml("#7A5A3F"),
                ForeColor = Color.White
            };
            btnRemoveSelected.Click += (btnSender, btnE) =>
            {
                if (squadTruppenLB.SelectedItem == null)
                {
                    MessageBox.Show("Wähle eine Truppe aus");
                    return;
                }
                int index = squadTruppenLB.SelectedIndex;
                Truppe truppe = selectedSquad.Mitglieder[index];
                selectedSquad.EntferneTruppe(truppe);

                squadTruppenLB.Items.RemoveAt(index);
            };
            squadPanel.Controls.Add(btnRemoveSelected);
            Button closeBtn = new Button
            {
                Text = "Schließen",
                Location = new Point(200, 150),
                Size = new Size(150, 30),
                BackColor = ColorTranslator.FromHtml("#7A5A3F"),
                ForeColor = Color.White
            };
            closeBtn.Click += (btnSender, btnE) =>
            {
                squadPanel.Visible = false;
            };
            squadPanel.Controls.Add(closeBtn);
            Button kaufeNahkaempfer = new Button
            {
                Text = "Nahkämpfer Kaufen",
                Location = new Point(200, 50),
                Size = new Size(150, 30),
                BackColor = ColorTranslator.FromHtml("#7A5A3F"),
                ForeColor = Color.White
            };
            kaufeNahkaempfer.Click += (btnSender, btnE) =>
            {
                if (!selectedSquad.AktuellesFeld.GehoertZuStadt)
                {
                    MessageBox.Show("Musst in deiner Stadt sein, um Truppen kaufen zu können");
                    return;
                }
                Nahkaempfer nahkaempfer = new Nahkaempfer();
                nahkaempfer.Besitzer = aktuellerSpieler;
                if (nahkaempfer.Preis > aktuellerSpieler.geld)
                {
                    MessageBox.Show("Du hast nicht genügend Geld!");
                }
                else
                {
                    selectedSquad.TruppeHinzufuegen(nahkaempfer);
                    aktuellerSpieler.geld -= nahkaempfer.Preis;
                    squadTruppenLB.Items.Add(nahkaempfer);
                    UIAktualisierung();
                    UpdateUIInfo(selectedSquad);
                }
            };
            squadPanel.Controls.Add(kaufeNahkaempfer);
            Button kaufeFernkaempfer = new Button
            {
                Text = "Fernkämpfer Kaufen",
                Location = new Point(200, 80),
                Size = new Size(150, 30),
                BackColor = ColorTranslator.FromHtml("#7A5A3F"),
                ForeColor = Color.White
            };
            kaufeFernkaempfer.Click += (btnSender, btnE) =>
            {
                if (!selectedSquad.AktuellesFeld.GehoertZuStadt)
                {
                    MessageBox.Show("Musst in deiner Stadt sein, um Truppen kaufen zu können");
                    return;
                }
                Fernkaempfer fernkaempfer = new Fernkaempfer();
                fernkaempfer.Besitzer = aktuellerSpieler;
                if (fernkaempfer.Preis > aktuellerSpieler.geld)
                {
                    MessageBox.Show("Du hast nicht genügend Geld!");
                }
                else
                {
                    selectedSquad.TruppeHinzufuegen(fernkaempfer);
                    aktuellerSpieler.geld -= fernkaempfer.Preis;
                    squadTruppenLB.Items.Add(fernkaempfer);
                    UIAktualisierung();
                    UpdateUIInfo(selectedSquad);
                }
            };
            squadPanel.Controls.Add(kaufeFernkaempfer);
            Controls.Add(squadPanel);
        }

        private void squadPanelBtn_Click(object sender, EventArgs e)
        {
            squadPanel.Visible = true;
            squadTruppenLB.Items.Clear();
            foreach (Truppe truppe in selectedSquad.Mitglieder)
            {
                squadTruppenLB.Items.Add(truppe.ToString());
            }
            squadPanel.BringToFront();
        }

        private void rescourcenVerkauf_Click(object sender, EventArgs e)
        {
            if (Gewinnueberpruefung() == true) { return; }
            if (marktFenster.Visible == true)
            {
                marktFenster.Hide();
                eisenMarkt.Hide();
                kohleMarkt.Hide();
                stahlMarkt.Hide();
                weizenMarkt.Hide();
            }
            else
            {
                marktFenster.Show();
                eisenMarkt.Show(); eisenMarkt.BringToFront();
                kohleMarkt.Show(); kohleMarkt.BringToFront();
                stahlMarkt.Show(); stahlMarkt.BringToFront();
                weizenMarkt.Show(); weizenMarkt.BringToFront();
            }
        }

        private void eisenMarkt_Click(object sender, EventArgs e)
        {
            bool abziehbarRes = true;

            if (spieler[aktuellerSpielerIndex].rescourcenBesitz.Eisen >= 10) 
            {
                foreach (var feld in alleFelder)
                {
                    if (feld.besitzer == spieler[aktuellerSpielerIndex])
                    {
                        if (feld.rescourcen != null)
                        {
                            if (abziehbarRes == true)
                            {
                                feld.rescourcen.Eisen -= 10;
                                spieler[aktuellerSpielerIndex].geld += feld.rescourcen.wert * 3;
                                abziehbarRes = false;
                                UIAktualisierung();
                            }
                        }
                    }
                }
            }
            else 
            {
                MessageBox.Show("Nicht genügend Eisen zu Verkaufen!");
            }
        }

        private void kohleMarkt_Click(object sender, EventArgs e)
        {
            bool abziehbarRes = true;

            if (spieler[aktuellerSpielerIndex].rescourcenBesitz.Kohle >= 10)
            {
                foreach (var feld in alleFelder)
                {
                    if (feld.besitzer == spieler[aktuellerSpielerIndex])
                    {
                        if (feld.rescourcen != null)
                        {
                            if (abziehbarRes == true)
                            {
                                feld.rescourcen.Kohle -= 10;
                                spieler[aktuellerSpielerIndex].geld += feld.rescourcen.wert * 3;
                                abziehbarRes = false;
                                UIAktualisierung();
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Nicht genügend Kohle zu Verkaufen!");
            }
        }

        private void stahlMarkt_Click(object sender, EventArgs e)
        {
            bool abziehbarRes = true;

            if (spieler[aktuellerSpielerIndex].rescourcenBesitz.Stahl >= 10)
            {
                foreach (var feld in alleFelder)
                {
                    if (feld.besitzer == spieler[aktuellerSpielerIndex])
                    {
                        if (feld.rescourcen != null)
                        {
                            if (abziehbarRes == true)
                            {
                                feld.rescourcen.Stahl -= 10;
                                spieler[aktuellerSpielerIndex].geld += feld.rescourcen.wert * 3;
                                abziehbarRes = false;
                                UIAktualisierung();
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Nicht genügend Stahl zu Verkaufen!");
            }
        }

        private void weizenMarkt_Click(object sender, EventArgs e)
        {
            bool abziehbarRes = true;

            if (spieler[aktuellerSpielerIndex].rescourcenBesitz.Weizen >= 10)
            {
                foreach (var feld in alleFelder)
                {
                    if (feld.besitzer == spieler[aktuellerSpielerIndex])
                    {
                        if (feld.rescourcen != null)
                        {
                            if (abziehbarRes == true)
                            {
                                feld.rescourcen.Weizen -= 10;
                                spieler[aktuellerSpielerIndex].geld += feld.rescourcen.wert * 3;
                                abziehbarRes = false;
                                UIAktualisierung();
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Nicht genügend Weizen zu Verkaufen!");
            }
        }

        public void SpielstandUpdate() 
        {
            foreach (var spielerStaedte in aktuellerSpieler.staedteBesitz)
            {

                if (spielerStaedte.Leben <= 0)
                {
                    spielerStaedte.EntferneStadt();
                    spielerStaedte.Besitzer = null;
                    aktuellerSpieler.staedteBesitz.Remove(spielerStaedte);
                    EntferneBewegungsbereich(null);
                    SpielstandUpdate();
                    return;
                }
                else
                {
                    spielerStaedte.Leben -= 50;
                }
            }
        }

        private void recruitSquad_MouseEnter(object sender, EventArgs e)
        {
            recruitSquad.Image = Properties.Resources.recruit_squadglow;
        }

        private void recruitSquad_MouseLeave(object sender, EventArgs e)
        {
            recruitSquad.Image = Properties.Resources.recruit_squad;
        }

        private void rescourcenFenster_MouseEnter(object sender, EventArgs e)
        {
            rescourcenFenster.BackgroundImage = Properties.Resources.rescourceviewglow;
        }

        private void rescourcenFenster_MouseLeave(object sender, EventArgs e)
        {
            rescourcenFenster.BackgroundImage = Properties.Resources.rescourceview;
        }

        private void rescourcenVerkauf_MouseEnter(object sender, EventArgs e)
        {
            rescourcenVerkauf.BackgroundImage = Properties.Resources.rescourcesellglow;
        }

        private void rescourcenVerkauf_MouseLeave(object sender, EventArgs e)
        {
            rescourcenVerkauf.BackgroundImage = Properties.Resources.rescourcesell;
        }
    }
}
    
    


