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
using ZILab3Lib;

namespace ZILab4
{
        public partial class Form1 : Form
        {
            private object errorListLock = new object();
            private ConcurrentQueue<string> errorQueue = new ConcurrentQueue<string>();
            private ConcurrentQueue<string> messageQueue = new ConcurrentQueue<string>();
            private EncodingService encodingService { get; set; } = new EncodingService();
            private TcpClient tcpClient { get; set; } = null;
            private TcpListener tcpListener { get; set; } = null;

            public Form1()
            {
                InitializeComponent();
                timer1.Start();
            }

            private uint[] GetKey()
            {
                try
                {
                    var key = new uint[4] { Convert.ToUInt32(keyBox1.Text), Convert.ToUInt32(keyBox2.Text), Convert.ToUInt32(keyBox3.Text), Convert.ToUInt32(keyBox4.Text) };
                    return key;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Invalid key. {ex.Message}");
                }
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
                            var buffer = new byte[i];
                            Array.Copy(bytes, buffer, i);
                            Log(string.Format("Received bytes: {0}, size: {1}", BitConverter.ToString(buffer), i));
                            data = DecodeText(buffer);
                            Log(string.Format("Received: {0}", data));
                            if (data != null)
                            {
                                messageQueue.Enqueue(data);
                            }
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
                    serverButton.Enabled = false;
                    connectButton.Enabled = false;
                    sendButton.Enabled = true;
                    tcpClient = new TcpClient(ipTextBox.Text, port);
                    Log("Connected to the server");
                    StartListeningThread();
                } 
                catch (Exception error)
                {
                    Log(error.Message);
                }
            }


            private byte[] EncodeText(string Text)
            {
                try 
                { 
                    var bytes = Encoding.UTF8.GetBytes(Text);
                    var key = GetKey();
                    var jey1 = string.Join(",", key);
                    Log($"Props for calculating mac: {BitConverter.ToString(bytes)} {jey1}");
                    var encodedBytes = encodingService.Encode(bytes, key);
                    Log($"encoded bytes: {BitConverter.ToString(encodedBytes)}");
                    Log($"Encoded Text, size: {encodedBytes.Length}");

                    return encodedBytes.Concat(bytes).ToArray();
                }
                catch(Exception e)
                {
                    Log($"Decoding Error {e.Message}");
                }

                return null;
            }

            private string DecodeText(byte[] bytes)
            {
                try
                {
                    var key = GetKey();
                    // First 16 bytes is MAC
                    var origFileSize = bytes.Length - 16;

                    var encodedSize = 16;
                    var encodedPart = new byte[16];
                    var origPart = new byte[origFileSize];

                    Log($"encodedSize {encodedSize}");
                    Log($"origFileSize {origFileSize}");
                    Log($"totalSize {bytes.Length}");

                    Array.Copy(bytes, 0, encodedPart, 0, encodedSize);
                    Array.Copy(bytes, 16, origPart, 0, origFileSize);

                    var jey1 = string.Join(",", key);
                    Log($"Props for calculating mac: {BitConverter.ToString(origPart)} {jey1}");
                    var mac = encodingService.Encode(origPart, key);

                    bool msgVerified = Enumerable.SequenceEqual(encodedPart, mac);
                    var verifiedText = msgVerified ? "verified" : "unverified";

                    Log($"Recieved mac {BitConverter.ToString(encodedPart)}");
                    Log($"Calculated mac {BitConverter.ToString(mac)}");

                    var text = $"{Encoding.UTF8.GetString(origPart)}  ({verifiedText})";

                    Log($"Verfied text: {msgVerified}");

                    return text;
                }
                catch(Exception e)
                {
                    Log($"Decoding Error {e.Message}");
                }

                return null;
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
                    var data = EncodeText(message);

                    if (data != null)
                    {
                        NetworkStream stream = tcpClient.GetStream();
                        stream.Write(data, 0, data.Length);
                        messageQueue.Enqueue(message);
                        Log(string.Format("Sent: {0}, bytes: {1}", message, BitConverter.ToString(data)));
                    }
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
