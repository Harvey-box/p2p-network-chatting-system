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

namespace WindowsFormsApp1
{
    public partial class userwin : Form
    {
        IPAddress selfIP;
        String thisname;
        int lisport = 8000;
        int testlisport = 8003;
        int[] testlisport2 = { 8001, 8002, 8004 };

        public userwin(String name)
        {
            InitializeComponent();
            thisname = name;
            this.Text = thisname;
            userdata_listView.Columns.Add("username");
            userdata_listView.Columns.Add("state");
            userdata_listView.Columns.Add("IP");
            userdata_listView.Columns[0].Width = (int)(0.35 * userdata_listView.Width);
            userdata_listView.Columns[1].Width = (int)(0.2 * userdata_listView.Width);
            userdata_listView.Columns[2].Width = (int)(0.45 * userdata_listView.Width);

            TCP_concect.tcpClient.Send(Encoding.UTF8.GetBytes("q2017011519"));
            byte[] data_receive = new byte[1024];
            int length_receive = TCP_concect.tcpClient.Receive(data_receive);
            selfIP = IPAddress.Parse(Encoding.UTF8.GetString(data_receive, 0, length_receive));
            Console.WriteLine(selfIP.ToString());
            listen();
        }


        private void listen()               //監聽
        {
            if (TCP_concect.checkport(lisport))
            {
                MessageBox.Show("Listen port has been used!", "Error", MessageBoxButtons.OK);
            }
            IPEndPoint point = new IPEndPoint(selfIP, lisport);
            Socket newsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            newsocket.Bind(point);          //綁定IP和端口
            newsocket.Listen(20);           //最大連接數為20
            asynaccept(newsocket);
            Console.WriteLine("start_listen:");
        }

        private void asynaccept(Socket newsocket)
        {
            newsocket.BeginAccept(asyncResult =>                                       //每接收到信息就執行這段語句
            {
                Socket peer = newsocket.EndAccept(asyncResult);
                //chat_private p = new chat_private(peer);
                byte[] data_receive = new byte[1024];
                int length_receive = peer.Receive(data_receive);
                String message_receive = Encoding.UTF8.GetString(data_receive, 0, length_receive);
                Console.WriteLine(message_receive);
                string signal = "";
                string allname = "";
                if (length_receive > 8)
                {
                    for(int i=0; i<8; i++)
                    {
                        signal += message_receive[i];
                    }
                    for(int i=8; i<length_receive; i++)
                    {
                        allname += message_receive[i];
                    }
                }
                if(signal == "--group-")
                {
                    Socket[] allsocket = new Socket[1];
                    allsocket[0] = newsocket;
                    Thread th = new Thread(() => Application.Run(new chat_group(allname, allsocket,1, thisname)));
                    th.SetApartmentState(System.Threading.ApartmentState.STA);
                    th.Start();
                }
                else
                {
                    Thread th = new Thread(() => Application.Run(new chat_private(message_receive, peer)));
                    th.SetApartmentState(System.Threading.ApartmentState.STA);
                    th.Start();
                    /*chat_private p = new chat_private(message_receive, peer);
                    p.Show();*/
                }
                asynaccept(newsocket);
            }, null);
        }


        private void chat_private_button_Click(object sender, EventArgs e)          //發起私聊
        {
            bool check = false;
            String frd_name = "q" + frd_username_text.Text;
            TCP_concect.tcpClient.Send(Encoding.UTF8.GetBytes(frd_name));       //發送登入信息

            byte[] data_receive = new byte[1024];
            int length_receive = TCP_concect.tcpClient.Receive(data_receive);
            String message_receive = Encoding.UTF8.GetString(data_receive, 0, length_receive);
            Console.WriteLine(message_receive);

            if(message_receive == "Incorrect No." || message_receive == "Please send the correct message.")
            {
                MessageBox.Show("This username is not exist!", "Error", MessageBoxButtons.OK);
            }
            else
            {
                foreach (ListViewItem item1 in userdata_listView.Items)
                {
                    if (item1.Text == frd_username_text.Text)
                    {
                        check = true;
                        item1.SubItems.Clear();
                        item1.Text = frd_username_text.Text;
                        if (message_receive == "n")
                        {
                            item1.SubItems.Add("offline");
                            item1.SubItems.Add("/");
                        }
                        else
                        {
                            item1.SubItems.Add("online");
                            item1.SubItems.Add(message_receive);
                        }
                    }
                }

                if(check == false)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = frd_username_text.Text;
                    if (message_receive == "n")
                    {
                        item.SubItems.Add("offline");
                        item.SubItems.Add("/");
                    }
                    else
                    {
                        item.SubItems.Add("online");
                        item.SubItems.Add(message_receive);
                    }
                    userdata_listView.Items.Add(item);
                }
                frd_username_text.Text = "";
            }
        }

