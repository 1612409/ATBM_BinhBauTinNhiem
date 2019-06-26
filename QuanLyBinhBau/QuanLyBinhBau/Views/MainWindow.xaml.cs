using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Configuration;
using System.Data;


namespace QuanLyBinhBau.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OracleConnection con = null;
        public MainWindow()
        {
            this.setConnection();
            InitializeComponent();
        }

        private void loadDataGrid()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM DBA_ROLES";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            myDataGird.ItemsSource = dt.DefaultView;
            dr.Close();
        }

        private void XemDSRoles()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM DBA_ROLES";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            XemDS.ItemsSource = dt.DefaultView;
            dr.Close();
        }

        private void XemDSTable()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from user_tables where to_date(LAST_ANALYZED) is null and TABLESPACE_NAME='SYSTEM' and TABLE_NAME not like '%$%'";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            XemDS.ItemsSource = dt.DefaultView;
            dr.Close();
        }

        private void XemDSUser()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from dba_users where to_date(EXPIRY_DATE)<> '08-MAR-17' ";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            XemDS.ItemsSource = dt.DefaultView;
            dr.Close();
        }

        private void Them_USer()
        {
            String Name = textName.Text;
            String Pass = textPass.Text;
            String text = "create user " + Name + " identified by " + Pass;
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandText = text;

            cmd.CommandType = CommandType.Text;

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Tao User thanh cong");
                OracleCommand cmd1 = con.CreateCommand();
                cmd1.CommandText = "select * from dba_users where to_date(EXPIRY_DATE)<> '08-MAR-17' ";
                cmd1.CommandType = CommandType.Text;
                OracleDataReader dr = cmd1.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                XemDSUserThem.ItemsSource = dt.DefaultView;
                dr.Close();
            }
            catch(Exception expt)
            {
                MessageBox.Show("Khong the tao User.");
            }
        }

        private void Them_Tables()
        {
            String NameTable = textTable.Text;
            String Column = TextColumns.Text;
            String text = "create table " + NameTable + " ( " + Column+" )";
            //MessageBox.Show(text);
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandText = text;

            cmd.CommandType = CommandType.Text;

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Tao bang thanh cong");
                OracleCommand cmd1 = con.CreateCommand();
                cmd1.CommandText = "select * from user_tables where to_date(LAST_ANALYZED) is null and TABLESPACE_NAME='SYSTEM' and TABLE_NAME not like '%$%'";
                cmd1.CommandType = CommandType.Text;
                OracleDataReader dr = cmd1.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                XemDSTablesThem.ItemsSource = dt.DefaultView;
                dr.Close();
            }
            catch (Exception expt)
            {
                MessageBox.Show("Khong the tao bang.");
            }
        }

        private void Them_Roles()
        {
            String Name = textNameRoles.Text;
            String Pass = textPassRoles.Text;
            String text = "create role " + Name + " identified by " + Pass;
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandText = text;

            cmd.CommandType = CommandType.Text;

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Tao Roles thanh cong");
                OracleCommand cmd1 = con.CreateCommand();
                cmd1.CommandText = "SELECT * FROM DBA_ROLES ";
                cmd1.CommandType = CommandType.Text;
                OracleDataReader dr = cmd1.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                XemDSRolesThem.ItemsSource = dt.DefaultView;
                dr.Close();
            }
            catch (Exception expt)
            {
                MessageBox.Show("Khong the tao Roles.");
            }
        }


        private void setConnection()
        {
            String connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            con = new OracleConnection(connectionString);
            try
            {
                con.Open();
            }
            catch(Exception expt)
            {
                MessageBox.Show("Loi ket  noi");
            }
        }

        private void Windows_load(object sender, RoutedEventArgs e)
        {
            this.loadDataGrid();
        }

        private void Windows_close(object sender, EventArgs e)
        {
            con.Close();
        }

        private void Roles_Click(object sender, RoutedEventArgs e)
        {
            this.XemDSRoles();
        }

        private void User_Click(object sender, RoutedEventArgs e)
        {
            this.XemDSUser();
        }

        private void Table_Click(object sender, RoutedEventArgs e)
        {
            this.XemDSTable();
        }

        private void ThemUser_Click(object sender, RoutedEventArgs e)
        {
            this.Them_USer();
        }

        private void ButtonThemTable_Click(object sender, RoutedEventArgs e)
        {
            this.Them_Tables();
        }

        //private void XemDSRolesThem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    this.Them_Roles();
        //}

        private void ThemRoles_Click(object sender, RoutedEventArgs e)
        {
            this.Them_Roles();
        }
    }
}
