using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using System.Data.Common;

namespace DemoBasePgSQL
{
    public partial class StartForm : Form
    {
        private String connStr = "Server=localhost;Port=5432;User=postgres;Password=12564589;Database=demo;";

        public StartForm()
        {
            InitializeComponent();
            
            using (NpgsqlConnection conn = new NpgsqlConnection(connStr))
            using (NpgsqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS friends (id serial," +
                  @"lastname TEXT DEFAULT '' NOT NULL," +
                  @"firstname TEXT DEFAULT '' NOT NULL," +
                  @"birth DATE NOT NULL," +
                  @"CONSTRAINT pid PRIMARY KEY(id));";
                cmd.ExecuteNonQuery();
            }

            SelectData();
        }

        private void SelectData()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connStr))
            using (NpgsqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT * FROM friends";
                NpgsqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EditForm frmEdit = new EditForm();
            DialogResult result = frmEdit.ShowDialog(this);

            if (result == DialogResult.Cancel)
                return;

            using (NpgsqlConnection conn = new NpgsqlConnection(connStr))
            using (NpgsqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = @"INSERT INTO friends (lastname, firstname, birth) VALUES(@lastname, @firstname, @birth);";
                cmd.Parameters.Add(new NpgsqlParameter("@lastname", frmEdit.textBoxLastName.Text));
                cmd.Parameters.Add(new NpgsqlParameter("@firstname", frmEdit.textBoxFirstName.Text));
                cmd.Parameters.Add(new NpgsqlParameter("@birth", frmEdit.dtpBirthDate.Value.ToString("yyyy-MM-dd")));
                cmd.ExecuteScalar();
            }
            SelectData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.SelectedRows[0].Index;
            int id = 0;
            bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
            if (converted == false)
                return;

            EditForm frmEdit = new EditForm();
            NpgsqlDataReader npgSqlDataReader;

            using (NpgsqlConnection conn = new NpgsqlConnection(connStr))
            using (NpgsqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = @"SELECT * FROM friends WHERE id=@id;";
                cmd.Parameters.Add(new NpgsqlParameter("@id", id));

                npgSqlDataReader = cmd.ExecuteReader();

                foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
                {
                    frmEdit.textBoxLastName.Text = dbDataRecord["lastname"].ToString();
                    frmEdit.textBoxFirstName.Text = dbDataRecord["firstname"].ToString();
                    frmEdit.dtpBirthDate.Value = DateTime.Parse(npgSqlDataReader["birth"].ToString());
                }
            }

            
            DialogResult result = frmEdit.ShowDialog(this);

            if (result == DialogResult.Cancel)
                return;

            using (NpgsqlConnection conn = new NpgsqlConnection(connStr))
            using (NpgsqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "UPDATE friends SET lastname=@lastname, firstname=@firstname, birth=@date WHERE id=@id";
                cmd.Parameters.Add(new NpgsqlParameter("@lastname", frmEdit.textBoxLastName.Text));
                cmd.Parameters.Add(new NpgsqlParameter("@firstname", frmEdit.textBoxFirstName.Text));
                cmd.Parameters.Add(new NpgsqlParameter("@date", frmEdit.dtpBirthDate.Value.ToString("yyyy-MM-dd")));
                cmd.Parameters.Add(new NpgsqlParameter("@id", id));
                cmd.ExecuteNonQuery();
            }

            SelectData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
