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
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    string query = "SELECT hitung_total_gaji_proyek(@idProyek, @excludeDevId)";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idProyek", idProyek);
                        cmd.Parameters.AddWithValue("@excludeDevId", excludeDevId.HasValue ? (object)excludeDevId.Value : DBNull.Value);

                        var result = cmd.ExecuteScalar();
                        return result != DBNull.Value && result != null ? Convert.ToDecimal(result) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error menghitung total gaji proyek: " + ex.Message);
            }
        }

        public bool ValidasiBudgetProyek(int idProyek, decimal gajiBaru, int? excludeDevId = null)
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    string query = "SELECT cek_budget_proyek(@idProyek, @gajiBaru, @excludeDevId)";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idProyek", idProyek);
                        cmd.Parameters.AddWithValue("@gajiBaru", gajiBaru);
                        cmd.Parameters.AddWithValue("@excludeDevId", excludeDevId.HasValue ? (object)excludeDevId.Value : DBNull.Value);

                        var result = cmd.ExecuteScalar();
                        return result != null && Convert.ToBoolean(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error validasi budget proyek: " + ex.Message);
            }
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
                            d.jumlah_bug AS ""Jumlah Bug"",
                            hitung_skor_developer(d.status_kontrak, d.fitur_selesai, d.jumlah_bug) AS ""Skor"",
                            hitung_gaji_developer(d.status_kontrak, d.fitur_selesai, d.jumlah_bug) AS ""Total Gaji""
                        FROM developer d
                        INNER JOIN proyek p ON d.id_proyek = p.id_proyek
                        ORDER BY d.id_dev";

                    using (var adapter = new NpgsqlDataAdapter(query, conn))
                    {
                        adapter.Fill(dt);
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
                                else
                                {
                                    dev = new DeveloperFreelance(
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
