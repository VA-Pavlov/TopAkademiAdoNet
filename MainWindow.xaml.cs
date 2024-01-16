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
        {connect = new SqlConnection(@"Data Source =(localdb)\MSSQLLocalDB; Initial Catalog = StudentsBook;
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
            select.CommandText = @"select * from Ocenki";
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
            SqlCommand select = new SqlCommand(@"select FIO from Ocenki", connect);
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
            SqlCommand select = new SqlCommand(@"select SROC from Ocenki", connect);
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

            SqlCommand select = new SqlCommand(@"select FIO from Ocenki where SROC>@p1", connect);
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
            SqlCommand select = new SqlCommand(@"select DISTINCT NAMELISTENMINOC from Ocenki ", connect);
            SqlDataReader selectReader = select.ExecuteReader();
            while (selectReader.Read())
            {
                TextBloxkOne.Text += selectReader[0] + "\n";
            }
            selectReader.Close();
        }
    }
}
