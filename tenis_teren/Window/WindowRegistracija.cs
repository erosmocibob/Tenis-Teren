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
    public partial class WindowRegistracija : Form
    {
        public WindowRegistracija()
        {
            InitializeComponent();
            textBox3.MaxLength = 11;            
        }

        bool unos_kartice = false;
        private void button1_Click(object sender, EventArgs e)
        {
            int chars = textBox3.Text.Length;
            if (textBox1.Text == "" || textBox2.Text=="" || textBox3.Text == "" || chars!= 11 || unos_kartice==false)
            {
                MessageBox.Show("Niste unijeli sve potrebe podatke ili OIB nije 11 znamenaka ili niste dodali kreditnu karticu");
            }
            else
            {
                Clan clan = new Clan();
                
                clan.Ime = textBox1.Text;
                clan.Prezime = textBox2.Text;
                clan.Oib = textBox3.Text;
                clan.Sredstva= 0;
                
                DBClanovi.DodajClan(clan);
                MessageBox.Show("Uspjesno ste napravili račun");
                this.Close();                
            }

        }     
        
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if(!char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Mozete unijet samo brojeve");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Mozete unijet samo slova");
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Mozete unijet samo slova");
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            List<Clan> listaclanova = DBClanovi.DohvatiSveClanove();
            List<Upravitelj> listaupravitelja = DBUpravitelj.DohvatiSveUpravitelje();
            Boolean postojeci_oib = false;
            foreach (var i in listaclanova)
            {
                if (textBox3.Text == i.Oib)
                {
                    postojeci_oib = true;                  
                    break;
                }
            }

            foreach (var i in listaupravitelja)
            {
                if (textBox3.Text == i.Sifra)
                {
                    postojeci_oib = true;
                    break;
                }
            }

            if (postojeci_oib)
            {               
                MessageBox.Show("Vec imate napravljen račun");
                textBox3.Text = "";
            }            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var newform = new WindowUnosKartice();
            newform.ShowDialog();
            unos_kartice = true;
        }
    }
}
