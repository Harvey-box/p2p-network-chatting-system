using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace WindowsFormsApp1
{
    static class TCP_concect
    {
        public static Socket tcpClient;
        public static bool tcp_connect()
        {
            tcpClient.Connect(Userdata.serverPoint);
            return check_connect(); 
        }

        public static bool tcp_connect(Socket newsocket, IPEndPoint point)
        {
            newsocket.Connect(point);
            return check_connect();
        }

        public static bool check_connect()
        {
            bool blockingState = tcpClient.Blocking;
            bool state;
            try
            {
                byte[] tmp = new byte[1];
                tcpClient.Blocking = false;         //設為非阻塞式
                tcpClient.Send(tmp, 0, 0);
                state = true;
            }
            catch(SocketException e)
            {
                if (e.NativeErrorCode.Equals(10035))        //數據包被阻止了
                {
                    state = true;
                }
                else
                {
                    state = false;
                }
            }
            finally
            {
                tcpClient.Blocking = blockingState;
            }
            return state;
        }

        public static bool check_connect(Socket thissocket)
        {
            bool blockingState = thissocket.Blocking;
            bool state;
            try
            {
                byte[] tmp = new byte[1];
                thissocket.Blocking = false;         //設為非阻塞式
                thissocket.Send(tmp, 0, 0);
                state = true;
            }
            catch (SocketException e)
            {
                if (e.NativeErrorCode.Equals(10035))        //數據包被阻止了
                {
                    state = true;
                }
                else
                {
                    state = false;
                }
            }
            finally
            {
                thissocket.Blocking = blockingState;
            }
            return state;
        }

        public static bool checkport(int port)
        {
            IPGlobalProperties thisuser = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] allpoints = thisuser.GetActiveTcpListeners();
            foreach(IPEndPoint point in allpoints)          //遍歷所有TCP連接
            {
                if(point.Port == port)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
