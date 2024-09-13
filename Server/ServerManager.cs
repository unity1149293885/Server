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

namespace Server
{
    internal class Client
    {
        private readonly Thread Accept;
        private readonly Socket ClientSocket;

        private readonly byte[] data = new byte[1024];

        public Client(Socket socket)
        {
            ClientSocket = socket;

            Accept = new Thread(ReceiveMessage);//每一个客户端生成，都新开个线程
            Console.WriteLine("连接一个客户端 客户端uid：");
            Accept.Start();
        }

        public bool connected => ClientSocket.Connected;

        //事件的接受
        public void ReceiveMessage()
        {
            try
            {
                while (true)
                {
                    if (ClientSocket.Poll(10, SelectMode.SelectRead) || !ClientSocket.Connected)
                    {
                        ClientSocket.Close();
                        Console.WriteLine("客户端关闭连接");
                        break;
                    }
                    
                    //接收协议编号
                    byte[] ProtocolNumber_byte = new byte[8];
                    ClientSocket.Receive(ProtocolNumber_byte, 0, ProtocolNumber_byte.Length, SocketFlags.None);
                    long ProtocolNumber = BitConverter.ToInt64(ProtocolNumber_byte, 0);

                    //接收数据大小
                    byte[] ClientDataLength = new byte[8];
                    ClientSocket.Receive(ClientDataLength, 0, ClientDataLength.Length, SocketFlags.None);
                    long ClientDataSize = BitConverter.ToInt64(ClientDataLength, 0);

                    byte[] ClientData = new byte[ClientDataSize];
                    if (ClientDataSize > 0)
                    {
                        ClientSocket.Receive(ClientData, 0, ClientData.Length, SocketFlags.None);
                    }
                    else
                    {
                        ClientData = new byte[0];
                    }

                    string content = Encoding.UTF8.GetString(ClientData, 0, (int)ClientDataSize);

                    Console.WriteLine("协议号：" + ProtocolNumber + "  包体内容：" + content);

                    long ServertoClientProtocolNumber = ProtocolNumber + 1;
                    string ProtocolName = "Protocol_" + ServertoClientProtocolNumber;
                    Protocol p = new Protocol();
                    Type T = p.GetType();
                    MethodInfo ProtocolMethodInfo = T.GetMethod(ProtocolName);
                    if (ProtocolMethodInfo == null)
                    {
                        ProtocolMethodInfo = T.GetMethod("Protocol_0");
                    }

                    var type = ProtocolMethodInfo.ReturnType;

                    object[] ProtocolParamerter = { ClientData };
                    object ServerData_object = ProtocolMethodInfo.Invoke(null, ProtocolParamerter);

                    byte[] ServerData = new byte[0];
                    if (ServerData_object != null)
                    {
                        string jsonstr = JsonConvert.SerializeObject(ServerData_object);
                        ServerData = Encoding.UTF8.GetBytes(jsonstr);
                    }

                    SendtoClient(ServertoClientProtocolNumber, ServerData);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("报错信息：" + e);
            }
        }

        public void SendtoClient(long ProtocolNumber, byte[] serverData)//服务端-》客户端
        {
            var ServerData = serverData; //字节类型的内容

            //包装数据大小
            byte[] ClientDataLength = BitConverter.GetBytes((long)ServerData.Length);
            ServerData = ClientDataLength.Concat(ServerData).ToArray();
            //包装协议编号
            byte[] ProtocolNumber_byte = BitConverter.GetBytes(ProtocolNumber);
            ServerData = ProtocolNumber_byte.Concat(ServerData).ToArray();

            ClientSocket.Send(ServerData, 0, ServerData.Length, SocketFlags.None);
        }

    }
}
