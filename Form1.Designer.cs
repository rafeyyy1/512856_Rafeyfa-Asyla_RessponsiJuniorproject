namespace ResponsiraFeyfa
{
    partial class Form1
    {
        /// <summary>
      ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
      if (disposing && (components != null))
         {
            components.Dispose();
         }
      base.Dispose(disposing);
      }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label2 = new Label();
            groupBox1 = new GroupBox();
            txtNamaDeveloper = new TextBox();
            cbProyek = new ComboBox();
            cbStatusKontrak = new ComboBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            groupBox2 = new GroupBox();
            label9 = new Label();
            label6 = new Label();
            txtJumlahBug = new TextBox();
            txtFiturSelesai = new TextBox();
            label7 = new Label();
            label8 = new Label();
            btnDelete = new Button();
            btnUpdate = new Button();
            groupBox4 = new GroupBox();
            dgvDeveloper = new DataGridView();
            dgvProyek = new DataGridView();
            btnInsert = new Button();
            groupBox3 = new GroupBox();
            pictureBox1 = new PictureBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDeveloper).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvProyek).BeginInit();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Calibri", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.DeepPink;
            label2.Location = new Point(298, 84);
            label2.Name = "label2";
            label2.Size = new Size(438, 33);
            label2.TabIndex = 1;
            label2.Text = "Developer Team Performance Tracker";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtNamaDeveloper);
            groupBox1.Controls.Add(cbProyek);
            groupBox1.Controls.Add(cbStatusKontrak);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Font = new Font("Calibri", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(32, 121);
            groupBox1.Margin = new Padding(3, 4, 3, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 4, 3, 4);
            groupBox1.Size = new Size(991, 153);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "DATA DEVELOPER";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // txtNamaDeveloper
            // 
            txtNamaDeveloper.Font = new Font("Calibri", 10F);
            txtNamaDeveloper.Location = new Point(149, 27);
            txtNamaDeveloper.Margin = new Padding(3, 4, 3, 4);
            txtNamaDeveloper.Name = "txtNamaDeveloper";
            txtNamaDeveloper.Size = new Size(827, 28);
            txtNamaDeveloper.TabIndex = 9;
            // 
            // cbProyek
            // 
            cbProyek.Font = new Font("Calibri", 10F);
            cbProyek.FormattingEnabled = true;
            cbProyek.Location = new Point(149, 64);
            cbProyek.Margin = new Padding(3, 4, 3, 4);
            cbProyek.Name = "cbProyek";
            cbProyek.Size = new Size(827, 29);
            cbProyek.TabIndex = 8;
            // 
            // cbStatusKontrak
            // 
            cbStatusKontrak.Font = new Font("Calibri", 10F);
            cbStatusKontrak.FormattingEnabled = true;
            cbStatusKontrak.Items.AddRange(new object[] { "Freelance", "Fulltime", "Partime" });
            cbStatusKontrak.Location = new Point(149, 101);
            cbStatusKontrak.Margin = new Padding(3, 4, 3, 4);
            cbStatusKontrak.Name = "cbStatusKontrak";
            cbStatusKontrak.Size = new Size(827, 29);
            cbStatusKontrak.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Calibri", 10F);
            label5.Location = new Point(6, 105);
            label5.Name = "label5";
            label5.Size = new Size(118, 21);
            label5.TabIndex = 6;
            label5.Text = "Status Kontrak:";
            label5.Click += label5_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Calibri", 10F);
            label4.Location = new Point(6, 68);
            label4.Name = "label4";
            label4.Size = new Size(97, 21);
            label4.TabIndex = 5;
            label4.Text = "Pilih Proyek:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Calibri", 10F);
            label3.Location = new Point(6, 31);
            label3.Name = "label3";
            label3.Size = new Size(130, 21);
            label3.TabIndex = 4;
            label3.Text = "Nama Developer:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(txtJumlahBug);
            groupBox2.Controls.Add(txtFiturSelesai);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(label8);
            groupBox2.Font = new Font("Calibri", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox2.Location = new Point(32, 293);
            groupBox2.Margin = new Padding(3, 4, 3, 4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 4, 3, 4);
            groupBox2.Size = new Size(991, 153);
            groupBox2.TabIndex = 9;
            groupBox2.TabStop = false;
            groupBox2.Text = "DATA KINERJA";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Calibri", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label9.Location = new Point(286, 64);
            label9.Name = "label9";
            label9.Size = new Size(183, 18);
            label9.TabIndex = 13;
            label9.Text = "Jumlah Bug yang Ditemukan";
            label9.Click += label9_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Calibri", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label6.Location = new Point(286, 27);
            label6.Name = "label6";
            label6.Size = new Size(186, 18);
            label6.TabIndex = 12;
            label6.Text = "Jumlah Fitur yang Dikerjakan";
            // 
            // txtJumlahBug
            // 
            txtJumlahBug.Font = new Font("Calibri", 10F);
            txtJumlahBug.Location = new Point(149, 64);
            txtJumlahBug.Margin = new Padding(3, 4, 3, 4);
            txtJumlahBug.Name = "txtJumlahBug";
            txtJumlahBug.Size = new Size(125, 28);
            txtJumlahBug.TabIndex = 11;
            // 
            // txtFiturSelesai
            // 
            txtFiturSelesai.Font = new Font("Calibri", 10F);
            txtFiturSelesai.Location = new Point(149, 27);
            txtFiturSelesai.Margin = new Padding(3, 4, 3, 4);
            txtFiturSelesai.Name = "txtFiturSelesai";
            txtFiturSelesai.Size = new Size(125, 28);
            txtFiturSelesai.TabIndex = 10;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Calibri", 10F);
            label7.Location = new Point(6, 68);
            label7.Name = "label7";
            label7.Size = new Size(94, 21);
            label7.TabIndex = 5;
            label7.Text = "Jumlah Bug:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Calibri", 10F);
            label8.Location = new Point(6, 31);
            label8.Name = "label8";
            label8.Size = new Size(99, 21);
            label8.TabIndex = 4;
            label8.Text = "Fitur Selesai:";
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.LightPink;
            btnDelete.Font = new Font("Calibri", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDelete.Location = new Point(794, 454);
            btnDelete.Margin = new Padding(3, 4, 3, 4);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(229, 53);
            btnDelete.TabIndex = 17;
            btnDelete.Text = "DELETE";
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.LightPink;
            btnUpdate.Font = new Font("Calibri", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnUpdate.Location = new Point(406, 454);
            btnUpdate.Margin = new Padding(3, 4, 3, 4);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(229, 53);
            btnUpdate.TabIndex = 16;
            btnUpdate.Text = "EDIT";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click_1;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(dgvDeveloper);
            groupBox4.Font = new Font("Calibri", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox4.Location = new Point(32, 530);
            groupBox4.Margin = new Padding(3, 4, 3, 4);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(3, 4, 3, 4);
            groupBox4.Size = new Size(991, 226);
            groupBox4.TabIndex = 14;
            groupBox4.TabStop = false;
            groupBox4.Text = "DAFTAR PERFORMA TIM";
            // 
            // dgvDeveloper
            // 
            dgvDeveloper.BackgroundColor = Color.LavenderBlush;
            dgvDeveloper.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDeveloper.Font = new Font("Calibri", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dgvDeveloper.GridColor = SystemColors.ScrollBar;
            dgvDeveloper.Location = new Point(8, 29);
            dgvDeveloper.Margin = new Padding(3, 4, 3, 4);
            dgvDeveloper.Name = "dgvDeveloper";
            dgvDeveloper.RowHeadersWidth = 51;
            dgvDeveloper.Size = new Size(968, 179);
            dgvDeveloper.TabIndex = 0;
            dgvDeveloper.CellContentClick += dgvDeveloper_CellContentClick;
            // 
            // dgvProyek
            // 
            dgvProyek.BackgroundColor = SystemColors.Control;
            dgvProyek.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Calibri", 10F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dgvProyek.DefaultCellStyle = dataGridViewCellStyle1;
            dgvProyek.Location = new Point(7, 28);
            dgvProyek.Margin = new Padding(3, 4, 3, 4);
            dgvProyek.Name = "dgvProyek";
            dgvProyek.RowHeadersWidth = 51;
            dgvProyek.Size = new Size(334, 260);
            dgvProyek.TabIndex = 1;
            // 
            // btnInsert
            // 
            btnInsert.BackColor = Color.LightPink;
            btnInsert.Font = new Font("Calibri", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnInsert.Location = new Point(32, 454);
            btnInsert.Margin = new Padding(3, 4, 3, 4);
            btnInsert.Name = "btnInsert";
            btnInsert.Size = new Size(229, 53);
            btnInsert.TabIndex = 15;
            btnInsert.Text = "INSERT";
            btnInsert.UseVisualStyleBackColor = false;
            btnInsert.Click += btnInsert_Click_1;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(dgvProyek);
            groupBox3.Font = new Font("Calibri", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox3.Location = new Point(0, 0);
            groupBox3.Margin = new Padding(3, 4, 3, 4);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(3, 4, 3, 4);
            groupBox3.Size = new Size(0, 0);
            groupBox3.TabIndex = 15;
            groupBox3.TabStop = false;
            groupBox3.Text = "DAFTAR PERFORMA TIM";
            groupBox3.Visible = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(458, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(125, 62);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SeaShell;
            ClientSize = new Size(1063, 769);
            Controls.Add(pictureBox1);
            Controls.Add(groupBox3);
            Controls.Add(btnDelete);
            Controls.Add(btnInsert);
            Controls.Add(groupBox4);
            Controls.Add(btnUpdate);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(label2);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Developer Team Performance Tracker";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDeveloper).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvProyek).EndInit();
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label2;
        private GroupBox groupBox1;
        private Label label3;
        private Label label4;
        private Label label5;
        private ComboBox cbStatusKontrak;
   private ComboBox cbProyek;
        private GroupBox groupBox2;
        private Label label7;
        private Label label8;
        private TextBox txtNamaDeveloper;
        private TextBox txtJumlahBug;
        private TextBox txtFiturSelesai;
        private Label label6;
        private Label label9;
        private GroupBox groupBox4;
    private DataGridView dgvDeveloper;
  private DataGridView dgvProyek;
        private Button btnInsert;
 private Button btnUpdate;
      private Button btnDelete;
        private GroupBox groupBox3;
        private PictureBox pictureBox1;
    }
}
