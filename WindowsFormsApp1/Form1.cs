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
    public partial class Form1 : Form
    {
        public Form1()
        {
            TCP_concect.tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            if(TCP_concect.tcp_connect() == false)
            {
                MessageBox.Show("Connect fail!", "Error", MessageBoxButtons.OK);
            }
            InitializeComponent();
        }

        private void login_button_Click(object sender, EventArgs e)
        {
            String username = username_text.Text;
            String password = password_text.Text;
            String userdata = username + "_" + password;
            TCP_concect.tcpClient.Send(Encoding.UTF8.GetBytes(userdata));       //發送登入信息

            byte[] data_receive = new byte[1024];
            int length_receive = TCP_concect.tcpClient.Receive(data_receive);
            String message_receive = Encoding.UTF8.GetString(data_receive, 0, length_receive);
            //Console.WriteLine(message_receive);
            if (message_receive == "lol")
            {
                this.Hide();
                userwin p = new userwin(username);
                p.Show();
            }
            else
            {
                MessageBox.Show("Your username or password is wrong!", "Error", MessageBoxButtons.OK);
            }
        }

    }
}
