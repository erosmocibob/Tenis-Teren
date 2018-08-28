using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace tenis_teren
{
    public partial class WindowTvojeRezervacije : Form
    {
        public WindowTvojeRezervacije()
        {
            InitializeComponent();

            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.MultiSelect = false;
            button3.Hide();
        }

        private void WindowTvojeRezervacije_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("col0", "id");
            dataGridView1.Columns.Add("col1", "Vrijeme");
            dataGridView1.Columns.Add("col2", "Datum");
            dataGridView1.Columns.Add("col3", "Oznaka terena");
            dataGridView1.Columns.Add("col4", "id osobe");
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[4].Visible = false;

            string vrijeme;

            if (WindowPrijava.prijavljen_clan == true)
            {
                Clan a = new Clan();
                long? id_o = WindowPrijava.id_clan;
                a.id = id_o;

                List<Rezervacija_terena> rezervacije = DBRezervacija_terena.GetRezervacija(a);

                foreach (var i in rezervacije)
                {
                    vrijeme = DBRezervacija_terena.DohvatiVrijeme((int)i.Vrijeme);
                    dataGridView1.Rows.Add(i.id, vrijeme, i.Datum.ToLongDateString(), i.Teren.Oznaka_terena, i.Id_osobe);
                }

            }
            else if(WindowPrijava.prijavljen_upravitelj==true)
            {
                button3.Show();
                List<Rezervacija_terena> rezervacije = DBUpravitelj.DohvatiSveRezervacije();

                foreach (var i in rezervacije)
                {
                    vrijeme = DBRezervacija_terena.DohvatiVrijeme((int)i.Vrijeme);
                    dataGridView1.Rows.Add(i.id, vrijeme, i.Datum.ToLongDateString(), i.Teren.Oznaka_terena, i.Id_osobe);
                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Jeste li siguri da zelite stornirat rezervaciju", "Storniraj rezervaciju", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    int selectedRowIdx = this.dataGridView1.SelectedRows[0].Index;
                    long idRez = long.Parse(dataGridView1.Rows[selectedRowIdx].Cells["col0"].Value.ToString());                       

                    long idOsobe = long.Parse(dataGridView1.Rows[selectedRowIdx].Cells["col4"].Value.ToString());
                    DBRezervacija_terena.StornirajRezervaciju(idRez, idOsobe);
                                      
                    dataGridView1.Rows.RemoveAt(selectedRowIdx);
                    DBRezervacija_terena.Izbrisirezervaciju(idRez);
                    if (WindowPrijava.prijavljen_clan == true)
                    {
                        MessageBox.Show("Vraćeno vam je 50 kn na računu");
                    }
                   if (DBClanovi.ProvjeraOsoba(idOsobe) == true && WindowPrijava.prijavljen_upravitelj==true)
                    {
                        MessageBox.Show("Članu je vraćeno 50 kn na računu");
                    }
                    
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string vrijeme;
            
            List<Rezervacija_terena> rezervacije = DBRezervacija_terena.RezervacijeNove();

            foreach (var i in rezervacije)
            {
                vrijeme = DBRezervacija_terena.DohvatiVrijeme((int)i.Vrijeme);
                dataGridView1.Rows.Add(i.id, vrijeme, i.Datum.ToLongDateString(), i.Teren.Oznaka_terena, i.Id_osobe);
            }
        }
    }
}
