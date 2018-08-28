using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace tenis_teren
{
    class BazaPodataka
    {
        private static string connectionString = "Data Source=bazapodataka.sqlite3";
        internal static SQLiteConnection con = new SQLiteConnection(connectionString);

        internal static void OtvoriKonekciju()
        {
            con.Open();
        }

        internal static void ZatvoriKonekciju()
        {
            con.Close();
        }

    }

}
