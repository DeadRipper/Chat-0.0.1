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
using System.Windows.Shapes;

namespace Chat_v._0._0._1
{
    /// <summary>
    /// Логика взаимодействия для DataForChat.xaml
    /// </summary>
    public partial class DataForChat : Window
    {
        public DataForChat()
        {
            InitializeComponent();
        }

        private void Conn_button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }
    }
}
