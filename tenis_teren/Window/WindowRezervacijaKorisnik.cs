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
    public partial class WindowRezervacijaKorisnik : Form
    {
        public WindowRezervacijaKorisnik()
        {
            InitializeComponent();

            

            IDictionary<int, string> dict = new Dictionary<int, string>();

            int sat = int.Parse(Form1.Passsingtime);


            var sati2 = new List<string> { "08:00", "09:00", "10:00", "11:00", "12:00",
                "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00" };

            int i = 1;
            foreach (var sat4 in sati2)
            {
                if (sat == i)
                {
                    label4.Text = sat4.ToString();
                    break;
                }
                i++;
            }

            label1.Text = Form1.Passingdate.Date.ToString("dd/MM/yyyy");  

        }
        bool unos_kartice = false;
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Mozete unijet samo slova");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var newform = new WindowUnosKartice();
            newform.ShowDialog();
            unos_kartice = true;
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || unos_kartice==false )
            {
                MessageBox.Show("Niste unijeli ime, prezime ili niste unijeli kreditnu karticu");
            }
            else
            {
                string ime = textBox1.Text;
                string prezime = textBox2.Text;

                long vrijeme = int.Parse(Form1.Passsingtime);

                DateTime datum = DateTime.Parse(label1.Text);

                Teren teren = new Teren("Porec", "67765");

                Korisnik kor = new Korisnik(ime, prezime);

                DBKorisnici.DodajKorisnik(kor);

                DBKorisnici.DohvatiKorisnik();

                long? korisnik_id = DBKorisnici.DohvatiKorisnik();

                Korisnik zadnji_kor = new Korisnik();
                zadnji_kor.id = korisnik_id;


                Rezervacija_terena input = new Rezervacija_terena(teren, vrijeme, datum, zadnji_kor);


                DBRezervacija_terena.DodajrezervacijuKorisnika(input);

                MessageBox.Show("Uspjesno ste napravili rezervaciju");
                this.Close();

            }  

            
        }
    }
}
