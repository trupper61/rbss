using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rbss1
{
    public partial class Settings : Form
    {
        public int spielerAnzahl { get; set; }
        public Settings()
        {
            InitializeComponent();

        }
        /// <summary>
        /// Event-Handler -> Click
        /// Speichert Int als spielerAnzahl, DialogResult.OK und schließt sich
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okBtn_Click(object sender, EventArgs e)
        {
            spielerAnzahl = Convert.ToInt32(spielerAnzalInput.Value);
            DialogResult = DialogResult.OK;
            this.Close();
        }
        /// <summary>
        /// Event-Handler -> Click
        /// Setzt DialogResult zu Cancel und schließt sich
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
