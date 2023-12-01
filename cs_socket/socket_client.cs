byte[] imageBytes = File.ReadAllBytes("E:/download/pra/C#/searching.png");

IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
IPEndPoint remoteEP = new IPEndPoint(ipAddress, 4323);
Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
sender.Connect(remoteEP);
while(sender.Connected) {
    sender.Send(imageBytes);
}
