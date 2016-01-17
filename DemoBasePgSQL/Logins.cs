using System;
using System.Windows.Forms;
using Npgsql;

namespace DemoBasePgSQL
{
    public partial class Logins : Form
    {
        public Logins()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        public NpgsqlConnectionStringBuilder ConStr()
        {
            string myConnectionString = "";
            NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder(myConnectionString);

            builder.Add("Server", textServer.Text);
            builder.Add("Port", textPort.Text);
            builder.Add("User", textUser.Text);
            builder.Add("Password", textPas.Text);
            builder.Add("Database", textBase.Text);

            return builder;
        }
    }
}
