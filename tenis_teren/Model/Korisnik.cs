using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tenis_teren
{
    public class Korisnik
    {
        public long? id { get; set; }
        private string ime;
        private string prezime;

        public Korisnik(string i, string p)
        {
            this.Ime = i;
            this.Prezime = p;
        }

        public Korisnik() { }

        public string Ime
        {
            get { return ime; }
            set
            {
                ime = value;
            }
        }
        public string Prezime
        {
            get { return prezime; }
            set
            {                
                prezime = value;
            }
        }        

    }
}
