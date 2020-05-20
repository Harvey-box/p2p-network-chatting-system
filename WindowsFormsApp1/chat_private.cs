using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class chat_private : Form
    {
        Socket thissocket;
        public String adr = "";
        private ManualResetEvent interrupt = new ManualResetEvent(false);
        private delegate void client(Socket file_socket);
        OpenFileDialog openFileDialog1;
        SaveFileDialog saveFileDialog1;
        DialogResult dialogresult;
        string receive_filename;
        bool chatting = true;

        public chat_private(String frdname, Socket newsocket)
        {
            InitializeComponent();
            this.Text = frdname;
            thissocket = newsocket;
            Message_Receive();
        }

        private void Message_Receive()
        {
            byte[] message = new byte[1024]; 
            ListViewItem item;
            thissocket.BeginReceive(message, 0, message.Length, SocketFlags.None, result =>
            {
                int length = thissocket.EndReceive(result);
                string receive_message = Encoding.UTF8.GetString(message, 0, length);
                string message2 = "";
                char first = receive_message[0];
                Console.WriteLine(message2);
                if (length >= 8 && first == '-')
                {
                    for (int i = 0; i < 8; i++)
                    {
                        message2 += receive_message[i];
                    }
                }
                if (message2 == "--file--")
                {
                    receive_filename = "";
                    for(int i=8; i<receive_message.Length; i++)
                    {
                        receive_filename += receive_message[i];
                    }
                    interrupt.Reset();
                    client clien = new client(File_Receive);
                    Invoke(clien, new object[] { thissocket });     //委托線程
                    interrupt.WaitOne();
                    Message_Receive();
                }
                else if (message2 == "--end---")
                {
                    MessageBox.Show("your friend has closed the chatroom", "notice", MessageBoxButtons.OK);
                    //thissocket.Shutdown(SocketShutdown.Both);
                    chatting = false;
                }
                else
                {
                    item = new ListViewItem();
                    item.BackColor = Color.DarkOliveGreen;
                    item.Text = receive_message;
                    Console.WriteLine(item.Text);
                    chatrecord_listView.Invoke(new EventHandler(delegate
                    {
                        chatrecord_listView.Items.Add(item);
                    }));
                    Message_Receive();
                }
            }, null);
        }

        private void File_Receive(Socket file_listener)
        {
            DialogResult dr = MessageBox.Show("A file sending requist is received", "Notice", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            int pass_size = 160000000;
            byte[] pass = new byte[pass_size];
            int single_bytes;
            int total_bytes;
            /*
            byte[] data_receive = new byte[1024];
            int length_receive = thissocket.Receive(data_receive);
            String message_receive = Encoding.UTF8.GetString(data_receive, 0, length_receive);
            String message2 = "";
            String data = "";
            bool isdata = false;
            int datasize = 0;

            for(int i=0; i<message_receive.Length; i++)
            {
                if (isdata == true)
                {
                    data += message_receive[i];
                    datasize++;

                }else if(message_receive[i] != ' ')
                {
                    message2 += message_receive[i];
                }
                else
                {
                    isdata = true;
                }
            }
            Console.WriteLine(message2);*/

            if (dr == DialogResult.OK)//同意接受
            {
                saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "All files（*.*）|*.*|All files(*.*)|*.* ";
                saveFileDialog1.AddExtension = true;
                saveFileDialog1.FileName = receive_filename;
                /*Thread invokeThread = new Thread(new ThreadStart(InvokeMethod2));
                invokeThread.SetApartmentState(ApartmentState.STA);
                invokeThread.Start();
                invokeThread.Join();*/
                dialogresult = saveFileDialog1.ShowDialog();
                if (dialogresult == DialogResult.OK)
                {
                    string savepath = saveFileDialog1.FileName;//文件保存路径
                    FileStream fs = new FileStream(savepath, FileMode.Create, FileAccess.Write);
                    total_bytes = 0;
                    single_bytes = 0;
                    /*fs.Write(Encoding.UTF8.GetBytes(data), 0, datasize);
                    fs.Flush();*/
                    while (true)
                    {
                        single_bytes = file_listener.Receive(pass, pass_size, SocketFlags.None);
                        Console.WriteLine("single: " + single_bytes + " total: " + total_bytes);
                        fs.Write(pass, total_bytes, single_bytes);
                        fs.Flush(true);
                        total_bytes = total_bytes + single_bytes;
                        if (single_bytes < pass_size)//分片结束，接收完毕
                        {
                            break;
                        }
                        
                    }
                }
                MessageBox.Show("file receiving finished", "notice", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("you have refused the file", "notice", MessageBoxButtons.OK);
            }
            interrupt.Set();
        }

        private void submit_button_Click(object sender, EventArgs e)
        {
            if (chatting == false)
            {
                MessageBox.Show("your friend has closing the chatting room", "notice", MessageBoxButtons.OK);
                this.Close();
            }
            String newrecord = enter_TextBox.Text;
            byte[] message = Encoding.UTF8.GetBytes(newrecord);
            //Console.WriteLine(newrecord);
            Console.WriteLine(Encoding.UTF8.GetString(message, 0, message.Length));
            /*if (message.Length > 1200)
            {
                byte[][] message2 = new byte[1200][(int)(message.Length / 1200) + 1];
            }*/
            thissocket.BeginSend(message, 0, message.Length, SocketFlags.None, result =>
            {
                int length = thissocket.EndSend(result);
            },null);

            ListViewItem item = new ListViewItem();
            item.Text = newrecord;
            item.BackColor = Color.AliceBlue;
            enter_TextBox.Text = "";
            chatrecord_listView.Items.Add(item);
        }

        private void sendfile_button_Click(object sender, EventArgs e)
        {
            /*select_file win = new select_file();
            win.Show();
            win.Owner = this;
            if(win.DialogResult == DialogResult.OK)
            {
                Console.WriteLine(adr);
            }*/
            openFileDialog1 = new OpenFileDialog();          //選取文件窗口
            /*Thread invokeThread = new Thread(new ThreadStart(InvokeMethod));
            invokeThread.SetApartmentState(ApartmentState.STA);
            invokeThread.Start();
            invokeThread.Join();*/
            dialogresult = openFileDialog1.ShowDialog();
            if(dialogresult == DialogResult.OK)
            {
                string filename = openFileDialog1.FileName;
                string filename2 = "";
                for (int i = 0; i < filename.Length; i++)               //獲取文件名
                {
                    if (filename[i] == '\\')
                    {
                        filename2 = "";
                    }
                    else
                    {
                        filename2 += filename[i];
                    }
                }
                thissocket.Send(Encoding.UTF8.GetBytes("--file--"+filename2));
                //thissocket.Send(Encoding.UTF8.GetBytes(filename2+' '));
                thissocket.SendFile(filename, null, null, TransmitFileOptions.UseDefaultWorkerThread);
                MessageBox.Show("sended", "notice", MessageBoxButtons.OK);
                Console.WriteLine(filename);
            }
        }

        private void InvokeMethod()
        {
            dialogresult = openFileDialog1.ShowDialog();
        }

        private void InvokeMethod2()
        {
            dialogresult = saveFileDialog1.ShowDialog();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (chatting == true)
            {
                thissocket.Send(Encoding.UTF8.GetBytes("--end---"));
            }
            
            //thissocket.Shutdown(SocketShutdown.Both);
        }
    }
}
