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
    public partial class WindowUnosKartice : Form
    {
        public WindowUnosKartice()
        {
            InitializeComponent();
            pictureBox1.Hide();
            textBox2.MaxLength = 3;
            textBox1.MaxLength = 16;
            comboBox1.Text = "mjesec";
            comboBox2.Text = "godina";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(pictureBox1.Visible == true)
            {
                pictureBox1.Visible = false;
            }
            else
            {
                pictureBox1.Visible = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Mozete unijet samo brojeve");
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Mozete unijet samo brojeve");
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Mozete unijet samo slova");
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Mozete unijet samo slova");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int broj_kartice = textBox1.Text.Length;
            int sig_kod = textBox2.Text.Length;
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || broj_kartice != 16 
                || textBox4.Text == "" || sig_kod != 3 || comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Niste unijeli sve potrebe podatke");
            }
            else
            {
                MessageBox.Show("Uspjesno ste unijeli kreditnu karticu");
                this.Close();
            }
        }
        
    }
}
