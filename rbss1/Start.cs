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
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
            // Erstellt Picturebox, welches als Hauptbild genutzt wird
            PictureBox mainWindow = new PictureBox
            {
                Size = new Size(this.ClientSize.Width, this.ClientSize.Height),
                Image = Properties.Resources.main_menu,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(0, 0)
            };
            Controls.Add(mainWindow);

            // Erstellt Buttons
            Button startGame = CreateButton("Start Game", new Point(50, 250));
            Button exitGame = CreateButton("Exit Game", new Point(350, 250));

            startGame.Click += new EventHandler(StartGame);
            exitGame.Click += new EventHandler(ExitGame);

            mainWindow.Controls.Add(startGame);
            mainWindow.Controls.Add(exitGame);
        }

        /// <summary>
        /// Hilfsmethode zum Erstellen eines Buttons mit Eigenschaften
        /// </summary>
        /// <param name="text"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        private Button CreateButton(string text, Point location)
        {
            return new Button
            {
                Text = text,
                Size = new Size(200, 50),
                Location = location,
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.DarkSlateGray,
                FlatStyle = FlatStyle.Flat
            };
        }
        /// <summary>
        /// Event-Handler, der beim Klicken SettingsForm aufruft
        /// Validiert nach DialogResult, ob das Game gestartet wird oder nicht
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartGame(Object sender, EventArgs e)
        {
            Settings settings = new Settings();
            DialogResult result = settings.ShowDialog();

            if (result == DialogResult.OK)
            {
                int spielerAnzahl = settings.spielerAnzahl;
                MessageBox.Show($"Das Spiel wird mit {spielerAnzahl} gestartet!");
                Form1 game = new Form1(spielerAnzahl, this);
                game.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Einstellungen wurden nicht geändert.");    
            }

        }
        /// <summary>
        /// Event-Handler, der beim Klicken die StartForm schließt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitGame(Object sender, EventArgs e)
        {
            MessageBox.Show("Exiting Game....");
            this.Close();
        }
    }
}
