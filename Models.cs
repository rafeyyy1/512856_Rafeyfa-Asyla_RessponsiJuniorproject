using System;

namespace ResponsiraFeyfa.Models
{
    public abstract class BaseEntity
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
    }

    public class Proyek : BaseEntity
    {
      private string namaProyek = string.Empty;
      private string client = string.Empty;
      private decimal budget;

        public string NamaProyek
        {
            get { return namaProyek; }
            set { namaProyek = value; }
      }

    public string Client
      {
            get { return client; }
            set { client = value; }
        }

        public decimal Budget
   {
          get { return budget; }
          set { budget = value; }
        }

      public Proyek()
        {
        }

    public Proyek(int id, string namaProyek, string client, decimal budget)
        {
            this.Id = id;
                this.namaProyek = namaProyek;
                this.client = client;
                this.budget = budget;
        }

        public override string ToString()
        {
     return namaProyek;
      }
    }

    /// <summary>
    /// Fulltime Developer - Inheritance dari Developer
    /// SKOR = (10 * Fitur) - (5 * Bug)
    /// Total Gaji = GajiPokok + (SKOR * 20.000)
    /// </summary>
    public class DeveloperFulltime : Developer
    {
        private const decimal GajiPokok = 5000000m;
        private const decimal SkorMultiplier = 20000m;

        public DeveloperFulltime() : base() { }

     public DeveloperFulltime(int id, string namaDeveloper, int idProyek, int fiturSelesai, int jumlahBug)
      : base(id, namaDeveloper, idProyek, "Fulltime", fiturSelesai, jumlahBug)
     {
        }

        /// <summary>
        /// Hitung SKOR untuk Fulltime: SKOR = (10 * Fitur) - (5 * Bug)
        /// </summary>
        public override int CalculateSkor()
        {
            int skor = (10 * FiturSelesai) - (5 * JumlahBug);
            return Math.Max(0, skor);
        }

        /// <summary>
        /// Hitung Total Gaji untuk Fulltime: GajiPokok + (SKOR * 20.000)
        /// </summary>
        public override decimal CalculateTotalGaji()
        {
            int skor = CalculateSkor();
            return GajiPokok + (skor * SkorMultiplier);
        }
    }

    /// <summary>
    /// Freelance Developer - Inheritance dari Developer
    /// SKOR = (100 * (1 - (2 * Bug) / (3 * Fitur)))
  /// Total Gaji:
    /// - SKOR >= 80: 500.000 * Fitur
    /// - SKOR >= 50: 400.000 * Fitur
    /// - SKOR < 50: 250.000 * Fitur
    /// </summary>
    public class DeveloperFreelance : Developer
    {
      private const decimal HighRatePerFitur = 500000m;   // SKOR >= 80
        private const decimal MediumRatePerFitur = 400000m; // SKOR >= 50
        private const decimal LowRatePerFitur = 250000m;       // SKOR < 50

  public DeveloperFreelance() : base() { }

        public DeveloperFreelance(int id, string namaDeveloper, int idProyek, int fiturSelesai, int jumlahBug)
            : base(id, namaDeveloper, idProyek, "Freelance", fiturSelesai, jumlahBug)
      {
        }

        /// <summary>
        /// Hitung SKOR untuk Freelance: SKOR = (100 * (1 - (2 * Bug) / (3 * Fitur)))
        /// </summary>
   public override int CalculateSkor()
        {
    if (FiturSelesai == 0)
           return 0;

            double skorHitung = 100 * (1 - ((2.0 * JumlahBug) / (3.0 * FiturSelesai)));
      int skor = (int)Math.Round(skorHitung);
            return Math.Max(0, skor);
    }

        /// <summary>
  /// Hitung Total Gaji untuk Freelance berdasarkan SKOR
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

    public abstract class Developer : BaseEntity
    {
      private string namaDeveloper = string.Empty;
   private int idProyek;
    private string statusKontrak = string.Empty;
        private int fiturSelesai;
        private int jumlahBug;

  public string NamaDeveloper
        {
            get { return namaDeveloper; }
     set { namaDeveloper = value; }
        }

    public int IdProyek
        {
            get { return idProyek; }
  set { idProyek = value; }
 }

        public string StatusKontrak
        {
  get { return statusKontrak; }
    set { statusKontrak = value; }
        }

 public int FiturSelesai
        {
         get { return fiturSelesai; }
        set
            {
     if (value < 0)
          throw new ArgumentException("Fitur selesai tidak boleh negatif");
 fiturSelesai = value;
      }
        }

        public int JumlahBug
        {
get { return jumlahBug; }
            set
       {
      if (value < 0)
                    throw new ArgumentException("Jumlah bug tidak boleh negatif");
       jumlahBug = value;
   }
        }

        /// <summary>
        /// Abstract method untuk menghitung SKOR (Polymorphism)
        /// </summary>
        public abstract int CalculateSkor();

        /// <summary>
     /// Abstract method untuk menghitung TOTAL GAJI (Polymorphism)
        /// </summary>
        public abstract decimal CalculateTotalGaji();

        /// <summary>
        /// Check apakah gaji melebihi budget
        /// </summary>
        public bool IsUnderBudget(decimal budget)
        {
   return CalculateTotalGaji() <= budget;
   }

    public Developer()
      {
        }

 public Developer(int id, string namaDeveloper, int idProyek, string statusKontrak, int fiturSelesai, int jumlahBug)
        {
   this.Id = id;
     this.namaDeveloper = namaDeveloper;
    this.idProyek = idProyek;
 this.statusKontrak = statusKontrak;
            this.FiturSelesai = fiturSelesai;
  this.JumlahBug = jumlahBug;
        }
    }
}
