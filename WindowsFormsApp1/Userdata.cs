using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace WindowsFormsApp1
{
    static class Userdata
    {
        static String username;
        static String password;
        public static String serverIP = "166.111.140.57";
        public static int serverPort = 8000;
        public static IPEndPoint serverPoint = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);

        static void setUserdata(String name, String pass)
        {
            username = name;
            password = pass;
        }

        public static String getServerIP()
        {
            return serverIP;
        }

        public static int getServerPort()
        {
            return serverPort;
        } 
    }
}
