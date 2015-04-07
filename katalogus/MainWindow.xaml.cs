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
using MySql.Data;
using MySql.Data.MySqlClient;

namespace katalogus
{
    public partial class MainWindow : Window
    {
        public static string connStr = "server=192.168.1.40;user=vstest;database=vstest;port=3306;password=pass;";
        MySqlConnection conn = new MySqlConnection(connStr);
        public MainWindow()
        {
            InitializeComponent();
        }

        private void csatlakoz_Click(object sender, RoutedEventArgs e)
        {
            
            status.Text = "Csatlakozás DB-hez...";
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {

                status.Text = ex.ToString();
            }
            status.Text = "Csatlakozva adatbázishoz.";
        }

        private void kuld_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlCommand parancs = new MySqlCommand();
                parancs.Connection = conn;
                parancs.CommandText = "INSERT INTO vstest.konyvek VALUES(NULL, @cim,@szerzo, @isbn);";
                parancs.Prepare();

                parancs.Parameters.AddWithValue("@cim", cim.Text);
                parancs.Parameters.AddWithValue("@szerzo", szerzo.Text);
                parancs.Parameters.AddWithValue("@isbn", isbn.Text);
                //parancs.Parameters.AddWithValue("@cim", cim.Text);
                parancs.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                status.Text = ex.ToString();
            }
            status.Text = "Elküldve.";
        }
    }
}
