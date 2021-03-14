using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Concurrent;

namespace ZILab4
{
    public partial class Form1 : Form
    {
        private object errorListLock = new object();
        private ConcurrentQueue<string> errorQueue = new ConcurrentQueue<string>();
        private ConcurrentQueue<string> messageQueue = new ConcurrentQueue<string>();
        private TcpClient tcpClient { get; set; } = null;
        private TcpListener tcpListener { get; set; } = null;

        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void MainWindow1(System.ComponentModel.CancelEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void port_TextChanged(object sender, EventArgs e)
        {

        }

        private void Log(string message)
        {
            lock (errorListLock)
            {
                errorQueue.Enqueue(message);
            }
        }

        private void StartServerThread()
        {
            var thread = new Thread(() => {
                try
                {
                    tcpListener.Start();

                    byte[] bytes = new byte[4096];
                    string data = null;

                    while (true)
                    {
                        Log("Waiting for a connection... ");
                        tcpClient = tcpListener.AcceptTcpClient();
                        Log("Connected!");
                        StartListeningThread();
                    }
                } 
                catch (Exception error)
                {
                    Log(error.Message);
                }
            });
            thread.Start();
        }

        private void StartListeningThread()
        {
            var thread = new Thread(() => {
                try
                {
                    string data = null;
                    byte[] bytes = new byte[4096];
                    // Get a stream object for reading and writing
                    NetworkStream stream = tcpClient.GetStream();
                    int i;
                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = Encoding.UTF8.GetString(bytes, 0, i);
                        Log(string.Format("Received: {0}", data));
                        messageQueue.Enqueue(data);
                    }
                }
                catch (Exception error)
                {
                    Log(error.Message);
                }
            });
            thread.Start();
        }

        private void serverButton_Click(object sender, EventArgs e)
        {
            try
            {
                var ip = IPAddress.Parse(ipTextBox.Text);
                var port = Convert.ToInt32(portTextBox.Text);
                serverButton.Enabled = false;
                connectButton.Enabled = false;
                tcpListener = new TcpListener(ip, port);
                StartServerThread();
                sendButton.Enabled = true;
            } 
            catch (Exception error)
            {
                Log(error.Message);
            }
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            try
            {
                var port = Convert.ToInt32(portTextBox.Text);
                var message = "hello world!";
                var data = Encoding.UTF8.GetBytes(message);
                serverButton.Enabled = false;
                connectButton.Enabled = false;
                sendButton.Enabled = true;
                tcpClient = new TcpClient(ipTextBox.Text, port);
                StartListeningThread();
            } 
            catch (Exception error)
            {
                Log(error.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!errorQueue.IsEmpty)
            {
                errorQueue.TryDequeue(out var message);
                errorList.Items.Add(message);
            }

            if (!messageQueue.IsEmpty)
            {
                messageQueue.TryDequeue(out var message);
                messagesList.Items.Add(message);
            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            try
            {
                var message = sendMessageTextBox.Text;
                var data = Encoding.UTF8.GetBytes(message);
                NetworkStream stream = tcpClient.GetStream();
                stream.Write(data, 0, data.Length);
                messageQueue.Enqueue(message);
                Log(string.Format("Sent: {0}", message));
            } 
            catch (Exception error)
            {
                Log(error.Message);
            }
        }



        private void Form1_FormClosing(object sender, FormClosingEventArgs ev)
        {
            try
            {
                tcpClient?.Close();
                Log("tcpClient closed");
            } 
            catch (Exception ex)
            {
                Log(ex.Message);
            }

            try
            {
                tcpListener?.Stop();
                Log("tcpListener closed ");
            }
            catch (Exception e)
            {
                Log(e.Message);
            }
        }
    }
}
