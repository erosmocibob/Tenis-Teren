using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace tenis_teren
{
    class DBUpravitelj
    {
        static DBUpravitelj()
        {
            SQLiteCommand com = BazaPodataka.con.CreateCommand();

            com.CommandText = @"CREATE TABLE IF NOT EXISTS Upravitelj (
				    id integer primary key autoincrement,
				    ime nvarchar(32) NOT NULL,
				    prezime nvarchar(32) NOT NULL,
                    sifra nvarchar(32))";

            com.ExecuteNonQuery();
            com.Dispose();
        }


        public static List<Upravitelj> DohvatiSveUpravitelje()
        {
            List<Upravitelj> listaUpravitelja = new List<Upravitelj>();
            SQLiteCommand c = BazaPodataka.con.CreateCommand();

            c.CommandText = "SELECT id, ime, prezime, sifra FROM Upravitelj";

            SQLiteDataReader reader = c.ExecuteReader();

            while (reader.Read())
            {
                Upravitelj k = new Upravitelj();
                k.id = (long)reader["id"];
                k.Sifra = (string)reader["sifra"];
                k.Ime = (string)reader["ime"];

                listaUpravitelja.Add(k);
            }

            reader.Dispose();
            c.Dispose();

            return listaUpravitelja;

        }

        public static List<Rezervacija_terena> DohvatiSveRezervacije()
        {
            var lista = new List<Rezervacija_terena>();
            SQLiteCommand c = BazaPodataka.con.CreateCommand();

            c.CommandText = String.Format(@"SELECT id, vrijeme, datum, oznaka_terena, id_osobe FROM Rezervacija_terena");

            SQLiteDataReader reader = c.ExecuteReader();
            while (reader.Read())
            {
                Rezervacija_terena a = new Rezervacija_terena();
                a.id = (long)reader["id"];
                a.Vrijeme = (long)reader["vrijeme"];
                a.Datum = DateTime.FromFileTime(reader.GetInt64(2));
                a.Teren = new Teren();
                a.Teren.Oznaka_terena = (string)reader["oznaka_terena"];
                a.Id_osobe = (long)reader["id_osobe"];
                lista.Add(a);
            }

            c.Dispose();
            return lista;
        }

    }
}
