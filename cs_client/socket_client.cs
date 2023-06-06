using System.Net.Sockets;

const string Hostname = "localhost";
const string UnixSocketPath = "/tmp/foo.sock";

using var socket = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.IP);

var endpoint = new UnixDomainSocketEndPoint(UnixSocketPath);
socket.Connect(endpoint);

var requestBytes = System.Text.Encoding.UTF8.GetBytes($"GET / HTTP/1.0\r\nHost: {Hostname}\r\nAccept: */*\r\n\r\n");
socket.Send(requestBytes);

byte[] receivedBytes = new byte[1024];
socket.Receive(receivedBytes, 1024, SocketFlags.None);

Console.WriteLine(System.Text.Encoding.UTF8.GetString(receivedBytes));