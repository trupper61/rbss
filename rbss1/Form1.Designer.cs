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
            this.UIInfo = new System.Windows.Forms.PictureBox();
            this.UI = new System.Windows.Forms.PictureBox();
            this.anzahlRes = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.UIInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UI)).BeginInit();
            this.SuspendLayout();
            // 
            // UIInfo
            // 
            this.UIInfo.BackColor = System.Drawing.Color.Transparent;
            this.UIInfo.Image = global::rbss1.Properties.Resources.UI2eisen;
            this.UIInfo.Location = new System.Drawing.Point(583, 346);
            this.UIInfo.Name = "UIInfo";
            this.UIInfo.Size = new System.Drawing.Size(216, 76);
            this.UIInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.UIInfo.TabIndex = 1;
            this.UIInfo.TabStop = false;
            this.UIInfo.Visible = false;
            // 
            // UI
            // 
            this.UI.BackColor = System.Drawing.Color.Transparent;
            this.UI.Image = global::rbss1.Properties.Resources.UI1;
            this.UI.Location = new System.Drawing.Point(497, -3);
            this.UI.Name = "UI";
            this.UI.Size = new System.Drawing.Size(326, 465);
            this.UI.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.UI.TabIndex = 0;
            this.UI.TabStop = false;
            // 
            // anzahlRes
            // 
            this.anzahlRes.AutoSize = true;
            this.anzahlRes.BackColor = System.Drawing.Color.Transparent;
            this.anzahlRes.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.anzahlRes.Location = new System.Drawing.Point(626, 346);
            this.anzahlRes.Name = "anzahlRes";
            this.anzahlRes.Size = new System.Drawing.Size(45, 15);
            this.anzahlRes.TabIndex = 2;
            this.anzahlRes.Text = "Anzahl: ";
            this.anzahlRes.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.anzahlRes);
            this.Controls.Add(this.UIInfo);
            this.Controls.Add(this.UI);
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
    }
}

