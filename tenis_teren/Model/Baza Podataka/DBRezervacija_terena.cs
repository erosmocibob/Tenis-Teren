using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace tenis_teren
{
    public static class DBRezervacija_terena
    {

        static DBRezervacija_terena()
        {
           SQLiteCommand com = BazaPodataka.con.CreateCommand();

                com.CommandText = @"CREATE TABLE IF NOT EXISTS Rezervacija_terena (
				       id integer primary key autoincrement,				   
                       vrijeme integer NOT NULL,
                       datum DateTime NOT NULL,
                       oznaka_terena nvrchar(32) NOT NULL,
                       id_osobe LONG NOT NULL, 
                       FOREIGN KEY(oznaka_terena) REFERENCES Tereni(oznaka_terena))";

			        com.ExecuteNonQuery();
			        com.Dispose();            
        }

        public static string DohvatiVrijeme(int vrijemeIdx)
        {
            var vremena = new List<string> { "08:00", "09:00", "10:00", "11:00", "12:00",
                "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00" };
            int idx = vrijemeIdx - 1;

            return vremena[idx];
        }

        public static void DodajrezervacijuClana(Rezervacija_terena v)
        {
            SQLiteCommand c = BazaPodataka.con.CreateCommand();

            c.CommandText = string.Format(@"INSERT INTO Rezervacija_terena (vrijeme, datum, oznaka_terena, id_osobe) 
            VALUES ('{0}', '{1}', '{2}', '{3}')", v.Vrijeme, v.Datum.ToFileTime(), v.Teren.Oznaka_terena, v.Clan.id);

            c.ExecuteNonQuery();
            c.Dispose();

        }

        public static void DodajrezervacijuKorisnika(Rezervacija_terena v)
        {
            SQLiteCommand c = BazaPodataka.con.CreateCommand();

            c.CommandText = string.Format(@"INSERT INTO Rezervacija_terena (vrijeme, datum, oznaka_terena, id_osobe) 
            VALUES ('{0}', '{1}', '{2}', '{3}')", v.Vrijeme, v.Datum.ToFileTime(), v.Teren.Oznaka_terena, v.Korisnik.id);

            c.ExecuteNonQuery();
            c.Dispose();

        }


        public static void DodajrezervacijuUpravitelj(Rezervacija_terena v)
        {
            SQLiteCommand c = BazaPodataka.con.CreateCommand();

            c.CommandText = string.Format(@"INSERT INTO Rezervacija_terena (vrijeme, datum, oznaka_terena, id_osobe) 
            VALUES ('{0}', '{1}', '{2}', '{3}')", v.Vrijeme, v.Datum.ToFileTime(), v.Teren.Oznaka_terena, v.Upravitelj.id);

            c.ExecuteNonQuery();
            c.Dispose();

        }




        public static List<Rezervacija_terena> DohvatiSve()
        {
            var lista = new List<Rezervacija_terena>();
            SQLiteCommand c = BazaPodataka.con.CreateCommand();

            c.CommandText = String.Format(@"SELECT id, vrijeme, datum, oznaka_terena FROM Rezervacija_terena");

            SQLiteDataReader reader = c.ExecuteReader();
            while (reader.Read())
            {
                Rezervacija_terena a = new Rezervacija_terena();
                a.id = (long)reader["id"];
                a.Vrijeme = (long)reader["vrijeme"];
                a.Datum = DateTime.FromFileTime(reader.GetInt64(2));
                a.Teren = new Teren();
                a.Teren.Oznaka_terena = (string)reader["oznaka_terena"];
                lista.Add(a);
            }

            c.Dispose();
            return lista;
        }



        public static  List<Rezervacija_terena> GetRezervacija(Clan b )
        {
            var lista = new List<Rezervacija_terena>();
            SQLiteCommand c = BazaPodataka.con.CreateCommand();

            c.CommandText = String.Format(@"SELECT id, vrijeme, datum, oznaka_terena, id_osobe FROM Rezervacija_terena WHERE id_osobe='{0}' and datum>'{1}' ORDER BY datum ASC;", b.id, DateTime.Now.AddDays(-1).ToFileTime());
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


        public static List<Rezervacija_terena> RezervacijeNove()
        {
            var lista = new List<Rezervacija_terena>();
            SQLiteCommand c = BazaPodataka.con.CreateCommand();

            c.CommandText = String.Format(@"SELECT id, vrijeme, datum, oznaka_terena FROM Rezervacija_terena WHERE datum>'{0}' ORDER BY datum ASC;", DateTime.Now.AddDays(-1).ToFileTime());
            SQLiteDataReader reader = c.ExecuteReader();
            while (reader.Read())
            {
                Rezervacija_terena a = new Rezervacija_terena();
                a.id = (long)reader["id"];
                a.Vrijeme = (long)reader["vrijeme"];
                a.Datum = DateTime.FromFileTime(reader.GetInt64(2));
                a.Teren = new Teren();
                a.Teren.Oznaka_terena = (string)reader["oznaka_terena"];

                lista.Add(a);
            }
            c.Dispose();
            return lista;
        }

        public static void Izbrisirezervaciju(long id)
        {
            SQLiteCommand c = BazaPodataka.con.CreateCommand();
            c.CommandText = string.Format(@"DELETE FROM Rezervacija_terena WHERE id = '{0}'", id);
            c.ExecuteNonQuery();

            c.Dispose();
        }

        public static void StornirajRezervaciju(long id, long? id_osobe)
        {
            SQLiteCommand c = BazaPodataka.con.CreateCommand();
            // dohvati sve clanove
            // provjer ako postoji clan as id == id_osobe
            // ako postoji, storniraj (povecaj iznos sredstava)
            // else samo returnaj

            List<Clan> listaclanova = DBClanovi.DohvatiSveClanove();

            double sredstva = DBClanovi.SelectSredstva(id_osobe);
            sredstva = sredstva + 50.00;
            foreach (var i in listaclanova)
            {
                if (id_osobe == i.id)
                {
                    DBClanovi.DodajSredstva(id_osobe, sredstva);
                    break;
                }               
            }
        }           


        public static void UpdateSredstva(double sredstva,long? id)
        {
            SQLiteCommand c = BazaPodataka.con.CreateCommand();
            c.CommandText = string.Format(@"UPDATE Clanovi SET sredstva='{0}' WHERE id = '{1}'",sredstva, id);
            c.ExecuteNonQuery();

            c.Dispose();
        }
       
    }
}
