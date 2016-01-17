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
    }
}
