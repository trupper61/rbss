namespace rbss1
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.truppeComboBox = new System.Windows.Forms.ComboBox();
            this.rescourcenVerkauf = new System.Windows.Forms.PictureBox();
            this.squadPanelBtn = new System.Windows.Forms.PictureBox();
            this.stahlwerkbauen = new System.Windows.Forms.Button();
            this.recruitSquad = new System.Windows.Forms.PictureBox();
            this.farmbauen = new System.Windows.Forms.Button();
            this.wheatAnzahl = new System.Windows.Forms.Label();
            this.steelAnzahl = new System.Windows.Forms.Label();
            this.wheatInventory = new System.Windows.Forms.PictureBox();
            this.steelInventory = new System.Windows.Forms.PictureBox();
            this.coalAnzahl = new System.Windows.Forms.Label();
            this.coalInventory = new System.Windows.Forms.PictureBox();
            this.eisenAnzahl = new System.Windows.Forms.Label();
            this.eisenInventory = new System.Windows.Forms.PictureBox();
            this.rescourcenlabel = new System.Windows.Forms.Label();
            this.rescourceinventory = new System.Windows.Forms.PictureBox();
            this.rescourcenFenster = new System.Windows.Forms.PictureBox();
            this.momentanerSpieler = new System.Windows.Forms.Label();
            this.bewpunktanzeige = new System.Windows.Forms.Label();
            this.geldanzeige = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.recruitSoldiers = new System.Windows.Forms.PictureBox();
            this.stadtbauen = new System.Windows.Forms.Button();
            this.einnehmen = new System.Windows.Forms.Button();
            this.construction = new System.Windows.Forms.PictureBox();
            this.weiter = new System.Windows.Forms.Button();
            this.titelLabel = new System.Windows.Forms.Label();
            this.ItemPB = new System.Windows.Forms.PictureBox();
            this.truppenLebenLB = new System.Windows.Forms.Label();
            this.truppenSchadenLB = new System.Windows.Forms.Label();
            this.anzahlRes = new System.Windows.Forms.Label();
            this.UIInfo = new System.Windows.Forms.PictureBox();
            this.UI = new System.Windows.Forms.PictureBox();
            this.marktFenster = new System.Windows.Forms.PictureBox();
            this.eisenMarkt = new System.Windows.Forms.PictureBox();
            this.kohleMarkt = new System.Windows.Forms.PictureBox();
            this.stahlMarkt = new System.Windows.Forms.PictureBox();
            this.weizenMarkt = new System.Windows.Forms.PictureBox();
            this.aktuellerSpielerFarbe = new System.Windows.Forms.PictureBox();
            this.farbedesSpielers = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.rescourcenVerkauf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.squadPanelBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recruitSquad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wheatInventory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.steelInventory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.coalInventory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eisenInventory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rescourceinventory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rescourcenFenster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recruitSoldiers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.construction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UIInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.marktFenster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eisenMarkt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kohleMarkt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stahlMarkt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.weizenMarkt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aktuellerSpielerFarbe)).BeginInit();
            this.SuspendLayout();
            // 
            // truppeComboBox
            // 
            this.truppeComboBox.FormattingEnabled = true;
            this.truppeComboBox.Location = new System.Drawing.Point(937, 50);
            this.truppeComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.truppeComboBox.Name = "truppeComboBox";
            this.truppeComboBox.Size = new System.Drawing.Size(92, 21);
            this.truppeComboBox.TabIndex = 11;
            this.truppeComboBox.Visible = false;
            // 
            // rescourcenVerkauf
            // 
            this.rescourcenVerkauf.BackgroundImage = global::rbss1.Properties.Resources.rescourcesell;
            this.rescourcenVerkauf.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rescourcenVerkauf.Location = new System.Drawing.Point(1042, 302);
            this.rescourcenVerkauf.Margin = new System.Windows.Forms.Padding(2);
            this.rescourcenVerkauf.Name = "rescourcenVerkauf";
            this.rescourcenVerkauf.Size = new System.Drawing.Size(50, 50);
            this.rescourcenVerkauf.TabIndex = 33;
            this.rescourcenVerkauf.TabStop = false;
            this.rescourcenVerkauf.Click += new System.EventHandler(this.rescourcenVerkauf_Click);
            this.rescourcenVerkauf.MouseEnter += new System.EventHandler(this.rescourcenVerkauf_MouseEnter);
            this.rescourcenVerkauf.MouseLeave += new System.EventHandler(this.rescourcenVerkauf_MouseLeave);
            // 
            // squadPanelBtn
            // 
            this.squadPanelBtn.Image = global::rbss1.Properties.Resources.settings;
            this.squadPanelBtn.Location = new System.Drawing.Point(986, 193);
            this.squadPanelBtn.Margin = new System.Windows.Forms.Padding(2);
            this.squadPanelBtn.Name = "squadPanelBtn";
            this.squadPanelBtn.Size = new System.Drawing.Size(50, 50);
            this.squadPanelBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.squadPanelBtn.TabIndex = 32;
            this.squadPanelBtn.TabStop = false;
            this.squadPanelBtn.Visible = false;
            this.squadPanelBtn.Click += new System.EventHandler(this.squadPanelBtn_Click);
            // 
            // stahlwerkbauen
            // 
            this.stahlwerkbauen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.stahlwerkbauen.Image = global::rbss1.Properties.Resources.labelbackround;
            this.stahlwerkbauen.Location = new System.Drawing.Point(718, 50);
            this.stahlwerkbauen.Margin = new System.Windows.Forms.Padding(2);
            this.stahlwerkbauen.Name = "stahlwerkbauen";
            this.stahlwerkbauen.Size = new System.Drawing.Size(68, 19);
            this.stahlwerkbauen.TabIndex = 30;
            this.stahlwerkbauen.Text = "Stahlwerk";
            this.stahlwerkbauen.UseVisualStyleBackColor = true;
            this.stahlwerkbauen.Visible = false;
            this.stahlwerkbauen.Click += new System.EventHandler(this.stahlwerkbauen_Click);
            // 
            // recruitSquad
            // 
            this.recruitSquad.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("recruitSquad.BackgroundImage")));
            this.recruitSquad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.recruitSquad.Image = global::rbss1.Properties.Resources.recruit_squad;
            this.recruitSquad.Location = new System.Drawing.Point(1042, 193);
            this.recruitSquad.Name = "recruitSquad";
            this.recruitSquad.Size = new System.Drawing.Size(50, 50);
            this.recruitSquad.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.recruitSquad.TabIndex = 29;
            this.recruitSquad.TabStop = false;
            this.recruitSquad.Click += new System.EventHandler(this.recruitSquad_Click);
            this.recruitSquad.MouseEnter += new System.EventHandler(this.recruitSquad_MouseEnter);
            this.recruitSquad.MouseLeave += new System.EventHandler(this.recruitSquad_MouseLeave);
            // 
            // farmbauen
            // 
            this.farmbauen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.farmbauen.Image = global::rbss1.Properties.Resources.labelbackround;
            this.farmbauen.Location = new System.Drawing.Point(790, 50);
            this.farmbauen.Margin = new System.Windows.Forms.Padding(2);
            this.farmbauen.Name = "farmbauen";
            this.farmbauen.Size = new System.Drawing.Size(68, 19);
            this.farmbauen.TabIndex = 26;
            this.farmbauen.Text = "Farm Errichten";
            this.farmbauen.UseVisualStyleBackColor = true;
            this.farmbauen.Visible = false;
            this.farmbauen.Click += new System.EventHandler(this.farmbauen_Click);
            // 
            // wheatAnzahl
            // 
            this.wheatAnzahl.AutoSize = true;
            this.wheatAnzahl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.wheatAnzahl.Image = global::rbss1.Properties.Resources.labelbackround;
            this.wheatAnzahl.Location = new System.Drawing.Point(610, 330);
            this.wheatAnzahl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.wheatAnzahl.Name = "wheatAnzahl";
            this.wheatAnzahl.Size = new System.Drawing.Size(70, 15);
            this.wheatAnzahl.TabIndex = 25;
            this.wheatAnzahl.Text = "Rescourcen:";
            this.wheatAnzahl.Visible = false;
            // 
            // steelAnzahl
            // 
            this.steelAnzahl.AutoSize = true;
            this.steelAnzahl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.steelAnzahl.Image = global::rbss1.Properties.Resources.labelbackround;
            this.steelAnzahl.Location = new System.Drawing.Point(609, 294);
            this.steelAnzahl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.steelAnzahl.Name = "steelAnzahl";
            this.steelAnzahl.Size = new System.Drawing.Size(70, 15);
            this.steelAnzahl.TabIndex = 24;
            this.steelAnzahl.Text = "Rescourcen:";
            this.steelAnzahl.Visible = false;
            // 
            // wheatInventory
            // 
            this.wheatInventory.BackgroundImage = global::rbss1.Properties.Resources.wheatinventory;
            this.wheatInventory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.wheatInventory.Location = new System.Drawing.Point(574, 330);
            this.wheatInventory.Margin = new System.Windows.Forms.Padding(2);
            this.wheatInventory.Name = "wheatInventory";
            this.wheatInventory.Size = new System.Drawing.Size(32, 32);
            this.wheatInventory.TabIndex = 23;
            this.wheatInventory.TabStop = false;
            this.wheatInventory.Visible = false;
            // 
            // steelInventory
            // 
            this.steelInventory.BackgroundImage = global::rbss1.Properties.Resources.stahlinventory1;
            this.steelInventory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.steelInventory.Location = new System.Drawing.Point(574, 294);
            this.steelInventory.Margin = new System.Windows.Forms.Padding(2);
            this.steelInventory.Name = "steelInventory";
            this.steelInventory.Size = new System.Drawing.Size(32, 32);
            this.steelInventory.TabIndex = 22;
            this.steelInventory.TabStop = false;
            this.steelInventory.Visible = false;
            // 
            // coalAnzahl
            // 
            this.coalAnzahl.AutoSize = true;
            this.coalAnzahl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.coalAnzahl.Image = global::rbss1.Properties.Resources.labelbackround;
            this.coalAnzahl.Location = new System.Drawing.Point(610, 257);
            this.coalAnzahl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.coalAnzahl.Name = "coalAnzahl";
            this.coalAnzahl.Size = new System.Drawing.Size(70, 15);
            this.coalAnzahl.TabIndex = 21;
            this.coalAnzahl.Text = "Rescourcen:";
            this.coalAnzahl.Visible = false;
            // 
            // coalInventory
            // 
            this.coalInventory.BackgroundImage = global::rbss1.Properties.Resources.coalinventory1;
            this.coalInventory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.coalInventory.Location = new System.Drawing.Point(574, 257);
            this.coalInventory.Margin = new System.Windows.Forms.Padding(2);
            this.coalInventory.Name = "coalInventory";
            this.coalInventory.Size = new System.Drawing.Size(32, 32);
            this.coalInventory.TabIndex = 20;
            this.coalInventory.TabStop = false;
            this.coalInventory.Visible = false;
            // 
            // eisenAnzahl
            // 
            this.eisenAnzahl.AutoSize = true;
            this.eisenAnzahl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.eisenAnzahl.Image = global::rbss1.Properties.Resources.labelbackround;
            this.eisenAnzahl.Location = new System.Drawing.Point(609, 221);
            this.eisenAnzahl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.eisenAnzahl.Name = "eisenAnzahl";
            this.eisenAnzahl.Size = new System.Drawing.Size(70, 15);
            this.eisenAnzahl.TabIndex = 19;
            this.eisenAnzahl.Text = "Rescourcen:";
            this.eisenAnzahl.Visible = false;
            // 
            // eisenInventory
            // 
            this.eisenInventory.BackgroundImage = global::rbss1.Properties.Resources.ironinventory;
            this.eisenInventory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.eisenInventory.Location = new System.Drawing.Point(574, 221);
            this.eisenInventory.Margin = new System.Windows.Forms.Padding(2);
            this.eisenInventory.Name = "eisenInventory";
            this.eisenInventory.Size = new System.Drawing.Size(32, 32);
            this.eisenInventory.TabIndex = 18;
            this.eisenInventory.TabStop = false;
            this.eisenInventory.Visible = false;
            // 
            // rescourcenlabel
            // 
            this.rescourcenlabel.AutoSize = true;
            this.rescourcenlabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rescourcenlabel.Image = global::rbss1.Properties.Resources.labelbackround;
            this.rescourcenlabel.Location = new System.Drawing.Point(574, 195);
            this.rescourcenlabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.rescourcenlabel.Name = "rescourcenlabel";
            this.rescourcenlabel.Size = new System.Drawing.Size(70, 15);
            this.rescourcenlabel.TabIndex = 17;
            this.rescourcenlabel.Text = "Rescourcen:";
            this.rescourcenlabel.Visible = false;
            // 
            // rescourceinventory
            // 
            this.rescourceinventory.BackgroundImage = global::rbss1.Properties.Resources.labelbackround;
            this.rescourceinventory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.rescourceinventory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rescourceinventory.Location = new System.Drawing.Point(549, 184);
            this.rescourceinventory.Margin = new System.Windows.Forms.Padding(2);
            this.rescourceinventory.Name = "rescourceinventory";
            this.rescourceinventory.Size = new System.Drawing.Size(125, 216);
            this.rescourceinventory.TabIndex = 16;
            this.rescourceinventory.TabStop = false;
            this.rescourceinventory.Visible = false;
            // 
            // rescourcenFenster
            // 
            this.rescourcenFenster.BackgroundImage = global::rbss1.Properties.Resources.rescourceview;
            this.rescourcenFenster.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rescourcenFenster.Location = new System.Drawing.Point(1042, 248);
            this.rescourcenFenster.Margin = new System.Windows.Forms.Padding(2);
            this.rescourcenFenster.Name = "rescourcenFenster";
            this.rescourcenFenster.Size = new System.Drawing.Size(50, 50);
            this.rescourcenFenster.TabIndex = 15;
            this.rescourcenFenster.TabStop = false;
            this.rescourcenFenster.Click += new System.EventHandler(this.rescourcenFenster_Click);
            this.rescourcenFenster.MouseEnter += new System.EventHandler(this.rescourcenFenster_MouseEnter);
            this.rescourcenFenster.MouseLeave += new System.EventHandler(this.rescourcenFenster_MouseLeave);
            // 
            // momentanerSpieler
            // 
            this.momentanerSpieler.AutoSize = true;
            this.momentanerSpieler.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.momentanerSpieler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.momentanerSpieler.Image = global::rbss1.Properties.Resources.labelbackround;
            this.momentanerSpieler.Location = new System.Drawing.Point(616, 126);
            this.momentanerSpieler.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.momentanerSpieler.Name = "momentanerSpieler";
            this.momentanerSpieler.Size = new System.Drawing.Size(103, 15);
            this.momentanerSpieler.TabIndex = 14;
            this.momentanerSpieler.Text = "Momentaner Spieler";
            // 
            // bewpunktanzeige
            // 
            this.bewpunktanzeige.AutoSize = true;
            this.bewpunktanzeige.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bewpunktanzeige.Image = global::rbss1.Properties.Resources.labelbackround;
            this.bewpunktanzeige.Location = new System.Drawing.Point(627, 88);
            this.bewpunktanzeige.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.bewpunktanzeige.Name = "bewpunktanzeige";
            this.bewpunktanzeige.Size = new System.Drawing.Size(57, 15);
            this.bewpunktanzeige.TabIndex = 13;
            this.bewpunktanzeige.Text = "movpoints";
            // 
            // geldanzeige
            // 
            this.geldanzeige.AutoSize = true;
            this.geldanzeige.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.geldanzeige.Image = global::rbss1.Properties.Resources.labelbackround;
            this.geldanzeige.Location = new System.Drawing.Point(627, 41);
            this.geldanzeige.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.geldanzeige.Name = "geldanzeige";
            this.geldanzeige.Size = new System.Drawing.Size(40, 15);
            this.geldanzeige.TabIndex = 12;
            this.geldanzeige.Text = "money";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::rbss1.Properties.Resources.data;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(549, -5);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(124, 198);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // recruitSoldiers
            // 
            this.recruitSoldiers.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("recruitSoldiers.BackgroundImage")));
            this.recruitSoldiers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.recruitSoldiers.Location = new System.Drawing.Point(1042, 136);
            this.recruitSoldiers.Name = "recruitSoldiers";
            this.recruitSoldiers.Size = new System.Drawing.Size(50, 50);
            this.recruitSoldiers.TabIndex = 10;
            this.recruitSoldiers.TabStop = false;
            this.recruitSoldiers.Click += new System.EventHandler(this.recruitSoldiers_Click);
            this.recruitSoldiers.MouseEnter += new System.EventHandler(this.recruitSoldiers_MouseEnter);
            this.recruitSoldiers.MouseLeave += new System.EventHandler(this.recruitSoldiers_MouseLeave);
            // 
            // stadtbauen
            // 
            this.stadtbauen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.stadtbauen.Image = global::rbss1.Properties.Resources.labelbackround;
            this.stadtbauen.Location = new System.Drawing.Point(863, 50);
            this.stadtbauen.Name = "stadtbauen";
            this.stadtbauen.Size = new System.Drawing.Size(68, 19);
            this.stadtbauen.TabIndex = 9;
            this.stadtbauen.Text = "Stadt Errichten";
            this.stadtbauen.UseVisualStyleBackColor = true;
            this.stadtbauen.Visible = false;
            this.stadtbauen.Click += new System.EventHandler(this.stadtbauen_Click);
            // 
            // einnehmen
            // 
            this.einnehmen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.einnehmen.Image = global::rbss1.Properties.Resources.labelbackround;
            this.einnehmen.Location = new System.Drawing.Point(728, 270);
            this.einnehmen.Name = "einnehmen";
            this.einnehmen.Size = new System.Drawing.Size(128, 19);
            this.einnehmen.TabIndex = 8;
            this.einnehmen.Text = "Feld Einnehmen";
            this.einnehmen.UseVisualStyleBackColor = true;
            this.einnehmen.Visible = false;
            this.einnehmen.Click += new System.EventHandler(this.einnehmen_Click);
            // 
            // construction
            // 
            this.construction.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("construction.BackgroundImage")));
            this.construction.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.construction.Location = new System.Drawing.Point(1042, 81);
            this.construction.Name = "construction";
            this.construction.Size = new System.Drawing.Size(50, 50);
            this.construction.TabIndex = 7;
            this.construction.TabStop = false;
            this.construction.Click += new System.EventHandler(this.construction_Click);
            this.construction.MouseEnter += new System.EventHandler(this.construction_MouseEnter);
            this.construction.MouseLeave += new System.EventHandler(this.construction_MouseLeave);
            // 
            // weiter
            // 
            this.weiter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.weiter.Image = global::rbss1.Properties.Resources.labelbackround;
            this.weiter.Location = new System.Drawing.Point(688, 8);
            this.weiter.Name = "weiter";
            this.weiter.Size = new System.Drawing.Size(76, 19);
            this.weiter.TabIndex = 5;
            this.weiter.Text = "Nächste Runde";
            this.weiter.UseVisualStyleBackColor = true;
            this.weiter.Click += new System.EventHandler(this.weiter_Click);
            // 
            // titelLabel
            // 
            this.titelLabel.AutoSize = true;
            this.titelLabel.BackColor = System.Drawing.Color.Transparent;
            this.titelLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.titelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titelLabel.Image = global::rbss1.Properties.Resources.labelbackround;
            this.titelLabel.Location = new System.Drawing.Point(868, 81);
            this.titelLabel.Name = "titelLabel";
            this.titelLabel.Size = new System.Drawing.Size(37, 19);
            this.titelLabel.TabIndex = 6;
            this.titelLabel.Text = "Titel";
            this.titelLabel.Visible = false;
            // 
            // ItemPB
            // 
            this.ItemPB.BackgroundImage = global::rbss1.Properties.Resources.ui_wood;
            this.ItemPB.Image = global::rbss1.Properties.Resources.squad_wappen;
            this.ItemPB.Location = new System.Drawing.Point(736, 102);
            this.ItemPB.Name = "ItemPB";
            this.ItemPB.Size = new System.Drawing.Size(127, 161);
            this.ItemPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ItemPB.TabIndex = 5;
            this.ItemPB.TabStop = false;
            this.ItemPB.Visible = false;
            // 
            // truppenLebenLB
            // 
            this.truppenLebenLB.AutoSize = true;
            this.truppenLebenLB.BackColor = System.Drawing.Color.Transparent;
            this.truppenLebenLB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.truppenLebenLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.truppenLebenLB.Image = global::rbss1.Properties.Resources.labelbackround;
            this.truppenLebenLB.Location = new System.Drawing.Point(868, 126);
            this.truppenLebenLB.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.truppenLebenLB.Name = "truppenLebenLB";
            this.truppenLebenLB.Size = new System.Drawing.Size(66, 22);
            this.truppenLebenLB.TabIndex = 3;
            this.truppenLebenLB.Text = "Leben:";
            this.truppenLebenLB.Visible = false;
            // 
            // truppenSchadenLB
            // 
            this.truppenSchadenLB.AutoSize = true;
            this.truppenSchadenLB.BackColor = System.Drawing.Color.Transparent;
            this.truppenSchadenLB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.truppenSchadenLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.truppenSchadenLB.Image = global::rbss1.Properties.Resources.labelbackround;
            this.truppenSchadenLB.Location = new System.Drawing.Point(868, 171);
            this.truppenSchadenLB.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.truppenSchadenLB.Name = "truppenSchadenLB";
            this.truppenSchadenLB.Size = new System.Drawing.Size(87, 22);
            this.truppenSchadenLB.TabIndex = 4;
            this.truppenSchadenLB.Text = "Schaden:";
            this.truppenSchadenLB.Visible = false;
            // 
            // anzahlRes
            // 
            this.anzahlRes.AutoSize = true;
            this.anzahlRes.BackColor = System.Drawing.Color.Transparent;
            this.anzahlRes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.anzahlRes.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.anzahlRes.Image = global::rbss1.Properties.Resources.labelbackround;
            this.anzahlRes.Location = new System.Drawing.Point(809, 344);
            this.anzahlRes.Name = "anzahlRes";
            this.anzahlRes.Size = new System.Drawing.Size(45, 15);
            this.anzahlRes.TabIndex = 2;
            this.anzahlRes.Text = "Anzahl: ";
            this.anzahlRes.Visible = false;
            // 
            // UIInfo
            // 
            this.UIInfo.BackColor = System.Drawing.Color.Transparent;
            this.UIInfo.Image = global::rbss1.Properties.Resources.UI2eisen;
            this.UIInfo.Location = new System.Drawing.Point(790, 405);
            this.UIInfo.Margin = new System.Windows.Forms.Padding(4);
            this.UIInfo.Name = "UIInfo";
            this.UIInfo.Size = new System.Drawing.Size(288, 94);
            this.UIInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.UIInfo.TabIndex = 1;
            this.UIInfo.TabStop = false;
            this.UIInfo.Visible = false;
            // 
            // UI
            // 
            this.UI.BackColor = System.Drawing.Color.Transparent;
            this.UI.Image = global::rbss1.Properties.Resources.UI1;
            this.UI.Location = new System.Drawing.Point(670, -5);
            this.UI.Margin = new System.Windows.Forms.Padding(4);
            this.UI.Name = "UI";
            this.UI.Size = new System.Drawing.Size(435, 572);
            this.UI.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.UI.TabIndex = 0;
            this.UI.TabStop = false;
            // 
            // marktFenster
            // 
            this.marktFenster.BackgroundImage = global::rbss1.Properties.Resources.labelbackround;
            this.marktFenster.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.marktFenster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.marktFenster.Location = new System.Drawing.Point(763, 367);
            this.marktFenster.Margin = new System.Windows.Forms.Padding(2);
            this.marktFenster.Name = "marktFenster";
            this.marktFenster.Size = new System.Drawing.Size(342, 132);
            this.marktFenster.TabIndex = 34;
            this.marktFenster.TabStop = false;
            this.marktFenster.Visible = false;
            // 
            // eisenMarkt
            // 
            this.eisenMarkt.BackgroundImage = global::rbss1.Properties.Resources.ironinventory;
            this.eisenMarkt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.eisenMarkt.Location = new System.Drawing.Point(812, 430);
            this.eisenMarkt.Margin = new System.Windows.Forms.Padding(2);
            this.eisenMarkt.Name = "eisenMarkt";
            this.eisenMarkt.Size = new System.Drawing.Size(32, 32);
            this.eisenMarkt.TabIndex = 35;
            this.eisenMarkt.TabStop = false;
            this.eisenMarkt.Visible = false;
            this.eisenMarkt.Click += new System.EventHandler(this.eisenMarkt_Click);
            // 
            // kohleMarkt
            // 
            this.kohleMarkt.BackgroundImage = global::rbss1.Properties.Resources.coalinventory1;
            this.kohleMarkt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.kohleMarkt.Location = new System.Drawing.Point(873, 430);
            this.kohleMarkt.Margin = new System.Windows.Forms.Padding(2);
            this.kohleMarkt.Name = "kohleMarkt";
            this.kohleMarkt.Size = new System.Drawing.Size(32, 32);
            this.kohleMarkt.TabIndex = 36;
            this.kohleMarkt.TabStop = false;
            this.kohleMarkt.Visible = false;
            this.kohleMarkt.Click += new System.EventHandler(this.kohleMarkt_Click);
            // 
            // stahlMarkt
            // 
            this.stahlMarkt.BackgroundImage = global::rbss1.Properties.Resources.stahlinventory1;
            this.stahlMarkt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.stahlMarkt.Location = new System.Drawing.Point(946, 430);
            this.stahlMarkt.Margin = new System.Windows.Forms.Padding(2);
            this.stahlMarkt.Name = "stahlMarkt";
            this.stahlMarkt.Size = new System.Drawing.Size(32, 32);
            this.stahlMarkt.TabIndex = 37;
            this.stahlMarkt.TabStop = false;
            this.stahlMarkt.Visible = false;
            this.stahlMarkt.Click += new System.EventHandler(this.stahlMarkt_Click);
            // 
            // weizenMarkt
            // 
            this.weizenMarkt.BackgroundImage = global::rbss1.Properties.Resources.wheatinventory;
            this.weizenMarkt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.weizenMarkt.Location = new System.Drawing.Point(1019, 430);
            this.weizenMarkt.Margin = new System.Windows.Forms.Padding(2);
            this.weizenMarkt.Name = "weizenMarkt";
            this.weizenMarkt.Size = new System.Drawing.Size(32, 32);
            this.weizenMarkt.TabIndex = 38;
            this.weizenMarkt.TabStop = false;
            this.weizenMarkt.Visible = false;
            this.weizenMarkt.Click += new System.EventHandler(this.weizenMarkt_Click);
            // 
            // aktuellerSpielerFarbe
            // 
            this.aktuellerSpielerFarbe.BackColor = System.Drawing.Color.Transparent;
            this.aktuellerSpielerFarbe.Location = new System.Drawing.Point(549, 396);
            this.aktuellerSpielerFarbe.Name = "aktuellerSpielerFarbe";
            this.aktuellerSpielerFarbe.Size = new System.Drawing.Size(124, 113);
            this.aktuellerSpielerFarbe.TabIndex = 39;
            this.aktuellerSpielerFarbe.TabStop = false;
            // 
            // farbedesSpielers
            // 
            this.farbedesSpielers.AutoSize = true;
            this.farbedesSpielers.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.farbedesSpielers.Image = global::rbss1.Properties.Resources.labelbackround;
            this.farbedesSpielers.Location = new System.Drawing.Point(589, 405);
            this.farbedesSpielers.Name = "farbedesSpielers";
            this.farbedesSpielers.Size = new System.Drawing.Size(68, 13);
            this.farbedesSpielers.TabIndex = 40;
            this.farbedesSpielers.Text = "Deine Farbe:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImage = global::rbss1.Properties.Resources.labelbackround;
            this.ClientSize = new System.Drawing.Size(1103, 501);
            this.Controls.Add(this.farbedesSpielers);
            this.Controls.Add(this.aktuellerSpielerFarbe);
            this.Controls.Add(this.weizenMarkt);
            this.Controls.Add(this.stahlMarkt);
            this.Controls.Add(this.kohleMarkt);
            this.Controls.Add(this.eisenMarkt);
            this.Controls.Add(this.marktFenster);
            this.Controls.Add(this.rescourcenVerkauf);
            this.Controls.Add(this.squadPanelBtn);
            this.Controls.Add(this.stahlwerkbauen);
            this.Controls.Add(this.recruitSquad);
            this.Controls.Add(this.farmbauen);
            this.Controls.Add(this.wheatAnzahl);
            this.Controls.Add(this.steelAnzahl);
            this.Controls.Add(this.wheatInventory);
            this.Controls.Add(this.steelInventory);
            this.Controls.Add(this.coalAnzahl);
            this.Controls.Add(this.coalInventory);
            this.Controls.Add(this.eisenAnzahl);
            this.Controls.Add(this.eisenInventory);
            this.Controls.Add(this.rescourcenlabel);
            this.Controls.Add(this.rescourceinventory);
            this.Controls.Add(this.rescourcenFenster);
            this.Controls.Add(this.momentanerSpieler);
            this.Controls.Add(this.bewpunktanzeige);
            this.Controls.Add(this.geldanzeige);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.truppeComboBox);
            this.Controls.Add(this.recruitSoldiers);
            this.Controls.Add(this.stadtbauen);
            this.Controls.Add(this.einnehmen);
            this.Controls.Add(this.construction);
            this.Controls.Add(this.weiter);
            this.Controls.Add(this.titelLabel);
            this.Controls.Add(this.ItemPB);
            this.Controls.Add(this.truppenLebenLB);
            this.Controls.Add(this.truppenSchadenLB);
            this.Controls.Add(this.anzahlRes);
            this.Controls.Add(this.UIInfo);
            this.Controls.Add(this.UI);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.rescourcenVerkauf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.squadPanelBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recruitSquad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wheatInventory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.steelInventory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.coalInventory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eisenInventory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rescourceinventory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rescourcenFenster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recruitSoldiers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.construction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UIInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.marktFenster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eisenMarkt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kohleMarkt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stahlMarkt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.weizenMarkt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aktuellerSpielerFarbe)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox UI;
        private System.Windows.Forms.PictureBox UIInfo;
        private System.Windows.Forms.Label anzahlRes;
        private System.Windows.Forms.Label truppenLebenLB;
        private System.Windows.Forms.Label truppenSchadenLB;
        private System.Windows.Forms.Button weiter;
        private System.Windows.Forms.PictureBox ItemPB;
        private System.Windows.Forms.Label titelLabel;
        private System.Windows.Forms.PictureBox construction;
        private System.Windows.Forms.Button einnehmen;
        private System.Windows.Forms.Button stadtbauen;
        private System.Windows.Forms.PictureBox recruitSoldiers;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label geldanzeige;
        private System.Windows.Forms.Label bewpunktanzeige;
        private System.Windows.Forms.Label momentanerSpieler;
        private System.Windows.Forms.PictureBox rescourceinventory;
        private System.Windows.Forms.Label rescourcenlabel;
        private System.Windows.Forms.PictureBox eisenInventory;
        private System.Windows.Forms.Label eisenAnzahl;
        private System.Windows.Forms.PictureBox coalInventory;
        private System.Windows.Forms.Label coalAnzahl;
        private System.Windows.Forms.PictureBox steelInventory;
        private System.Windows.Forms.PictureBox wheatInventory;
        private System.Windows.Forms.Label steelAnzahl;
        private System.Windows.Forms.Label wheatAnzahl;
        private System.Windows.Forms.Button farmbauen;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ComboBox truppeComboBox;
        private System.Windows.Forms.PictureBox recruitSquad;
        private System.Windows.Forms.PictureBox rescourcenFenster;
        private System.Windows.Forms.Button stahlwerkbauen;
        private System.Windows.Forms.PictureBox squadPanelBtn;
        private System.Windows.Forms.PictureBox rescourcenVerkauf;
        private System.Windows.Forms.PictureBox marktFenster;
        private System.Windows.Forms.PictureBox eisenMarkt;
        private System.Windows.Forms.PictureBox kohleMarkt;
        private System.Windows.Forms.PictureBox stahlMarkt;
        private System.Windows.Forms.PictureBox weizenMarkt;
        private System.Windows.Forms.PictureBox aktuellerSpielerFarbe;
        private System.Windows.Forms.Label farbedesSpielers;
    }
}

