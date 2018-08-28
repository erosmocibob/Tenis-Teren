using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tenis_teren
{
   public class Rezervacija_terena
    {
        public long? id { get; set; }
        private long vrijeme;
        private DateTime datum;
        private Teren teren;
        private Clan clan;
        private Upravitelj upravitelj;
        private Korisnik korisnik;
        private long id_osobe;

        public Rezervacija_terena(Teren a, long d, DateTime t, Upravitelj u)
        {
            this.Teren = a;
            this.Vrijeme = d;
            this.Datum = t;
            this.upravitelj = u;
        }



        public Rezervacija_terena(Teren a, long d, DateTime t, Korisnik k)
        {
            this.Teren = a;
            this.Vrijeme = d;
            this.Datum = t;
            this.korisnik = k;            
        }

        public Rezervacija_terena(Teren a, long d, DateTime t, Clan c)
        {
            this.Teren = a;
            this.Vrijeme = d;
            this.Datum = t;
            this.clan = c;
        }


        public Rezervacija_terena(Teren a, long d, DateTime t, long k)
        {
            this.Teren = a;
            this.Vrijeme = d;
            this.Datum = t;
            this.id_osobe = k;
        }
        public Rezervacija_terena() {}

        public Rezervacija_terena(Teren teren, long vrijeme, DateTime datum, long? id, Korisnik korisnik)
        {
            this.teren = teren;
            this.vrijeme = vrijeme;
            this.datum = datum;
            this.id = id;
            this.korisnik = korisnik;
        }

        public Korisnik Korisnik
        {
            get
            {
                return korisnik;
            }
            set
            {
                korisnik= value;
            }
        }

        public Upravitelj Upravitelj
        {
            get
            {
                return upravitelj;
            }
            set
            {
                upravitelj = value;
            }
        }

        public Clan Clan
        {
            get
            {
                return clan;
            }
            set
            {
                clan = value;
            }
        }

        public DateTime Datum
        {
            get{
                return datum;
            }
            set
            {
                datum = value;
            }
        }

        public long Vrijeme
        {
            get
            {
                return vrijeme;
            }
            set
            {
                vrijeme = value;
            }
        }

        public Teren Teren 
        { 
            get
            {
                return teren;
            }
            set
            {
                  teren = value;
            }
        }

        public long Id_osobe
        {
            get
            {
                return id_osobe;
            }
            set
            {
                id_osobe = value;
            }
        }

    }
}
