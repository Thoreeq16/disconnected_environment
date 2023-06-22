using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace disconnected_environment
{
    public partial class FormDataProdi : Form
    {
        private string stringConnection = "data source=DESKTOP-THORIQUNIVERSE\\THORIQ16;" + "database=universitas;User ID=sa;Password=123";
        private SqlConnection koneksi;

        public FormDataProdi()
        {
            InitializeComponent();
            koneksi = new SqlConnection(stringConnection);
            refreshform();

        }
        private void refreshform()
        {
            nmp.Text = "";
            nmp.Enabled = true;
            btnSave.Enabled = true;
            btnClear.Enabled = true;
        }
        private void dataGridView()
        {
            koneksi.Open();
            string str = "select nama_prodi from dbo.prodi";
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            koneksi.Close();
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            dataGridView();
            btnOpen.Enabled = true;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            nmp.Enabled = true;
            btnSave.Enabled=true;
            btnClear.Enabled=true;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            refreshform();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        private void FormDataProdi_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormHalamanUtama hu = new FormHalamanUtama();
            hu.Show();
            this.Hide();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            string nmProdi = nmp.Text;

            if (nmProdi == "")
            {
                MessageBox.Show("Masukkan Nama Prodi", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                koneksi.Open();
                string str = "insert into dbo.prodi (nama_prodi)" + "values(@id)";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("id", nmProdi));
                cmd.ExecuteNonQuery();

                koneksi.Close();
                MessageBox.Show("Data Berhasil Disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView();
                refreshform();
            }
        }
    }
}
