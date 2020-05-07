using System;
using System.Net.Sockets;
using System.Text;

namespace Network_Tcp_1
{
    class Program
    {
        //Client
        const int port = 8080;
        const string address = "127.0.0.1";
        static void Main(string[] args)
        {
            Console.Write("What is your name:");
            string userName = Console.ReadLine();
            TcpClient client = null;
            try
            {
                client = new TcpClient(address, port);
                NetworkStream stream = client.GetStream();

                while (true)
                {
                    Console.Write(userName + ": ");

                    string message = Console.ReadLine();
                    message = $"{userName} : {message}";

                    byte[] data = Encoding.Unicode.GetBytes(message);

                    stream.Write(data, 0, data.Length);


                    data = new byte[256];
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    message = builder.ToString();
                    Console.WriteLine("Server: " + message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Close();
            }
        }
    }
}
