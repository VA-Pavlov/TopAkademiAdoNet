using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace TopAkademiAdoNet
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connect;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Connect_Button_Click(object sender, RoutedEventArgs e)
        {connect = new SqlConnection(@"Data Source =(localdb)\MSSQLLocalDB; Initial Catalog = StudentBook;
                                             Integrated Security = SSPI");
            try
            {
                
                connect.Open();
                MessageBlock.Text = "Подключение установлено";
                ConnectPanel.Background = new SolidColorBrush(Colors.LightGreen);
            }
            catch
            {
                MessageBlock.Text = "Ошибка подключения";
                ConnectPanel.Background = new SolidColorBrush(Colors.LightPink);
            }
            

        }
        private void Disconnect_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connect.Close();
                MessageBlock.Text = "Сервер отключен";
                ConnectPanel.Background = new SolidColorBrush(Colors.LightPink);
            }
            catch
            {
                MessageBlock.Text = "Ошибка: сервер не подключен";
            }

        }

        private void AllTableRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SqlCommand select = new SqlCommand();
            select.Connection = connect;
            select.CommandText = @"select * from Table1";
            TextBloxkOne.Text = "";
           SqlDataReader selectReader = select.ExecuteReader();
            while (selectReader.Read())
            {
                for (int i = 0; i < selectReader.FieldCount; i++)
                {
                    TextBloxkOne.Text += selectReader[i] + "\t";
                }
                TextBloxkOne.Text += "\n";
            }
            selectReader.Close();
            
        }



        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            TextBloxkOne.Text = "";
            SqlCommand select = new SqlCommand(@"select FIO from Table1", connect);
            SqlDataReader selectReader = select.ExecuteReader();
            while (selectReader.Read())
            {
                TextBloxkOne.Text += selectReader[0] + "\n";
            }
            selectReader.Close();
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            TextBloxkOne.Text = "";
            SqlCommand select = new SqlCommand(@"select SROC from Table1", connect);
            SqlDataReader selectReader = select.ExecuteReader();

            while (selectReader.Read())
            {
                TextBloxkOne.Text += selectReader[0] + "\n";
            }
            selectReader.Close();
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            TextBloxkOne.Text = "";

            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = "@p1";
            param1.SqlDbType = System.Data.SqlDbType.Float;
            param1.Value = ValueBox.Text;

            SqlCommand select = new SqlCommand(@"select FIO from Table1 where SROC>@p1", connect);
            select.Parameters.Add(param1);
            SqlDataReader selectReader = select.ExecuteReader();
            while (selectReader.Read())
            {
                TextBloxkOne.Text += selectReader[0] + "\n";
            }
            selectReader.Close();
        }

        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
            TextBloxkOne.Text = "";
            SqlCommand select = new SqlCommand(@"select DISTINCT LISTENWITHMINOC from Table1 ", connect);
            SqlDataReader selectReader = select.ExecuteReader();
            while (selectReader.Read())
            {
                TextBloxkOne.Text += selectReader[0] + "\n";
            }
            selectReader.Close();
        }

        private void RadioButton_Checked_4(object sender, RoutedEventArgs e)
        {
            TextBlock2.Text = "";
            SqlCommand select = new SqlCommand(@"select MIN(SROC) from Table1 ", connect);
            SqlDataReader selectReader = select.ExecuteReader();
            selectReader.Read();
            TextBlock2.Text =selectReader[0]+"" ;
            selectReader.Close();
        }

        private void RadioButton_Checked_5(object sender, RoutedEventArgs e)
        {
            TextBlock2.Text = "";
            SqlCommand select = new SqlCommand(@"select MAX(SROC) from Table1 ", connect);
            SqlDataReader selectReader = select.ExecuteReader();
            selectReader.Read();
            TextBlock2.Text = selectReader[0] + "";
            selectReader.Close();
        }

        private void RadioButton_Checked_6(object sender, RoutedEventArgs e)
        {
            TextBlock2.Text = "";
            SqlCommand select = new SqlCommand(@"select COUNT(SROC) from Table1 where LISTENWITHMINOC='Math'", connect);
            SqlDataReader selectReader = select.ExecuteReader();
            selectReader.Read();
            TextBlock2.Text = selectReader[0] + "";
            selectReader.Close();
        }

        private void RadioButton_Checked_7(object sender, RoutedEventArgs e)
        {
            TextBlock2.Text = "";
            SqlCommand select = new SqlCommand(@"SELECT  NameGroup , Count(*) from Table1 group by NameGroup ", connect);
            SqlDataReader selectReader = select.ExecuteReader();
            while (selectReader.Read())
            {
                for (int i = 0; i < selectReader.FieldCount; i++)
                {
                    TextBlock2.Text += selectReader[i] + "\t";
                }
                TextBlock2.Text += "\n";
            }
            selectReader.Close();
        }

        private void RadioButton_Checked_8(object sender, RoutedEventArgs e)
        {
            TextBlock2.Text = "";
            SqlCommand select = new SqlCommand(@"SELECT AVG(SROC)  from Table1", connect);
            SqlDataReader selectReader = select.ExecuteReader();
            selectReader.Read();
            TextBlock2.Text = selectReader[0] + "";
            selectReader.Close();
        }
    }
}