        private void group_chat_button_Click(object sender, EventArgs e)                //發起群聊
        {
            foreach (ListViewItem item in userdata_listView.Items)
            {
                Console.WriteLine(item.SubItems[1].Text);
                if (item.Selected == true && item.SubItems[1].Text != "online")
                {
                    MessageBox.Show("your friend is not online", "error", MessageBoxButtons.OK);
                    return;
                }
            }

            string allname = thisname;
            Socket[] allsocket = new Socket[userdata_listView.SelectedItems.Count];
            foreach(ListViewItem item in userdata_listView.SelectedItems)
            {
                allname = allname + " " + item.SubItems[0].Text;
            }

            int i = 0;
            IPAddress IP;
            IPEndPoint point;
            int number = userdata_listView.SelectedItems.Count;
            foreach (ListViewItem item in userdata_listView.SelectedItems)
            {
                IP = IPAddress.Parse(item.SubItems[2].Text);
                point = new IPEndPoint(IP, testlisport2[i]);
                Socket newsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                TCP_concect.tcp_connect(newsocket, point);
                newsocket.Send(Encoding.UTF8.GetBytes("--group-"+allname));
                allsocket[i] = newsocket;
                i++;
            }
            Thread server_client_connection = new Thread(() => Application.Run(new chat_group(allname, allsocket, number, thisname)));
            server_client_connection.SetApartmentState(System.Threading.ApartmentState.STA);//单线程监听控制
            server_client_connection.Start();
        }

        private void chat_button_Click(object sender, EventArgs e)          //找朋友
        {
            String name = userdata_listView.SelectedItems[0].Text;
            IPAddress IP = IPAddress.Parse(userdata_listView.SelectedItems[0].SubItems[2].Text);
/*            while(TCP_concect.checkport(port))
            {
                port++;
            }*/
            IPEndPoint point = new IPEndPoint(IP, testlisport);
            Socket socket_peer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            if (!TCP_concect.tcp_connect(socket_peer, point))
            {
                MessageBox.Show("Connect fail!", "Error", MessageBoxButtons.OK);
            }
            else
            {
                socket_peer.Send(Encoding.UTF8.GetBytes(thisname));
                Thread server_client_connection = new Thread(() => Application.Run(new chat_private(name, socket_peer)));
                server_client_connection.SetApartmentState(System.Threading.ApartmentState.STA);//单线程监听控制
                server_client_connection.Start();
                //chat_private p = new chat_private(name, socket_peer);
                //p.Show();
            }
            

            /*chat_private p = new chat_private(name, IP);
            p.Show();*/

            /*Console.WriteLine(userdata_listView.SelectedItems[0].Text);
            Console.WriteLine(userdata_listView.SelectedItems[0].SubItems[2].Text);*/
        }

        private void update_button_Click(object sender, EventArgs e)        //更新朋友狀態
        {
            String username;
            foreach(ListViewItem item in userdata_listView.Items)
            {
                TCP_concect.tcpClient.Send(Encoding.UTF8.GetBytes("q"+item.Text));
                byte[] data_receive = new byte[1024];
                int length_receive = TCP_concect.tcpClient.Receive(data_receive);
                String message_receive = Encoding.UTF8.GetString(data_receive, 0, length_receive);
                Console.WriteLine(message_receive);

                username = item.Text;
                item.SubItems.Clear();
                item.Text = username;
                if (message_receive == "n")
                {
                    item.SubItems.Add("offline");
                    item.SubItems.Add("/");
                }
                else
                {
                    item.SubItems.Add("online");
                    item.SubItems.Add(message_receive);
                }
            }
        }

        private void logout_button_Click(object sender, EventArgs e)
        {
            String message = "logout" + thisname;
            TCP_concect.tcpClient.Send(Encoding.UTF8.GetBytes(message));

            byte[] data_receive = new byte[1024];
            int length_receive = TCP_concect.tcpClient.Receive(data_receive);
            String message_receive = Encoding.UTF8.GetString(data_receive, 0, length_receive);
            Console.WriteLine(message_receive);
            if(message_receive == "loo")
            {
                MessageBox.Show("logout success", "Error", MessageBoxButtons.OK);
                Application.Exit();
            }
            else
            {
                MessageBox.Show("logout fail", "Error", MessageBoxButtons.OK);
            }
        }
    }
}
