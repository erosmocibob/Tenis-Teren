using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tenis_teren
{
    public partial class WindowUplata : Form
    {
        public WindowUplata()
        {
            InitializeComponent();
            comboBox1.Text = "--Koliko kuna zelite uplatiti--";
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Nista odabrali nikakav iznos");
            }
            else
            {
               double kuna1 = WindowPrijava.sredstva;
               double kuna = kuna1+double.Parse(comboBox1.Text);
               long? id = WindowPrijava.id_clan.Value;

               DBClanovi.DodajSredstva(id,kuna);

                MessageBox.Show("Uspješno ste uplatili ste: " + comboBox1.Text + " kn ");
                WindowPrijava.sredstva = kuna;
                this.Close();
            }

        }
    }
}
