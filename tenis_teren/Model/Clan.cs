using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tenis_teren
{
    public class Clan
    {
        public long? id { get; set; }
        private string ime;
        private string prezime;
        private string oib;
        private double sredstva;

        public Clan(string i, string p, string o, double s)
        {
            this.Ime = i;
            this.Prezime = p;
            this.Oib = o;
            this.Sredstva = s;
        }

        public Clan(long? i, double s)
        {
            this.id = i;
            this.Sredstva = s;
        }

        public Clan() { }

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

        public string Oib
        {
            get
            {
                return oib;
            }
            set
            {
               oib = value;   
            }
        }

        public double Sredstva
        {
            get
            {
                return sredstva;
            }
            set
            {
                sredstva = value;
            }
        }

    }
}