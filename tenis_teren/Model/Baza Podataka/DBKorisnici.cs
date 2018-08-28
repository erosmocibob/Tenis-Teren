using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace tenis_teren
{
    public static class DBKorisnici
    {
        static DBKorisnici()
        {
            SQLiteCommand com = BazaPodataka.con.CreateCommand();

            com.CommandText = @"CREATE TABLE IF NOT EXISTS Korisnici (
				id integer primary key autoincrement,
				ime nvarchar(32) NOT NULL,
				prezime nvarchar(32) NOT NULL)";

            com.ExecuteNonQuery();
            com.Dispose();
        }       

        public static void DodajKorisnik(Korisnik a)
        {
            SQLiteCommand c = BazaPodataka.con.CreateCommand();

            c.CommandText = String.Format(@"INSERT INTO Korisnici (ime, prezime)
                    VALUES ('{0}', '{1}')", a.Ime, a.Prezime);

            c.ExecuteNonQuery();
            c.Dispose();
        }

        public static long? DohvatiKorisnik()
        {
            var lista = new List<Korisnik>();
            SQLiteCommand c = BazaPodataka.con.CreateCommand();
            c.CommandText = String.Format(@"select last_insert_rowid() as id;");

            SQLiteDataReader reader = c.ExecuteReader();
            while (reader.Read())
            {
                Korisnik a = new Korisnik();
                a.id = (long)reader["id"];

                lista.Add(a);
            }

            c.Dispose();
            return lista[0].id;
        }

    }

}
