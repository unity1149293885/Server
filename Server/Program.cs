using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Server
{
    public class Program
    {
        private static List<Client> ClientList = new List<Client>();
        private static List<Client> notClientList = new List<Client>();
        
        static void Main(string[] args)
        {
            string ipAddress;
            if (ConnectInfo.type == IpType.Cyou)
            {
                ipAddress = ConnectInfo.ipAddressCyou;
            }
            else
            {
                ipAddress = ConnectInfo.ipAddressHome;
            }
            Socket TcpServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//这句话是申请一个新的套接字，作用在InterNet网络，也就是因特网，类型是流式套接字，还有数据报类型等等，使用的协议是TCP协议
            TcpServer.Bind(new IPEndPoint(IPAddress.Parse(ipAddress), ConnectInfo.Port));//这一句话用于将套接字绑定在“当前主机IP的第7788号端口之上”这里的ip需要自己查阅
            TcpServer.Listen(100);
            Console.WriteLine("服务器已开启 Ip:"+ ipAddress+" 端口号："+ ConnectInfo.Port);

            //连接数据库
            MySqlTools.ConnectMySql();
            
            //更新玩家信息
            MySqlTools.UpdateAllUserInfo();

            //更新产品信息
            MySqlTools.GetAllDownItem(false);

            while (true)//服务端一旦开启就会一直运行下去
            {
                Socket socket = TcpServer.Accept();//Accept用于等待客户端接入，如果没有客户端接入就会一直等待
                Client client = new Client(socket);
                ClientList.Add(client);//用一个客户端的列表，来表示所有的客户端
            }
        }
    }
}
