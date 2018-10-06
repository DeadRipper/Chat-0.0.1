using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Net;
using System.Net.Sockets;

namespace Chat_v._0._0._1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        DataForChat dfc = new DataForChat();
        //ClientObject cl = new ClientObject();
        public TcpClient client;
        public NetworkStream stream;

        public void Work()
        {
            client = new TcpClient();
            try
            {
                //if(client.Client.Disconnect = false)
                client.Connect(dfc.ip_textbox_data_prog.Text, Convert.ToInt32(dfc.host_textbox_dataprog.Text)); //подключение клиента
                stream = client.GetStream(); // получаем поток

                string message = dfc.UserName_text_box.Text;
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);

                // запускаем новый поток для получения данных
                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.Start(); //старт потока
                SendMessage();
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                Disconnect();
            }
        }

        // отправка сообщений

        // change block
        public void SendMessage()
        {
            while (true)
            {
                string message = Write_textbox.Text;
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
        }

        // получение сообщений
        public void ReceiveMessage()
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[64]; // буфер для получаемых данных
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    string message = builder.ToString();
                    MainTextBlock.Text = message; //вывод сообщения
                }
                catch
                {
                    //Console.WriteLine("Подключение прервано!"); //соединение было прервано
                    //Console.ReadLine();
                    Disconnect();
                }
            }
        }

        public void Disconnect()
        {
            if (stream != null)
                stream.Close(); //отключение потока
            if (client != null)
                client.Close(); //отключение клиента
            Environment.Exit(0); //завершение процесса
        }


        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            //ServerObject chat = new ServerObject();
            //Thread ListenThread = new Thread(new ThreadStart(chat.Listen));
            //ListenThread.Start();
            //string data = Write_textbox.Text.ToString();
            //data = MainTextBlock.Text;
            SendMessage();
        }
    }

}
