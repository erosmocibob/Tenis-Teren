using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace tenis_teren
{
    public static class DBTereni
    {
        static DBTereni()
        {
            SQLiteCommand com = BazaPodataka.con.CreateCommand();
            
            com.CommandText = @"CREATE TABLE IF NOT EXISTS Tereni (
				id integer primary key autoincrement,
				naziv_terena nvarchar(32) NOT NULL,
                oznaka_terena nvrchar(32),
                CONSTRAINT oznaka_terena_unique UNIQUE(oznaka_terena))";

            com.ExecuteNonQuery();
            com.Dispose();
        }


        public static List<Teren> DohvatiSve()
        {
            var lista = new List<Teren>();

            SQLiteCommand c = BazaPodataka.con.CreateCommand();

            c.CommandText = String.Format(@"SELECT id, naziv_terena, oznaka_terena FROM Teren");

            SQLiteDataReader reader = c.ExecuteReader();
            while (reader.Read())
            {
                Teren a = new Teren();
                       
                a.id= (long)reader["id"];
                a.Oznaka_terena = (string)reader["oznaka_terena"];

                lista.Add(a);
            }

            c.Dispose();

            return lista;
        }

        public static void Dodaj(Teren a)
        {
            SQLiteCommand c = BazaPodataka.con.CreateCommand();
            c.CommandText = String.Format(@"INSERT INTO Tereni (naziv_terena, oznaka_terena)
            VALUES ('{0}', '{1}')", a.Naziv_terena, a.Oznaka_terena);

            c.ExecuteNonQuery();
            c.Dispose();
        }

    }
}
