using Npgsql;
using System;
using System.Data;

namespace ResponsiraFeyfa
{
    public class DatabaseHelper
    {
        // ========== ENCAPSULATION - Private Fields ==========
        private static string _connectionString =
          "Host=localhost;Port=5432;Database=responsirafeyfa;Username=postgresss;Password=informatika";

        // ========== ENCAPSULATION - Properties dengan Validation ==========
      /// <summary>
        /// Property untuk mengakses SelectedDevId dengan validation
      /// </summary>
        public static int SelectedDevId
  {
            get { return DatabaseHelper._selectedDevId; }
            private set
   {
          if (value >= -1)
   DatabaseHelper._selectedDevId = value;
            }
        }

        private static int _selectedDevId;

        public static NpgsqlConnection GetConnection()
        {
     try
          {
          var connection = new NpgsqlConnection(_connectionString);
    connection.Open();
          return connection;
       }
   catch (NpgsqlException ex)
         {
        throw new Exception("Gagal terhubung ke database PostgreSQL. " +
    "Pastikan PostgreSQL sudah berjalan dan credentials benar.\n" +
    "Error: " + ex.Message);
            }
       catch (Exception ex)
   {
        throw new Exception("Error koneksi database: " + ex.Message);
            }
      }

        public static bool TestConnection()
        {
            try
     {
                using (var conn = GetConnection())
          {
        return conn.State == ConnectionState.Open;
      }
   }
    catch
 {
                return false;
  }
        }
    }
}
