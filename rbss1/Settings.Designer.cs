namespace rbss1
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.playerCountLb = new System.Windows.Forms.Label();
            this.spielerAnzalInput = new System.Windows.Forms.NumericUpDown();
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.spielerAnzalInput)).BeginInit();
            this.SuspendLayout();
            // 
            // playerCountLb
            // 
            this.playerCountLb.AutoSize = true;
            this.playerCountLb.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerCountLb.Location = new System.Drawing.Point(6, 50);
            this.playerCountLb.Name = "playerCountLb";
            this.playerCountLb.Size = new System.Drawing.Size(188, 24);
            this.playerCountLb.TabIndex = 0;
            this.playerCountLb.Text = "Anzahl der Spieler:";
            // 
            // spielerAnzalInput
            // 
            this.spielerAnzalInput.BackColor = System.Drawing.Color.White;
            this.spielerAnzalInput.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spielerAnzalInput.Location = new System.Drawing.Point(256, 48);
            this.spielerAnzalInput.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.spielerAnzalInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spielerAnzalInput.Name = "spielerAnzalInput";
            this.spielerAnzalInput.Size = new System.Drawing.Size(100, 30);
            this.spielerAnzalInput.TabIndex = 1;
            this.spielerAnzalInput.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // okBtn
            // 
            this.okBtn.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.okBtn.Location = new System.Drawing.Point(50, 200);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(100, 40);
            this.okBtn.TabIndex = 2;
            this.okBtn.Text = "Ok";
            this.okBtn.UseVisualStyleBackColor = false;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.BackColor = System.Drawing.Color.IndianRed;
            this.cancelBtn.Location = new System.Drawing.Point(200, 200);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(100, 40);
            this.cancelBtn.TabIndex = 3;
            this.cancelBtn.Text = "Abbrechen";
            this.cancelBtn.UseVisualStyleBackColor = false;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(382, 253);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.spielerAnzalInput);
            this.Controls.Add(this.playerCountLb);
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Game Settings";
            ((System.ComponentModel.ISupportInitialize)(this.spielerAnzalInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label playerCountLb;
        private System.Windows.Forms.NumericUpDown spielerAnzalInput;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
    }
}