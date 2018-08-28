using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace tenis_teren
{
    public class Upravitelj
    {
        public long? id { get; set; }
        private string ime;
        private string prezime;
        private string sifra;

        public Upravitelj(string i, string p)
        {
            this.Ime = i;
            this.Prezime = p;

        }

        public Upravitelj() { }

        public string Ime
        {
            get
            {
                return ime;
            }
            set
            {
                ime = value;
            }
        }
        public string Prezime
        {
            get
            {
                return prezime;
            }
            set
            {
                prezime = value;
            }
        }

        public string Sifra
        {
            get
            {
                return sifra;
            }
            set
            {
                sifra = value;
            }

        }

    }
}
