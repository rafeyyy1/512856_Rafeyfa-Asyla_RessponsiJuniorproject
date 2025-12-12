using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using ResponsiraFeyfa.Models;

namespace ResponsiraFeyfa
{
    public class ProyekRepository
    {
        public List<Proyek> GetAll()
        {
            var proyekList = new List<Proyek>();

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    string query = "SELECT id_proyek, nama_proyek, client, budget FROM proyek ORDER BY id_proyek";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            proyekList.Add(new Proyek
                            {
                                Id = reader.GetInt32(0),
                                NamaProyek = reader.GetString(1),
                                Client = reader.GetString(2),
                                Budget = reader.GetDecimal(3)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error mengambil data proyek: " + ex.Message);
            }

            return proyekList;
        }

        public DataTable GetDataTable()
        {
            DataTable dt = new DataTable();

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    string query = @"
                        SELECT 
                            nama_proyek AS ""Nama Proyek"", 
                            client AS ""Client"", 
                            budget AS ""Budget (Rp)"" 
                        FROM proyek 
                        ORDER BY id_proyek";

                    using (var adapter = new NpgsqlDataAdapter(query, conn))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error mengambil data tabel proyek: " + ex.Message);
            }

            return dt;
        }

        public decimal GetBudgetById(int idProyek)
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    string query = "SELECT budget FROM proyek WHERE id_proyek = @id";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", idProyek);
                        var result = cmd.ExecuteScalar();
                        return result != null ? Convert.ToDecimal(result) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error mengambil budget proyek: " + ex.Message);
            }
        }

        public decimal GetTotalGajiByProyek(int idProyek, int? excludeDevId = null)
        {
            decimal totalGaji = 0;
            
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    string query = @"
    SELECT id_dev, status_kontrak, fitur_selesai, jumlah_bug
FROM developer 
  WHERE id_proyek = @idProyek";

       using (var cmd = new NpgsqlCommand(query, conn))
    {
          cmd.Parameters.AddWithValue("@idProyek", idProyek);

           using (var reader = cmd.ExecuteReader())
       {
      while (reader.Read())
           {
             int devId = reader.GetInt32(0);
           
        // Skip jika developer ini di-exclude
          if (excludeDevId.HasValue && devId == excludeDevId.Value)
                continue;

      string statusKontrak = reader.GetString(1);
    int fiturSelesai = reader.GetInt32(2);
          int jumlahBug = reader.GetInt32(3);

     // Hitung gaji berdasarkan status kontrak
       Developer dev;
  if (statusKontrak.Equals("Fulltime", StringComparison.OrdinalIgnoreCase))
                {
 dev = new DeveloperFulltime(devId, "", idProyek, fiturSelesai, jumlahBug);
   }
      else
    {
    dev = new PartTimeDeveloper(devId, "", idProyek, fiturSelesai, jumlahBug);
    }

  totalGaji += dev.CalculateTotalGaji();
           }
        }
         }
        }
   }
        catch (Exception ex)
            {
        throw new Exception("Error menghitung total gaji proyek: " + ex.Message);
          }

            return totalGaji;
        }
    }

    public class DeveloperRepository
    {
        public DataTable GetDataTable()
     {
    DataTable dt = new DataTable();

 try
            {
  using (var conn = DatabaseHelper.GetConnection())
        {
             string query = @"
         SELECT 
       d.id_dev AS ""ID"",
    d.nama_dev AS ""Nama Developer"",
    p.nama_proyek AS ""Proyek"",
     d.status_kontrak AS ""Status Kontrak"",
    d.fitur_selesai AS ""Fitur Selesai"",
 d.jumlah_bug AS ""Jumlah Bug""
              FROM developer d
       INNER JOIN proyek p ON d.id_proyek = p.id_proyek
      ORDER BY d.id_dev";

       using (var adapter = new NpgsqlDataAdapter(query, conn))
  {
        adapter.Fill(dt);
            }

            // Tambahkan kolom virtual untuk Skor dan Total Gaji (dihitung di C#)
             dt.Columns.Add("Skor", typeof(int));
         dt.Columns.Add("Total Gaji", typeof(decimal));

            // Hitung skor dan gaji untuk setiap baris
        foreach (DataRow row in dt.Rows)
                 {
string statusKontrak = row["Status Kontrak"].ToString();
  int fiturSelesai = Convert.ToInt32(row["Fitur Selesai"]);
int jumlahBug = Convert.ToInt32(row["Jumlah Bug"]);

        Developer dev;
       if (statusKontrak.Equals("Fulltime", StringComparison.OrdinalIgnoreCase))
      {
        dev = new DeveloperFulltime(0, "", 0, fiturSelesai, jumlahBug);
      }
             else
 {
  dev = new PartTimeDeveloper(0, "", 0, fiturSelesai, jumlahBug);
        }

      row["Skor"] = dev.CalculateSkor();
        row["Total Gaji"] = dev.CalculateTotalGaji();
    }
           }
            }
     catch (Exception ex)
     {
     throw new Exception("Error mengambil data developer: " + ex.Message);
   }

     return dt;
        }

        public bool Insert(Developer developer)
        {
            try
        {
      using (var conn = DatabaseHelper.GetConnection())
             {
           string query = @"
             INSERT INTO developer (nama_dev, id_proyek, status_kontrak, fitur_selesai, jumlah_bug) 
      VALUES (@nama, @proyek, @status, @fitur, @bug)";

            using (var cmd = new NpgsqlCommand(query, conn))
         {
           cmd.Parameters.AddWithValue("@nama", developer.NamaDeveloper);
    cmd.Parameters.AddWithValue("@proyek", developer.IdProyek);
       cmd.Parameters.AddWithValue("@status", developer.StatusKontrak);
         cmd.Parameters.AddWithValue("@fitur", developer.FiturSelesai);
        cmd.Parameters.AddWithValue("@bug", developer.JumlahBug);

        int rowsAffected = cmd.ExecuteNonQuery();
         return rowsAffected > 0;
        }
 }
            }
  catch (Exception ex)
 {
            throw new Exception("Error menambah data developer: " + ex.Message);
      }
        }

    public bool Update(Developer developer)
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
      {
       string query = @"
        UPDATE developer 
       SET nama_dev = @nama, 
  id_proyek = @proyek, 
 status_kontrak = @status, 
       fitur_selesai = @fitur, 
       jumlah_bug = @bug 
          WHERE id_dev = @id";

using (var cmd = new NpgsqlCommand(query, conn))
              {
            cmd.Parameters.AddWithValue("@id", developer.Id);
    cmd.Parameters.AddWithValue("@nama", developer.NamaDeveloper);
                 cmd.Parameters.AddWithValue("@proyek", developer.IdProyek);
      cmd.Parameters.AddWithValue("@status", developer.StatusKontrak);
               cmd.Parameters.AddWithValue("@fitur", developer.FiturSelesai);
                  cmd.Parameters.AddWithValue("@bug", developer.JumlahBug);

               int rowsAffected = cmd.ExecuteNonQuery();
             return rowsAffected > 0;
          }
           }
          }
 catch (Exception ex)
            {
        throw new Exception("Error mengupdate data developer: " + ex.Message);
  }
        }

 public bool Delete(int id)
      {
            try
        {
      using (var conn = DatabaseHelper.GetConnection())
         {
          string query = "DELETE FROM developer WHERE id_dev = @id";

       using (var cmd = new NpgsqlCommand(query, conn))
     {
      cmd.Parameters.AddWithValue("@id", id);

       int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
     }
          }
      }
      catch (Exception ex)
     {
       throw new Exception("Error menghapus data developer: " + ex.Message);
       }
        }

        public Developer? GetById(int id)
 {
          try
 {
    using (var conn = DatabaseHelper.GetConnection())
                {
 string query = @"
 SELECT id_dev, nama_dev, id_proyek, status_kontrak, fitur_selesai, jumlah_bug 
         FROM developer 
 WHERE id_dev = @id";

  using (var cmd = new NpgsqlCommand(query, conn))
             {
              cmd.Parameters.AddWithValue("@id", id);

    using (var reader = cmd.ExecuteReader())
     {
         if (reader.Read())
{
             string statusKontrak = reader.GetString(3);
        Developer dev;

     // Polymorphism - create instance berdasarkan status kontrak
             if (statusKontrak.Equals("Fulltime", StringComparison.OrdinalIgnoreCase))
  {
           dev = new DeveloperFulltime(
    reader.GetInt32(0),
          reader.GetString(1),
       reader.GetInt32(2),
          reader.GetInt32(4),
   reader.GetInt32(5)
  );
         }
             else if (statusKontrak.Equals("Partime", StringComparison.OrdinalIgnoreCase))
   {
  dev = new PartTimeDeveloper(
         reader.GetInt32(0),
     reader.GetString(1),
  reader.GetInt32(2),
     reader.GetInt32(4),
                reader.GetInt32(5)
       );
        }
         else
          {
              dev = new PartTimeDeveloper(
     reader.GetInt32(0),
     reader.GetString(1),
                 reader.GetInt32(2),
 reader.GetInt32(4),
          reader.GetInt32(5)
            );
          }

       return dev;
     }
       }
    }
      }
    }
            catch (Exception ex)
            {
       throw new Exception("Error mengambil data developer: " + ex.Message);
            }

     return null;
        }
    }
}
