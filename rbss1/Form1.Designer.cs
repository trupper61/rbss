﻿namespace rbss1
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
            this.UIInfo = new System.Windows.Forms.PictureBox();
            this.UI = new System.Windows.Forms.PictureBox();
            this.anzahlRes = new System.Windows.Forms.Label();
            this.truppenLebenLB = new System.Windows.Forms.Label();
            this.truppenSchadenLB = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.UIInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UI)).BeginInit();
            this.SuspendLayout();
            // 
            // UIInfo
            // 
            this.UIInfo.BackColor = System.Drawing.Color.Transparent;
            this.UIInfo.Image = global::rbss1.Properties.Resources.UI2eisen;
            this.UIInfo.Location = new System.Drawing.Point(777, 426);
            this.UIInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.UI.Location = new System.Drawing.Point(663, -4);
            this.UI.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.UI.Name = "UI";
            this.UI.Size = new System.Drawing.Size(435, 572);
            this.UI.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.UI.TabIndex = 0;
            this.UI.TabStop = false;
            // 
            // anzahlRes
            // 
            this.anzahlRes.AutoSize = true;
            this.anzahlRes.BackColor = System.Drawing.Color.Transparent;
            this.anzahlRes.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.anzahlRes.Location = new System.Drawing.Point(835, 426);
            this.anzahlRes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.anzahlRes.Name = "anzahlRes";
            this.anzahlRes.Size = new System.Drawing.Size(53, 17);
            this.anzahlRes.TabIndex = 2;
            this.anzahlRes.Text = "Anzahl: ";
            this.anzahlRes.Visible = false;
            // 
            // truppenLebenLB
            // 
            this.truppenLebenLB.AutoSize = true;
            this.truppenLebenLB.BackColor = System.Drawing.Color.Transparent;
            this.truppenLebenLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.truppenLebenLB.Location = new System.Drawing.Point(774, 79);
            this.truppenLebenLB.Name = "truppenLebenLB";
            this.truppenLebenLB.Size = new System.Drawing.Size(79, 25);
            this.truppenLebenLB.TabIndex = 3;
            this.truppenLebenLB.Text = "Leben:";
            this.truppenLebenLB.Visible = false;
            // 
            // TruppenSchadenLB
            // 
            this.truppenSchadenLB.AutoSize = true;
            this.truppenSchadenLB.BackColor = System.Drawing.Color.Transparent;
            this.truppenSchadenLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.truppenSchadenLB.Location = new System.Drawing.Point(774, 111);
            this.truppenSchadenLB.Name = "TruppenSchadenLB";
            this.truppenSchadenLB.Size = new System.Drawing.Size(105, 25);
            this.truppenSchadenLB.TabIndex = 4;
            this.truppenSchadenLB.Text = "Schaden:";
            this.truppenSchadenLB.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.truppenSchadenLB);
            this.Controls.Add(this.truppenLebenLB);
            this.Controls.Add(this.anzahlRes);
            this.Controls.Add(this.UIInfo);
            this.Controls.Add(this.UI);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
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
    }
}

