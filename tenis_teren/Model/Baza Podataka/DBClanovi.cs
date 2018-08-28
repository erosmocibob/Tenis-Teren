using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace tenis_teren
{
    public static class DBClanovi
    { 
        static DBClanovi()
        {
            SQLiteCommand com = BazaPodataka.con.CreateCommand();

            com.CommandText = @"CREATE TABLE IF NOT EXISTS Clanovi (
				    id integer primary key autoincrement,
				    ime nvarchar(32) NOT NULL,
				    prezime nvarchar(32) NOT NULL,
				    oib nvarchar(32) NOT NULL,
                    sredstva DOUBLE NOT NULL,
				    CONSTRAINT oib_unigue UNIQUE(oib))";

                com.ExecuteNonQuery();
                com.Dispose();
        }      
          public static void DodajClan(Clan a)
          {
                SQLiteCommand c = BazaPodataka.con.CreateCommand();
                c.CommandText = String.Format(@"INSERT INTO Clanovi (ime, prezime, oib, sredstva)
                VALUES ('{0}', '{1}', '{2}', {3})", a.Ime, a.Prezime, a.Oib, a.Sredstva);

                c.ExecuteNonQuery();
                c.Dispose();
          }


        public static void DodajSredstva(long? id_b, double sredstva_b)
        {
            SQLiteCommand c = BazaPodataka.con.CreateCommand();
            c.CommandText = String.Format(@"UPDATE Clanovi SET sredstva='{0}' WHERE id= '{1}'", sredstva_b, id_b );
            
            c.ExecuteNonQuery();
            c.Dispose();
        }


        public static double SelectSredstva(long? id_b)
        {
            String sqlCount = @"SELECT COUNT(id) FROM Clanovi WHERE id = " + id_b;
            SQLiteCommand cmdCount = new SQLiteCommand(sqlCount, BazaPodataka.con);
            long nadjenih = (long)cmdCount.ExecuteScalar();

            if (nadjenih > 0)
            {
                String sql = @"SELECT sredstva FROM Clanovi WHERE id = " + id_b;
                SQLiteCommand cmd = new SQLiteCommand(sql, BazaPodataka.con);
                double sredstva = (double)cmd.ExecuteScalar();

                return sredstva;
            }
            else
            {
                return 0.0;
            }           
        }

        public static long SelectRezervacije(long? id_b)
        {
            String sqlCount = @"SELECT COUNT(id) FROM Clanovi WHERE id = " + id_b;
            SQLiteCommand cmdCount = new SQLiteCommand(sqlCount, BazaPodataka.con);
            long nadjenih = (long)cmdCount.ExecuteScalar();
            Console.WriteLine("nadijenih: {0}",nadjenih);
            return nadjenih;
        }

        public static bool ProvjeraOsoba(long? id_b)
        {
            String sqlCount = @"SELECT COUNT(id) FROM Rezervacija_terena WHERE id_osobe = " + id_b;
            SQLiteCommand cmdCount = new SQLiteCommand(sqlCount, BazaPodataka.con);
            long nadjenih = (long)cmdCount.ExecuteScalar();
            bool osoba = false;
            if(nadjenih>0)
            {
                osoba=true;
            }
            else
            {
                osoba = false;
            }
            return osoba;
        }
        

        public static List<Clan> DohvatiSveClanove()
        {
            List<Clan> listaClanova = new List<Clan>();
            SQLiteCommand c = BazaPodataka.con.CreateCommand();

            c.CommandText = "SELECT id, oib, ime, sredstva FROM Clanovi";
            SQLiteDataReader reader = c.ExecuteReader();

            while (reader.Read())
            {
                Clan k = new Clan();
                k.id = (long)reader["id"];
                k.Oib = (string)reader["oib"];
                k.Ime = (string)reader["ime"];
                k.Sredstva = (double)reader["sredstva"];              

                listaClanova.Add(k);
            }
            reader.Dispose();
            c.Dispose();

            return listaClanova;
        }
    }
}
