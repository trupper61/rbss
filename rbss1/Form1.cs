﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
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
        private Squad selectedSquad = null;
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
                if (rekrutiermodus == true)
                {
                    return;
                }
                if (selectedTruppe != null && selectedTruppe != clickedTruppe)
                {
                    EntferneBewegungsbereich(null);
                    selectedTruppe.Angreifen(clickedTruppe);
                    ZeigeSchaden(selectedTruppe.textur, selectedTruppe.Schaden);
                    selectedTruppe = null;
                    HideUIInfo();
                    return;
                }
                if (selectedSquad != null)
                {
                    EntferneBewegungsbereich(null);
                    selectedSquad.Angreifen(clickedTruppe);
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

                if (selectedSquad != null && selectedSquad != clickedSquad)
                {
                    EntferneBewegungsbereich(null);
                    selectedSquad.Angreifen(clickedSquad);
                    ZeigeSchaden(selectedSquad.textur, selectedSquad.Gesamtschaden);
                    selectedSquad = null;
                    HideUIInfo();
                    return;
                }

                if (selectedTruppe != null)
                {
                    EntferneBewegungsbereich(null);
                    selectedTruppe.Angreifen(clickedSquad);
                    ZeigeSchaden(selectedSquad.textur, selectedSquad.Gesamtschaden);
                    selectedTruppe = null;
                    HideUIInfo();
                    return;
                }
                if (clickedSquad.Besitzer != aktuellerSpieler)
                {
                    MessageBox.Show("Das ist nicht dein Squad!");
                    return;
                }
                selectedSquad = clickedSquad;
                MakiereBewegungsreichweite(selectedSquad);
                UpdateUIInfo(clickedSquad);
                einnehmen.Show();
                return;
            }

            else if (clickedObject is Stadt clickedStadt)
            {
                selectedStadt = clickedStadt;
                if (clickedStadt != null)
                {
                    if (clickedStadt.Besitzer == aktuellerSpieler)
                    {
                        UpdateUIInfo(clickedStadt);

                        selectedTruppe = null;
                        EntferneBewegungsbereich(null);
                        return;
                    }
                    if (selectedTruppe != null)
                    {
                        EntferneBewegungsbereich(null);
                        selectedTruppe.Angreifen(clickedStadt);
                        ZeigeSchaden(selectedTruppe.textur, selectedTruppe.Schaden);
                        selectedTruppe = null;
                        HideUIInfo();
                        return;
                    }
                    else if (selectedSquad != null)
                    {
                        selectedSquad.Angreifen(clickedStadt);
                        ZeigeSchaden(selectedSquad.textur, selectedSquad.Gesamtschaden);
                        selectedSquad = null;
                        HideUIInfo();
                        EntferneBewegungsbereich(null);
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
                    if (clickedFeld.GehoertZuStadt && clickedFeld.besitzer != null)
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
                    if (lastClickedFeld.GehoertZuStadt && lastClickedFeld.besitzer != null)
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

                lastClickedFeld = clickedFeld;



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
                        selectedTruppe = null;
                        EntferneBewegungsbereich(null);
                    }
                    UpdateGame(selectedTruppe);
                }
                else if (selectedSquad != null && clickedFeld.feldart == "Grass")
                {
                    if (selectedSquad.BerechneDistanz(clickedFeld) <= selectedSquad.Bewegungsreichweite)
                    {
                        selectedSquad.BewegeZu(clickedFeld);
                        EntferneBewegungsbereich(null);
                        selectedSquad = null;
                        UIAktualisierung();
                        einnehmen.Hide();
                    }
                }
                lastClickedFeld = clickedFeld;

            }
            if (rekrutiermodus == true)
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
            }

        }

        public void Spielerwechsel()
        {
            aktuellerSpielerIndex++;

            if (aktuellerSpielerIndex >= spieler.Count)
            {
                if (Gewinnueberpruefung() == true)
                {
                    return;
                }
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
                }

                foreach (var feld in alleFelder)
                {

                    if (feld.FarmAufFeld != null)
                    {
                        feld.rescourcen.Weizen += feld.FarmAufFeld.weizenEinkommen;
                    }
                }
                foreach (var spieler in spieler)
                {
                    int abziehbarVar = spieler.staedteBesitz.Count;
                    foreach (var feld in alleFelder)
                    {
                        if (feld.besitzer == spieler)
                        {

                            if (feld.rescourcen != null)
                            {
                                if (abziehbarVar != 0)
                                {
                                    feld.rescourcen.Weizen -= 10;
                                    abziehbarVar--;
                                }
                            }
                        }
                    }
                }
            }

            aktuellerSpieler = spieler[aktuellerSpielerIndex];
            MessageBox.Show($"Spieler {aktuellerSpieler.spielernummer} ist dran");

            if (aktuellerSpieler.rescourcenBesitz.Weizen < 0)
            {
                MessageBox.Show("Rescourcendefizit! Sorge für Weizenproduktion, oder deine Zivilisation stirbt aus!");
            }
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
            if (lastClickedFeld != null && !lastClickedFeld.GehoertZuStadt && lastClickedFeld.besitzer != spieler[aktuellerSpielerIndex])
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
            }
            else if (stadtbauen.Visible == true)
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

            geldanzeige.Text = spieler[aktuellerSpielerIndex].geld.ToString();
            bewpunktanzeige.Text = spieler[aktuellerSpielerIndex].bewegungspunkte.ToString();
            momentanerSpieler.Text = $"Spieler {aktuellerSpielerIndex + 1}";

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
                if (Spieler.staedteBesitz.Count == 0)
                {
                    MessageBox.Show("Spieler ist Raus!");
                    spieler.Remove(Spieler);
                }
                if (spieler.Count == 1)
                {
                    MessageBox.Show($"Spieler {Spieler.ToString()} hat gewonnen!");
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
            Panel squadErstellenPanel = new Panel
            {
                Size = new Size(300, 400),
                Location = new Point(500, 50),
                BorderStyle = BorderStyle.FixedSingle
            };
            Label infoLabel = new Label
            {
                Text = "Wähle Truppen:",
                Location = new Point(10, 10),
                Size = new Size(280, 20)
            };
            squadErstellenPanel.Controls.Add(infoLabel);
            ListBox truppenAnzeige = new ListBox
            {
                Location = new Point(10, 40),
                Size = new Size(280, 200)
            };
            squadErstellenPanel.Controls.Add(truppenAnzeige);

            Button nahkaempferButton = new Button
            {
                Text = "Nahkämpfer + ",
                Location = new Point(10, 260),
                Size = new Size(120, 30)
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
                Size = new Size(120, 30)
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
                Size = new Size(120, 30)
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
                if (freiesFeld == null)
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
                aktuellerSpieler.geld -= preis;
                neuesSquad.textur.Tag = neuesSquad;
                squadErstellenPanel.Hide();
            };
            squadErstellenPanel.Controls.Add(confirmBtn);

            Button cancelBtn = new Button
            {
                Text = "Abbrechen",
                Location = new Point(150, 310),
                Size = new Size(120, 30)
            };
            cancelBtn.Click += (btnSender, btnE) =>
            {
                squadErstellenPanel.Hide();
            };
            squadErstellenPanel.Controls.Add(cancelBtn);
            Controls.Add(squadErstellenPanel);
            squadErstellenPanel.BringToFront();
        }
    }
}
    
    


