using System;
using System.Windows.Forms;
using ResponsiraFeyfa.Models;

namespace ResponsiraFeyfa
{
    public partial class Form1 : Form
    {
        private ProyekRepository proyekRepo = new ProyekRepository();
        private DeveloperRepository developerRepo = new DeveloperRepository();
        private int selectedDevId = 0;

        public Form1()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            try
            {
                LoadProyek();
                LoadData();

                btnInsert.Click += btnInsert_Click;
                btnUpdate.Click += btnUpdate_Click;
                btnDelete.Click += btnDelete_Click;

                dgvDeveloper.CellClick += dgvDeveloper_CellClick;

                dgvDeveloper.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvDeveloper.MultiSelect = false;
                dgvDeveloper.ReadOnly = true;
                dgvProyek.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvProyek.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("error inisialisasi form" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                // Load data developer
                dgvDeveloper.DataSource = developerRepo.GetDataTable();
                dgvDeveloper.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Load data proyek
                dgvProyek.DataSource = proyekRepo.GetDataTable();
                dgvProyek.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProyek()
        {
            try
            {
                var proyekList = proyekRepo.GetAll();
                cbProyek.DataSource = proyekList;
                cbProyek.DisplayMember = "NamaProyek";
                cbProyek.ValueMember = "Id";
                cbProyek.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading proyek: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInsert_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput())
                    return;

                int idProyek = (int)cbProyek.SelectedValue!;
                string statusKontrak = cbStatusKontrak.Text;
                int fiturSelesai = int.Parse(txtFiturSelesai.Text);
                int jumlahBug = int.Parse(txtJumlahBug.Text);

                // Polymorphism - Buat instance berdasarkan status kontrak
                Developer developer;
                if (statusKontrak.Equals("Fulltime", StringComparison.OrdinalIgnoreCase))
                {
                    developer = new DeveloperFulltime(
                        0,
                        txtNamaDeveloper.Text.Trim(),
                        idProyek,
                        fiturSelesai,
                        jumlahBug
                    );
                }
                else if (statusKontrak.Equals("Partime", StringComparison.OrdinalIgnoreCase))
                {
                    developer = new PartTimeDeveloper(
                        0,
                        txtNamaDeveloper.Text.Trim(),
                        idProyek,
                        fiturSelesai,
                        jumlahBug
                    );
                }
                else
                {
                    developer = new PartTimeDeveloper(
                        0,
                        txtNamaDeveloper.Text.Trim(),
                        idProyek,
                        fiturSelesai,
                        jumlahBug
                    );
                }

                decimal budgetProyek = proyekRepo.GetBudgetById(idProyek);
                decimal totalGajiSekarang = proyekRepo.GetTotalGajiByProyek(idProyek);
                decimal gajiDeveloperBaru = developer.CalculateTotalGaji();
                decimal totalGajiBaru = totalGajiSekarang + gajiDeveloperBaru;

                if (totalGajiBaru > budgetProyek)
                {
                    MessageBox.Show(
                        $"Tidak dapat menambahkan developer!\n\n" +
                        $"Budget Proyek: Rp {budgetProyek:N0}\n" +
                        $"Total Gaji Sekarang: Rp {totalGajiSekarang:N0}\n" +
                        $"Gaji Developer Baru: Rp {gajiDeveloperBaru:N0}\n" +
                        $"Total Gaji Akan Menjadi: Rp {totalGajiBaru:N0}\n\n" +
                        $"Kekurangan: Rp {(totalGajiBaru - budgetProyek):N0}\n\n" +
                        $"Proyek akan under budget!",
                        "Budget Tidak Mencukupi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                if (developerRepo.Insert(developer))
                {
                    MessageBox.Show(
                        $"Data developer berhasil ditambahkan!\n\n" +
                        $"Skor: {developer.CalculateSkor()}\n" +
                        $"Total Gaji: Rp {developer.CalculateTotalGaji():N0}\n" +
                        $"Sisa Budget: Rp {(budgetProyek - totalGajiBaru):N0}",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    LoadData();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Gagal menambahkan data developer!",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object? sender, EventArgs e)
        {
            try
            {
                // Cek apakah ada data yang dipilih
                if (selectedDevId == 0)
                {
                    MessageBox.Show("Pilih data developer yang akan diupdate!\n" +
                        "Klik pada baris di tabel Developer.",
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validasi input
                if (!ValidateInput())
                    return;

                // Konfirmasi update
                var result = MessageBox.Show($"Apakah Anda yakin ingin mengupdate data developer?\n\n" +
                    $"Nama: {txtNamaDeveloper.Text}",
                    "Konfirmasi Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    return;

                int idProyek = (int)cbProyek.SelectedValue!;
                string statusKontrak = cbStatusKontrak.Text;
                int fiturSelesai = int.Parse(txtFiturSelesai.Text);
                int jumlahBug = int.Parse(txtJumlahBug.Text);

                // Polymorphism - Buat instance berdasarkan status kontrak
                Developer developer;
                if (statusKontrak.Equals("Fulltime", StringComparison.OrdinalIgnoreCase))
                {
                    developer = new DeveloperFulltime(
                        selectedDevId,
                        txtNamaDeveloper.Text.Trim(),
                        idProyek,
                        fiturSelesai,
                        jumlahBug
                    );
                }
                else if (statusKontrak.Equals("Partime", StringComparison.OrdinalIgnoreCase))
                {
                    developer = new PartTimeDeveloper(
                        selectedDevId,
                        txtNamaDeveloper.Text.Trim(),
                        idProyek,
                        fiturSelesai,
                        jumlahBug
                    );
                }
                else
                {
                    developer = new PartTimeDeveloper(
                        selectedDevId,
                        txtNamaDeveloper.Text.Trim(),
                        idProyek,
                        fiturSelesai,
                        jumlahBug
                    );
                }

                // Validasi Budget - Cek apakah budget mencukupi (exclude developer yang sedang diupdate)
                decimal budgetProyek = proyekRepo.GetBudgetById(idProyek);
                decimal totalGajiLain = proyekRepo.GetTotalGajiByProyek(idProyek, selectedDevId);
                decimal gajiDeveloperBaru = developer.CalculateTotalGaji();
                decimal totalGajiBaru = totalGajiLain + gajiDeveloperBaru;

                if (totalGajiBaru > budgetProyek)
                {
                    MessageBox.Show(
                        $"Tidak dapat mengupdate developer!\n\n" +
                        $"Budget Proyek: Rp {budgetProyek:N0}\n" +
                        $"Total Gaji Developer Lain: Rp {totalGajiLain:N0}\n" +
                        $"Gaji Developer Ini: Rp {gajiDeveloperBaru:N0}\n" +
                        $"Total Gaji Akan Menjadi: Rp {totalGajiBaru:N0}\n\n" +
                        $"Kekurangan: Rp {(totalGajiBaru - budgetProyek):N0}\n\n" +
                        $"Proyek akan under budget!",
                        "Budget Tidak Mencukupi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Update ke database
                if (developerRepo.Update(developer))
                {
                    MessageBox.Show(
                        $"Data developer berhasil diupdate!\n\n" +
                        $"Skor: {developer.CalculateSkor()}\n" +
                        $"Total Gaji: Rp {developer.CalculateTotalGaji():N0}\n" +
                        $"Sisa Budget: Rp {(budgetProyek - totalGajiBaru):N0}",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    LoadData();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Gagal mengupdate data developer!",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object? sender, EventArgs e)
        {
            try
            {
                // Cek apakah ada data yang dipilih
                if (selectedDevId == 0)
                {
                    MessageBox.Show("Pilih data developer yang akan dihapus!\n" +
                        "Klik pada baris di tabel Developer.",
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Konfirmasi delete
                var result = MessageBox.Show($"Apakah Anda yakin ingin menghapus data developer?\n\n" +
                    $"ID: {selectedDevId}\n" +
                    $"Nama: {txtNamaDeveloper.Text}\n\n" +
                    $"Data yang dihapus tidak dapat dikembalikan!",
                    "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                    return;

                // Delete dari database
                if (developerRepo.Delete(selectedDevId))
                {
                    MessageBox.Show("Data developer berhasil dihapus!",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Gagal menghapus data developer!",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDeveloper_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < dgvDeveloper.Rows.Count)
                {
                    DataGridViewRow row = dgvDeveloper.Rows[e.RowIndex];

                    // Ambil data dari row yang dipilih
                    selectedDevId = Convert.ToInt32(row.Cells["ID"].Value);
                    txtNamaDeveloper.Text = row.Cells["Nama Developer"].Value.ToString();

                    // Set proyek combobox
                    string proyekName = row.Cells["Proyek"].Value.ToString() ?? "";
                    cbProyek.Text = proyekName;

                    cbStatusKontrak.Text = row.Cells["Status Kontrak"].Value.ToString();
                    txtFiturSelesai.Text = row.Cells["Fitur Selesai"].Value.ToString();
                    txtJumlahBug.Text = row.Cells["Jumlah Bug"].Value.ToString();

                    // Highlight selected row
                    dgvDeveloper.ClearSelection();
                    row.Selected = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saat memilih data: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            // Validasi Nama Developer
            if (string.IsNullOrWhiteSpace(txtNamaDeveloper.Text))
            {
                MessageBox.Show("Nama developer harus diisi!",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamaDeveloper.Focus();
                return false;
            }

            // Validasi Proyek
            if (cbProyek.SelectedValue == null || cbProyek.SelectedIndex == -1)
            {
                MessageBox.Show("Pilih proyek terlebih dahulu!",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbProyek.Focus();
                return false;
            }

            // Validasi Status Kontrak
            if (string.IsNullOrWhiteSpace(cbStatusKontrak.Text))
            {
                MessageBox.Show("Pilih status kontrak (Freelance/Fulltime)!",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbStatusKontrak.Focus();
                return false;
            }

            // Validasi Fitur Selesai
            if (!int.TryParse(txtFiturSelesai.Text, out int fitur))
            {
                MessageBox.Show("Fitur selesai harus berupa angka!",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFiturSelesai.Focus();
                txtFiturSelesai.SelectAll();
                return false;
            }

            if (fitur < 0)
            {
                MessageBox.Show("Fitur selesai tidak boleh negatif!",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFiturSelesai.Focus();
                txtFiturSelesai.SelectAll();
                return false;
            }

            // Validasi Jumlah Bug
            if (!int.TryParse(txtJumlahBug.Text, out int bug))
            {
                MessageBox.Show("Jumlah bug harus berupa angka!",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtJumlahBug.Focus();
                txtJumlahBug.SelectAll();
                return false;
            }

            if (bug < 0)
            {
                MessageBox.Show("Jumlah bug tidak boleh negatif!",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtJumlahBug.Focus();
                txtJumlahBug.SelectAll();
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            selectedDevId = 0;
            txtNamaDeveloper.Clear();
            cbProyek.SelectedIndex = -1;
            cbStatusKontrak.SelectedIndex = -1;
            txtFiturSelesai.Clear();
            txtJumlahBug.Clear();
            txtNamaDeveloper.Focus();

            // Clear selection di DataGridView
            if (dgvDeveloper.Rows.Count > 0)
            {
                dgvDeveloper.ClearSelection();
            }
        }

        // Keep existing event handlers untuk compatibility
        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void label9_Click(object sender, EventArgs e)
        {
        }

        private void btnInsert_Click_1(object sender, EventArgs e)
        {

        }

        private void dgvDeveloper_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {

        }
    }
}


