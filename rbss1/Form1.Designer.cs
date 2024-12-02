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
            this.farmbauen = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.truppeComboBox = new System.Windows.Forms.ComboBox();
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
            this.SuspendLayout();
            // 
            // wheatAnzahl
            // 
            this.wheatAnzahl.AutoSize = true;
            this.wheatAnzahl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.wheatAnzahl.Image = global::rbss1.Properties.Resources.labelbackround;
            this.wheatAnzahl.Location = new System.Drawing.Point(837, 402);
            this.wheatAnzahl.Name = "wheatAnzahl";
            this.wheatAnzahl.Size = new System.Drawing.Size(85, 18);
            this.wheatAnzahl.TabIndex = 25;
            this.wheatAnzahl.Text = "Rescourcen:";
            this.wheatAnzahl.Visible = false;
            // 
            // steelAnzahl
            // 
            this.steelAnzahl.AutoSize = true;
            this.steelAnzahl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.steelAnzahl.Image = global::rbss1.Properties.Resources.labelbackround;
            this.steelAnzahl.Location = new System.Drawing.Point(836, 355);
            this.steelAnzahl.Name = "steelAnzahl";
            this.steelAnzahl.Size = new System.Drawing.Size(85, 18);
            this.steelAnzahl.TabIndex = 24;
            this.steelAnzahl.Text = "Rescourcen:";
            this.steelAnzahl.Visible = false;
            // 
            // wheatInventory
            // 
            this.wheatInventory.BackgroundImage = global::rbss1.Properties.Resources.wheatinventory;
            this.wheatInventory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.wheatInventory.Location = new System.Drawing.Point(789, 388);
            this.wheatInventory.Name = "wheatInventory";
            this.wheatInventory.Size = new System.Drawing.Size(42, 39);
            this.wheatInventory.TabIndex = 23;
            this.wheatInventory.TabStop = false;
            this.wheatInventory.Visible = false;
            // 
            // steelInventory
            // 
            this.steelInventory.BackgroundImage = global::rbss1.Properties.Resources.stahlinventory1;
            this.steelInventory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.steelInventory.Location = new System.Drawing.Point(789, 343);
            this.steelInventory.Name = "steelInventory";
            this.steelInventory.Size = new System.Drawing.Size(42, 39);
            this.steelInventory.TabIndex = 22;
            this.steelInventory.TabStop = false;
            this.steelInventory.Visible = false;
            // 
            // coalAnzahl
            // 
            this.coalAnzahl.AutoSize = true;
            this.coalAnzahl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.coalAnzahl.Image = global::rbss1.Properties.Resources.labelbackround;
            this.coalAnzahl.Location = new System.Drawing.Point(837, 312);
            this.coalAnzahl.Name = "coalAnzahl";
            this.coalAnzahl.Size = new System.Drawing.Size(85, 18);
            this.coalAnzahl.TabIndex = 21;
            this.coalAnzahl.Text = "Rescourcen:";
            this.coalAnzahl.Visible = false;
            // 
            // coalInventory
            // 
            this.coalInventory.BackgroundImage = global::rbss1.Properties.Resources.coalinventory1;
            this.coalInventory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.coalInventory.Location = new System.Drawing.Point(789, 298);
            this.coalInventory.Name = "coalInventory";
            this.coalInventory.Size = new System.Drawing.Size(42, 39);
            this.coalInventory.TabIndex = 20;
            this.coalInventory.TabStop = false;
            this.coalInventory.Visible = false;
            // 
            // eisenAnzahl
            // 
            this.eisenAnzahl.AutoSize = true;
            this.eisenAnzahl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.eisenAnzahl.Image = global::rbss1.Properties.Resources.labelbackround;
            this.eisenAnzahl.Location = new System.Drawing.Point(837, 264);
            this.eisenAnzahl.Name = "eisenAnzahl";
            this.eisenAnzahl.Size = new System.Drawing.Size(85, 18);
            this.eisenAnzahl.TabIndex = 19;
            this.eisenAnzahl.Text = "Rescourcen:";
            this.eisenAnzahl.Visible = false;
            // 
            // eisenInventory
            // 
            this.eisenInventory.BackgroundImage = global::rbss1.Properties.Resources.ironinventory;
            this.eisenInventory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.eisenInventory.Location = new System.Drawing.Point(789, 253);
            this.eisenInventory.Name = "eisenInventory";
            this.eisenInventory.Size = new System.Drawing.Size(42, 39);
            this.eisenInventory.TabIndex = 18;
            this.eisenInventory.TabStop = false;
            this.eisenInventory.Visible = false;
            // 
            // rescourcenlabel
            // 
            this.rescourcenlabel.AutoSize = true;
            this.rescourcenlabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rescourcenlabel.Image = global::rbss1.Properties.Resources.labelbackround;
            this.rescourcenlabel.Location = new System.Drawing.Point(806, 227);
            this.rescourcenlabel.Name = "rescourcenlabel";
            this.rescourcenlabel.Size = new System.Drawing.Size(85, 18);
            this.rescourcenlabel.TabIndex = 17;
            this.rescourcenlabel.Text = "Rescourcen:";
            this.rescourcenlabel.Visible = false;
            // 
            // rescourceinventory
            // 
            this.rescourceinventory.BackgroundImage = global::rbss1.Properties.Resources.labelbackround;
            this.rescourceinventory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.rescourceinventory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rescourceinventory.Location = new System.Drawing.Point(766, 211);
            this.rescourceinventory.Name = "rescourceinventory";
            this.rescourceinventory.Size = new System.Drawing.Size(132, 253);
            this.rescourceinventory.TabIndex = 16;
            this.rescourceinventory.TabStop = false;
            this.rescourceinventory.Visible = false;
            // 
            // rescourcenFenster
            // 
            this.rescourcenFenster.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rescourcenFenster.Location = new System.Drawing.Point(1406, 242);
            this.rescourcenFenster.Name = "rescourcenFenster";
            this.rescourcenFenster.Size = new System.Drawing.Size(50, 50);
            this.rescourcenFenster.TabIndex = 15;
            this.rescourcenFenster.TabStop = false;
            this.rescourcenFenster.Click += new System.EventHandler(this.rescourcenFenster_Click);
            // 
            // momentanerSpieler
            // 
            this.momentanerSpieler.AutoSize = true;
            this.momentanerSpieler.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.momentanerSpieler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.momentanerSpieler.Image = global::rbss1.Properties.Resources.labelbackround;
            this.momentanerSpieler.Location = new System.Drawing.Point(822, 155);
            this.momentanerSpieler.Name = "momentanerSpieler";
            this.momentanerSpieler.Size = new System.Drawing.Size(130, 18);
            this.momentanerSpieler.TabIndex = 14;
            this.momentanerSpieler.Text = "Momentaner Spieler";
            // 
            // bewpunktanzeige
            // 
            this.bewpunktanzeige.AutoSize = true;
            this.bewpunktanzeige.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bewpunktanzeige.Image = global::rbss1.Properties.Resources.labelbackround;
            this.bewpunktanzeige.Location = new System.Drawing.Point(836, 108);
            this.bewpunktanzeige.Name = "bewpunktanzeige";
            this.bewpunktanzeige.Size = new System.Drawing.Size(71, 18);
            this.bewpunktanzeige.TabIndex = 13;
            this.bewpunktanzeige.Text = "movpoints";
            // 
            // geldanzeige
            // 
            this.geldanzeige.AutoSize = true;
            this.geldanzeige.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.geldanzeige.Image = global::rbss1.Properties.Resources.labelbackround;
            this.geldanzeige.Location = new System.Drawing.Point(836, 50);
            this.geldanzeige.Name = "geldanzeige";
            this.geldanzeige.Size = new System.Drawing.Size(50, 18);
            this.geldanzeige.TabIndex = 12;
            this.geldanzeige.Text = "money";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::rbss1.Properties.Resources.data;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(766, -6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(132, 221);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // recruitSoldiers
            // 
            this.recruitSoldiers.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("recruitSoldiers.BackgroundImage")));
            this.recruitSoldiers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.recruitSoldiers.Location = new System.Drawing.Point(1389, 167);
            this.recruitSoldiers.Margin = new System.Windows.Forms.Padding(4);
            this.recruitSoldiers.Name = "recruitSoldiers";
            this.recruitSoldiers.Size = new System.Drawing.Size(67, 62);
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
            this.stadtbauen.Location = new System.Drawing.Point(1151, 61);
            this.stadtbauen.Margin = new System.Windows.Forms.Padding(4);
            this.stadtbauen.Name = "stadtbauen";
            this.stadtbauen.Size = new System.Drawing.Size(91, 23);
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
            this.einnehmen.Location = new System.Drawing.Point(971, 332);
            this.einnehmen.Margin = new System.Windows.Forms.Padding(4);
            this.einnehmen.Name = "einnehmen";
            this.einnehmen.Size = new System.Drawing.Size(170, 23);
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
            this.construction.Location = new System.Drawing.Point(1389, 100);
            this.construction.Margin = new System.Windows.Forms.Padding(4);
            this.construction.Name = "construction";
            this.construction.Size = new System.Drawing.Size(67, 62);
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
            this.weiter.Location = new System.Drawing.Point(918, 10);
            this.weiter.Margin = new System.Windows.Forms.Padding(4);
            this.weiter.Name = "weiter";
            this.weiter.Size = new System.Drawing.Size(102, 23);
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
            this.titelLabel.Location = new System.Drawing.Point(1157, 100);
            this.titelLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.titelLabel.Name = "titelLabel";
            this.titelLabel.Size = new System.Drawing.Size(43, 22);
            this.titelLabel.TabIndex = 6;
            this.titelLabel.Text = "Titel";
            this.titelLabel.Visible = false;
            // 
            // ItemPB
            // 
            this.ItemPB.Image = global::rbss1.Properties.Resources.ranged;
            this.ItemPB.Location = new System.Drawing.Point(944, 76);
            this.ItemPB.Margin = new System.Windows.Forms.Padding(4);
            this.ItemPB.Name = "ItemPB";
            this.ItemPB.Size = new System.Drawing.Size(240, 264);
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
            this.truppenLebenLB.Location = new System.Drawing.Point(1157, 155);
            this.truppenLebenLB.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.truppenLebenLB.Name = "truppenLebenLB";
            this.truppenLebenLB.Size = new System.Drawing.Size(81, 27);
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
            this.truppenSchadenLB.Location = new System.Drawing.Point(1157, 211);
            this.truppenSchadenLB.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.truppenSchadenLB.Name = "truppenSchadenLB";
            this.truppenSchadenLB.Size = new System.Drawing.Size(107, 27);
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
            this.anzahlRes.Location = new System.Drawing.Point(1079, 424);
            this.anzahlRes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.anzahlRes.Name = "anzahlRes";
            this.anzahlRes.Size = new System.Drawing.Size(53, 17);
            this.anzahlRes.TabIndex = 2;
            this.anzahlRes.Text = "Anzahl: ";
            this.anzahlRes.Visible = false;
            // 
            // UIInfo
            // 
            this.UIInfo.BackColor = System.Drawing.Color.Transparent;
            this.UIInfo.Image = global::rbss1.Properties.Resources.UI2eisen;
            this.UIInfo.Location = new System.Drawing.Point(1054, 499);
            this.UIInfo.Margin = new System.Windows.Forms.Padding(5);
            this.UIInfo.Name = "UIInfo";
            this.UIInfo.Size = new System.Drawing.Size(384, 116);
            this.UIInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.UIInfo.TabIndex = 1;
            this.UIInfo.TabStop = false;
            this.UIInfo.Visible = false;
            // 
            // UI
            // 
            this.UI.BackColor = System.Drawing.Color.Transparent;
            this.UI.Image = global::rbss1.Properties.Resources.UI1;
            this.UI.Location = new System.Drawing.Point(894, -6);
            this.UI.Margin = new System.Windows.Forms.Padding(5);
            this.UI.Name = "UI";
            this.UI.Size = new System.Drawing.Size(580, 704);
            this.UI.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.UI.TabIndex = 0;
            this.UI.TabStop = false;
            // 
            // farmbauen
            // 
            this.farmbauen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.farmbauen.Image = global::rbss1.Properties.Resources.labelbackround;
            this.farmbauen.Location = new System.Drawing.Point(1054, 61);
            this.farmbauen.Name = "farmbauen";
            this.farmbauen.Size = new System.Drawing.Size(91, 23);
            this.farmbauen.TabIndex = 26;
            this.farmbauen.Text = "Farm Errichten";
            this.farmbauen.UseVisualStyleBackColor = true;
            this.farmbauen.Visible = false;
            this.farmbauen.Click += new System.EventHandler(this.farmbauen_Click);
            // 
            // truppeComboBox
            // 
            this.truppeComboBox.FormattingEnabled = true;
            this.truppeComboBox.Location = new System.Drawing.Point(1268, 76);
            this.truppeComboBox.Name = "truppeComboBox";
            this.truppeComboBox.Size = new System.Drawing.Size(121, 24);
            this.truppeComboBox.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1471, 617);
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
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
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
        private System.Windows.Forms.PictureBox rescourcenFenster;
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
    }
}

