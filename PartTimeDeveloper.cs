using System;

namespace ResponsiraFeyfa.Models
{
    /// <summary>
    /// PartTime Developer - Inheritance dari Developer
    /// SKOR = 100 * (1 - (2*Bug)/(3*Fitur))
    /// Total Gaji:
    /// - SKOR >= 80: 500.000 * Fitur
    /// - 50 <= SKOR < 80: 300.000 * Fitur
    /// - SKOR < 50: 400.000 * Fitur
    /// </summary>
public class PartTimeDeveloper : Developer
    {
        private const decimal HighRatePerFitur = 500000m;   // SKOR >= 80
        private const decimal MediumRatePerFitur = 300000m; // 50 <= SKOR < 80
        private const decimal LowRatePerFitur = 400000m;    // SKOR < 50

        public PartTimeDeveloper() : base() { }

        public PartTimeDeveloper(int id, string namaDeveloper, int idProyek, int fiturSelesai, int jumlahBug)
        : base(id, namaDeveloper, idProyek, "Partime", fiturSelesai, jumlahBug)
        {
        }

        /// <summary>
     /// Hitung SKOR untuk Partime: SKOR = 100 * (1 - (2*Bug)/(3*Fitur))
/// </summary>
        public override int CalculateSkor()
        {
            if (FiturSelesai == 0)
          return 0;

            double skorDouble = 100 * (1 - ((2.0 * JumlahBug) / (3.0 * FiturSelesai)));
       int skor = (int)Math.Round(skorDouble);
     return Math.Max(0, Math.Min(100, skor)); // Range 0-100
    }

     /// <summary>
  /// Hitung Total Gaji untuk Partime berdasarkan SKOR
        /// </summary>
        public override decimal CalculateTotalGaji()
        {
      int skor = CalculateSkor();

         if (skor >= 80)
       return HighRatePerFitur * FiturSelesai;
      else if (skor >= 50 && skor < 80)
       return MediumRatePerFitur * FiturSelesai;
          else // SKOR < 50
       return LowRatePerFitur * FiturSelesai;
        }
    }
}
