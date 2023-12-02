//byte[] imageBytes = File.ReadAllBytes("C:/Users/zhouliguo/Desktop/images/bmw1.png");

//IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
//IPEndPoint remoteEP = new IPEndPoint(ipAddress, 4323);
//Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//sender.Connect(remoteEP);
//while(sender.Connected) {
//    sender.Send(imageBytes);
//}

// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenCvSharp;
using System.Runtime.InteropServices;

namespace Client {
    class Client {
        private static string s;

        static void change(){
            s="asdasdas";
            Console.WriteLine(s);
        }
        
        static void Main(string[] args) {
            //IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            //IPEndPoint remoteEP = new IPEndPoint(ipAddress, 4323);
            /*
            Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Udp);
            string path =  "/tmp/foo.sock";
            UnixDomainSocketEndPoint endPoint = new UnixDomainSocketEndPoint(path);
            sender.Connect(endPoint);
            object? buffsize = sender.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer);
            Console.WriteLine(buffsize);

            // 发送数据
            string message = "Hello from client!";
            byte[] msg = Encoding.ASCII.GetBytes(message);
            int bytesSent = sender.Send(msg);
            Console.WriteLine($"Sent: {message}");

            // 接受数据
            byte[] buffer = new byte[1024];
            int bytesRec = sender.Receive(buffer);
            string data = Encoding.ASCII.GetString(buffer, 0, bytesRec);
            Console.WriteLine($"Received: {data}");

            // 关闭连接
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();

            Console.ReadKey();
            */ 
            VideoCapture capture;
            capture = new VideoCapture();
            capture.Open(0);

            int length = 640 * 480 * 3;
            byte[] bytes = new byte[length];

            //GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            //handle.Free();

            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 4323);
            Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sender.Connect(remoteEP);
            while(!sender.Connected) {
                continue;
            }

            while(true){
                Mat image = capture.RetrieveMat();
                Cv2.ImShow("client", image);
                Cv2.WaitKey(1);
                Marshal.Copy(image.Data, bytes, 0, length);
                
                //bool success = image.GetArray(out Vec3b[] data);
                
                //Console.WriteLine(bytes.Length);
                //Console.WriteLine(image.Rows);
                //Console.WriteLine(image.Cols);
                //Console.WriteLine(image.Channels());

                sender.Send(bytes);
            }
        }
    }
}
