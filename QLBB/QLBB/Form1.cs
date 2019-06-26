using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace QLBB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String connection = "Data Soure=   (DESCRIPTION =" +
                                "(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))" +
                                "(CONNECT_DATA =" +
                                  "(SERVER = DEDICATED)" +
                                  "(SERVICE_NAME = orcl)" +
                               " )" +
                             " ); User Id = SYSTEM; password=Loverinlen198;";
            OracleConnection con = new OracleConnection();
            con.ConnectionString = connection;

            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "select UCV1 from phieubau where ma_thanhvien='TV1'";
            cmd.Connection = con;

            cmd.CommandType = CommandType.Text;

            OracleDataReader dr = cmd.ExecuteReader();

            dr.Read();

            button1.Text = dr.GetString(0);

        }
    }
}
