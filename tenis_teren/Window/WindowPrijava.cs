using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace tenis_teren
{
    public partial class WindowPrijava : Form
    {
        public static string prijavljen_ime;
        public static bool prijavljen_clan = false;
        public static double sredstva;
        public static long? id_clan;
        public static long? id_upravitelj;
        public static bool prijavljen_upravitelj = false;

        public WindowPrijava()
        {
            InitializeComponent();

           oib.MaxLength = 11;
        }

        private void button1_Click(object sender, EventArgs e)
        {
        
            List<Clan> listaclanova = DBClanovi.DohvatiSveClanove();

            Boolean prijavaUspjesna = false;
            foreach (var i in listaclanova)
            {
                 if (oib.Text == i.Oib)
                 {
                     prijavaUspjesna = true;
                     prijavljen_ime = i.Ime;
                     sredstva = i.Sredstva;
                     prijavljen_clan = true;
                     id_clan = i.id;
                     prijavljen_upravitelj = false;
                     break;
                 }
            }
            


            if (prijavaUspjesna)
            {               
                MessageBox.Show("Uspjesno ste prijavljeni");               
                this.Close();
            }

            else
            {
                List<Upravitelj> listaupravitelja = DBUpravitelj.DohvatiSveUpravitelje();

                
                foreach(var i in listaupravitelja)
                {
                    if(oib.Text == i.Sifra)
                    {
                        prijavljen_ime = i.Ime;                        
                        prijavljen_upravitelj = true;
                        prijavljen_clan = false;
                        id_upravitelj = i.id;
                        break;


                    }

                    else
                    {
                        MessageBox.Show("Unijeli ste krivi OIB");
                        prijavljen_upravitelj = false;
                        prijavljen_clan = false;
                    }
                }

                if (prijavljen_upravitelj == true)
                {
                    MessageBox.Show("Uspjesno ste prijavljeni kao upravitelj");
                    this.Close();
                }
                           
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void oib_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Mozete unijet samo brojeve");
            }
        }
    }
}
