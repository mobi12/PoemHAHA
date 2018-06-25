using System.Windows;
using System.Data.SQLite;
using System;

namespace DesktopManager
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Random randomer = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void RandomPoetry_Click(object sender, RoutedEventArgs e)
        {
            string ConnectStr = "Data Source=" + Environment.CurrentDirectory + "\\tang.db";
            int id = randomer.Next(1, 42985);
            string title = null;
            string content = null;

            ConnectSQL(ConnectStr, ref title, ref content, ref id);

            while (content.Length > 120)
            {
                id = randomer.Next(1, 42985);
                ConnectSQL(ConnectStr, ref title, ref content, ref id);
            }

            Title.Text = title;
            Title.IsEnabled = true;
            PoetryContent.Text = content;
            PoetryContent.IsEnabled = true;
            Thickness thickness = new Thickness(0, 255, 0, 32);
            RandomPoetry.Margin = thickness;
        }

        private void ConnectSQL(string ConnectStr, ref string title, ref string content, ref int id)
        {
            using (SQLiteConnection connect = new SQLiteConnection(ConnectStr))
            {
                connect.Open(); //开启数据库连接

                SQLiteCommand commander = connect.CreateCommand();
                commander.CommandText = "SELECT title, text FROM poem WHERE id=" + id;

                SQLiteDataReader reader = commander.ExecuteReader();

                if (reader.Read())
                {
                    title = reader.GetString(0);
                    content = reader.GetString(1);
                }
            }
        }
    }
}