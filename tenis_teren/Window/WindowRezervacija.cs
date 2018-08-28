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
    public partial class WindowRezervacija : Form
    {
        public WindowRezervacija()
        {
            InitializeComponent();

            IDictionary<int, string> dict = new Dictionary<int, string>();
            int sat = int.Parse(Form1.Passsingtime);
          
            var sati2 = new List<string> { "08:00", "09:00", "10:00", "11:00", "12:00",
                "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "" +
                "19:00" };

            int i = 1;
            foreach (var sat4 in sati2)
            {
                if(sat==i )
                {
                    label4.Text = sat4.ToString();
                    break;
                }
                i++;
            }
            
            label1.Text = Form1.Passingdate.Date.ToString("dd/MM/yyyy"); 
        }
   
        private void button1_Click(object sender, EventArgs e)
        {
            double sredstva = DBClanovi.SelectSredstva(WindowPrijava.id_clan);

            if (sredstva<50.00)
            {
                MessageBox.Show("Nemate dovoljno sredstva za napravit rezervaciju");
                this.Close();
            }
           
            else{
                Clan clan = new Clan();

                long? id_clan = WindowPrijava.id_clan;
                clan.id = id_clan;

                long vrijeme = int.Parse(Form1.Passsingtime);

                DateTime datum = DateTime.Parse(label1.Text);

                Teren teren = new Teren("Porec", "67765");

                Rezervacija_terena input = new Rezervacija_terena(teren, vrijeme, datum, clan);

                DBRezervacija_terena.DodajrezervacijuClana(input);                

                sredstva = sredstva - 50;

                DBRezervacija_terena.UpdateSredstva(sredstva, id_clan);

                this.Close();
                MessageBox.Show("Napravili ste rezervaciju");

            }    
           
        }
       
    }
}
