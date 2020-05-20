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
    public partial class chat_group : Form
    {
        Socket[] socketlist;
        int allnumber;
        String[] allusername;
        String thisname;
        public String adr = "";
        private ManualResetEvent interrupt = new ManualResetEvent(false);
        private delegate void client(Socket file_socket);
        OpenFileDialog openFileDialog1;
        SaveFileDialog saveFileDialog1;
        DialogResult dialogresult;
        string receive_filename;
        bool chatting = true;

        public chat_group(String allname, Socket[] allsocket, int number, string name)
        {
            InitializeComponent();
            socketlist = allsocket;
            thisname = name;
            allusername = allname.Split(' ');
            ListViewItem item;
            for (int i=0; i<number; i++)
            {
                item = new ListViewItem();
                item.Text = allusername[i];
                member_listView.Items.Add(item);
            }
            Message_Receive();
        }

        private void Message_Receive()
        {
            byte[] message = new byte[1024];
            ListViewItem item;

            foreach (Socket thissocket in socketlist)
            {
                thissocket.BeginReceive(message, 0, message.Length, SocketFlags.None, result =>
                {
                    int length = thissocket.EndReceive(result);
                    string receive_message = Encoding.UTF8.GetString(message, 0, length);
                    string message2 = "";
                    Console.WriteLine(message2);
                    for (int i = 0; i < 8; i++)
                    {
                        message2 += receive_message[i];
                    }
                    if (message2 == "--file--")
                    {
                        receive_filename = "";
                        for (int i = 8; i < receive_message.Length; i++)
                        {
                            receive_filename += receive_message[i];
                        }
                        MessageBox.Show("1", "notice", MessageBoxButtons.OK);
                        interrupt.Reset();
                        client clien = new client(File_Receive);
                        Invoke(clien, new object[] { thissocket });     //委托線程
                        MessageBox.Show("2", "notice", MessageBoxButtons.OK);
                        interrupt.WaitOne();
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
                        
                    }
                }, null);
            }
            Message_Receive();
        }

        private void File_Receive(Socket file_listener)
        {
            DialogResult dr = MessageBox.Show("A file sending requist is received", "Notice", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            int pass_size = 500000;
            byte[] pass = new byte[pass_size];
            int single_bytes;
            int total_bytes;
            bool receive = false;

            if (dr == DialogResult.OK)//同意接受
            {
                saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "All files（*.*）|*.*|All files(*.*)|*.* ";
                saveFileDialog1.AddExtension = true;
                saveFileDialog1.FileName = receive_filename;
                Thread invokeThread = new Thread(new ThreadStart(InvokeMethod2));
                invokeThread.SetApartmentState(ApartmentState.STA);
                invokeThread.Start();
                invokeThread.Join();
                if (dialogresult == DialogResult.OK)
                {
                    receive = true;
                    string savepath = saveFileDialog1.FileName;//文件保存路径
                    FileStream fs = new FileStream(savepath, FileMode.Create, FileAccess.Write);
                    total_bytes = 0;
                    single_bytes = 0;
                    /*fs.Write(Encoding.UTF8.GetBytes(data), 0, datasize);
                    fs.Flush();*/
                    while (true)
                    {
                        single_bytes = file_listener.Receive(pass, pass_size, SocketFlags.None);
                        foreach(Socket thissocket in socketlist)
                        {
                            if (thissocket != file_listener)
                            {
                                thissocket.Send(pass);
                            }
                        }
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
            
            if(receive == false)
            {
                total_bytes = 0;
                single_bytes = 0;
                while (true)
                {
                    single_bytes = file_listener.Receive(pass, pass_size, SocketFlags.None);
                    foreach (Socket thissocket in socketlist)
                    {
                        if(thissocket != file_listener)
                        {
                            thissocket.Send(pass);
                        }
                    }
                    total_bytes = total_bytes + single_bytes;
                    if (single_bytes < pass_size)
                    {
                        break;
                    }
                }
            }
            interrupt.Set();
        }

        private void submit_button_Click(object sender, EventArgs e)
        {
            String newrecord = thisname + ":\n" + enter_TextBox.Text;
            byte[] message = Encoding.UTF8.GetBytes(newrecord);
            //Console.WriteLine(newrecord);
            Console.WriteLine(Encoding.UTF8.GetString(message, 0, message.Length));
            /*if (message.Length > 1200)
            {
                byte[][] message2 = new byte[1200][(int)(message.Length / 1200) + 1];
            }*/
            foreach(Socket thissocket in socketlist)
            {
                thissocket.BeginSend(message, 0, message.Length, SocketFlags.None, result =>
                {
                    int length = thissocket.EndSend(result);
                }, null);

                ListViewItem item = new ListViewItem();
                item.Text = newrecord;
                item.BackColor = Color.AliceBlue;
                enter_TextBox.Text = "";
                chatrecord_listView.Items.Add(item);
            }
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
            Thread invokeThread = new Thread(new ThreadStart(InvokeMethod));
            invokeThread.SetApartmentState(ApartmentState.STA);
            invokeThread.Start();
            invokeThread.Join();
            if (dialogresult == DialogResult.OK)
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
                foreach (Socket thissocket in socketlist)
                {
                    thissocket.Send(Encoding.UTF8.GetBytes("--file--" + filename2));
                    //thissocket.Send(Encoding.UTF8.GetBytes(filename2+' '));
                    thissocket.SendFile(filename, null, null, TransmitFileOptions.UseDefaultWorkerThread);    
                }
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
            /*if (chatting == true)
            {
                thissocket.Send(Encoding.UTF8.GetBytes("--end---"));
            }*/

            //thissocket.Shutdown(SocketShutdown.Both);
        }
    }
}
