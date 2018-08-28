using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

namespace tenis_teren
{

    public partial class Form1 : Form
    {        
        public static DateTime Passingdate;
        public static string Passsingtime;
        public static bool appJustOpened = true;

        public Form1()
        {
            this.BackgroundImage = Properties.Resources.bg;
            this.BackgroundImageLayout = ImageLayout.Stretch;           

            InitializeComponent();

            pictureBox1.Hide();
            tableLayoutPanel1.BackColor = Color.Transparent;
            button63.FlatAppearance.BorderColor = Color.White;
            List<Button> lstBtn = new List<Button>
            {
                button1, button2, button3, button4, button5,
                button6, button7, button8, button9, button10,
                button11, button12, button13, button14, button15,
                button16, button17, button18, button19, button20,
                button21, button22, button23, button24, button25,
                button26, button27, button28, button29, button30,
                button31, button32, button33, button34, button35,
                button36, button37, button38, button39, button40,
                button41, button42, button43, button44, button45,
                button46, button47, button48, button49, button50,
                button51, button52, button53, button54, button55,
                button56, button57, button58, button59, button60, 
            };
            foreach (Button btn in lstBtn)
            {
                btn.Click += myButtonClick;
            }           
        }
        
        private void myButtonClick(object sender, EventArgs e)
        {
            
            Button btn = sender as Button;
            string [] btnTagData = btn.Tag.ToString().Split('|');
            DateTime datum = DateTime.Parse(btnTagData[0]);
            string sat = btnTagData[1];
            
            if (btn.BackColor == Color.LightGreen)
            {
               Passingdate = datum.Date;
                Passsingtime = sat;

                if(WindowPrijava.prijavljen_clan==true)
                {
                     var newForm = new WindowRezervacija();
                    newForm.ShowDialog();
                }
                else if(WindowPrijava.prijavljen_upravitelj==true)
                {
                    var newForm = new WindowRezervacijaUpravitelj();
                    newForm.ShowDialog();
                }
                else
                {
                    var newForm = new WindowRezervacijaKorisnik();
                    newForm.ShowDialog();
                }
            }            
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {                                   
            //stavaljane datum na lebele
            int i = 0;
            var labels_datum = new List<Label> {label1, label2, label3, label4, label5};
            foreach (var label in labels_datum)
            {
                label.Text = DateTime.Now.AddDays(i).ToLongDateString();
                label.BackColor = Color.Transparent;
                i++;
            }            

            //stavljane dane na labele
            DateTime test ;
            var culture = new System.Globalization.CultureInfo("hr-HR");

            int j = 0;
            var labels_dan = new List<Label> {label18, label19, label20, label21, label22};
            foreach (var label in labels_dan)
            {
                test = DateTime.Now.AddDays(j);
                 
                label.Text = culture.DateTimeFormat.GetDayName(test.DayOfWeek).ToUpper();
                label.BackColor = Color.Transparent;
                j++;
            }

        }


        private void Button70_Click(object sender, EventArgs e)
        {
            var newForm = new WindowPrijava();
            newForm.StartPosition = FormStartPosition.CenterScreen;
            newForm.ShowDialog();          
        }

        private void button71_Click(object sender, EventArgs e)
        {
            var newForm = new WindowRegistracija();
            newForm.StartPosition = FormStartPosition.CenterScreen;
            newForm.ShowDialog();
        }

        private void button61_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            if (WindowPrijava.prijavljen_clan == true)
            {
                List<Clan> listaclanova = DBClanovi.DohvatiSveClanove();

                var id_clana = WindowPrijava.id_clan;

                double? sredstva = DBClanovi.SelectSredstva(id_clana);
                label23.Text = WindowPrijava.prijavljen_ime.ToString();

                button62.Text = sredstva.ToString() + " HRK";
                pictureBox1.Show();
                button62.Show();
                button63.Show();
            }
            else if (WindowPrijava.prijavljen_upravitelj==true)
            {
                label23.Text = WindowPrijava.prijavljen_ime.ToString();
                pictureBox1.Show();
                button62.Hide();
                button63.Show();
            }
            else
            {
                button62.Hide();
                button63.Hide();
            }

            IDictionary<string, Button> dict = new Dictionary<string, Button>();

            List<Button> lstBtn = new List<Button>
            {
                button1, button2, button3, button4, button5, button6,
                button7, button8, button9, button10, button11,
                button12, button13, button14, button15, button16,
                button17, button18, button19, button20, button21,
                button22, button23, button24, button25, button26,
                button27, button28, button29, button30, button31,
                button32, button33, button34, button35, button36,
                button37, button38, button39, button40, button41,
                button42, button43, button44, button45, button46,
                button47, button48, button49, button50, button51,
                button52, button53, button54, button55, button56,
                button57, button58, button59, button60
            };


            for (int x = 1; x <= 60; x++)
            {
                dict.Add(x.ToString(), lstBtn[x - 1]);
            }

            bool popunjeno;
            int buttonIdx = 0;

            int[] sati = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

            var rezervacije3 = DBRezervacija_terena.DohvatiSve();
            Rezervacija_terena rez;

            for (int d = 0; d < 5; d++)
            {
                DateTime trenutni_datum = DateTime.Now.AddDays(d);

                foreach (int sat in sati)
                {
                    popunjeno = false;
                    
                    for (int rezIdx = 0; rezIdx < rezervacije3.Count(); rezIdx++)
                    {
                        rez = rezervacije3[rezIdx];

                        if (rez.Vrijeme == sat && rez.Datum.Date == trenutni_datum.Date)
                        {
                            popunjeno = true;
                            break;
                        }
                    }
                    lstBtn[buttonIdx].Tag = trenutni_datum.Date.ToString() + '|' + sat.ToString();

                    if (popunjeno)
                    {
                        lstBtn[buttonIdx].BackColor = Color.Red;
                        lstBtn[buttonIdx].Text = "ZAUZETO";
                    }
                    else
                    {
                        lstBtn[buttonIdx].BackColor = Color.LightGreen;
                        lstBtn[buttonIdx].Text = "DOSTUPNO";
                    }

                    buttonIdx++;
                }                
            }

            if (Form1.appJustOpened)
            {
                WindowInformacije informacijeWindow = new WindowInformacije();
                informacijeWindow.Show();
                Form1.appJustOpened = false;
            }

        }

        private void button62_Click(object sender, EventArgs e)
        {
            var newform = new WindowUplata();
            newform.ShowDialog();
        }

        private void button63_Click(object sender, EventArgs e)
        {
            if (WindowPrijava.prijavljen_upravitelj == true)
            {
                var newform = new WindowTvojeRezervacije();
                newform.ShowDialog();
            }           
            else if (DBClanovi.SelectRezervacije(WindowPrijava.id_clan) < 0 )
            {
                MessageBox.Show("Nemate niti jednu rezervaciju");
            }         
           else
            {
                var newform = new WindowTvojeRezervacije();
                newform.ShowDialog();
            }    
        }

        private void button64_Click(object sender, EventArgs e)
        {
            var newform = new WindowInformacije();
            newform.ShowDialog();            
        }
       
    }
}
