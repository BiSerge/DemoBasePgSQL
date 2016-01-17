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
        //String connectionString = "Server=localhost;Port=5432;User=postgres;Password=12564589;Database=demo1;";

        public StartForm()
        {
            InitializeComponent();

            //string connStr = "Server=localhost;Port=5432;User=postgres;Password=12564589;Database=demo;";
            //string connStr = "Server=localhost;Port=5432;User=postgres;Password=12564589;";
            //using (NpgsqlConnection conn = new NpgsqlConnection(connStr))
            //using (NpgsqlCommand cmd = conn.CreateCommand())
            //{
            //    conn.Open();
            //    cmd.CommandText = "CREATE DATABASE IF NOT EXISTS `sharptest`;";
            //    cmd.ExecuteNonQuery();
            //}

            //connStr = "server=localhost;user=root;database=sharptest;";
            using (NpgsqlConnection conn = new NpgsqlConnection(connStr))
                //NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
            using (NpgsqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS friends (id serial," +
                  @"lastname TEXT DEFAULT '' NOT NULL," +
                  @"firstname TEXT DEFAULT '' NOT NULL," +
                  @"birth DATE NOT NULL," +
                  @"CONSTRAINT pid PRIMARY KEY(id));";
                cmd.ExecuteNonQuery();

                //CREATE TABLE public.test(
                //test_id serial NOT NULL,
                //"name"   text,
                //"user"   text DEFAULT "current_user"(),
                ///* Keys */
                //CONSTRAINT test_pkey PRIMARY KEY(test_id)

                //@"birth date NOT NULL default '2000-01-01'," +
            }

            //SelectData();
        }
    }
}
