using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tenis_teren
{
    public class Teren
    {
        public long? id { get; set; }
        private string naziv_terena;
        private string oznaka_terena;

        public Teren(string s, string o)
        {
            this.Naziv_terena = s;
            this.Oznaka_terena = o;
        }

        public string Naziv_terena
        {
            get
            {
                return naziv_terena;
            }
            set
            {
                naziv_terena = value;
            }
        }


        public string Oznaka_terena
        {
            get
            {
                return oznaka_terena;
            }
            set
            {
                oznaka_terena = value;
            }
        }
        
        public Teren() { }

    }
}
