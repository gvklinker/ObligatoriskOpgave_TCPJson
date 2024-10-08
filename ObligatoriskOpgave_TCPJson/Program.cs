﻿using ObligatoriskOpgave_TCPJson;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text.Json;

Console.WriteLine("TCP Client");
TcpListener listener = new TcpListener(IPAddress.Any, 21);
listener.Start();
while (true)
{
    TcpClient socket = listener.AcceptTcpClient();
    IPEndPoint ep = socket.Client.RemoteEndPoint as IPEndPoint;
    Console.WriteLine("Client connected: " + ep.Address);
    Task.Run(() => HandleClient(socket));
}

void HandleClient(TcpClient socket)
{
    NetworkStream ns = socket.GetStream();
    StreamReader sr = new StreamReader(ns);
    StreamWriter sw = new StreamWriter(ns);
    sw.WriteLine("Welcome");
    while (socket.Connected)
    {
        sw.WriteLine("Pick method: Add, Subtract or Random");
        sw.WriteLine("ex: {\"Method\": \"Add\", \"num1\": 11, \"num2\": 6}");
        sw.Flush();
        string message = sr.ReadLine();
        JSONObject jOb = JsonSerializer.Deserialize<JSONObject>(message);
        switch (jOb.Method.ToLower())
        {
            case "random": sw.WriteLine(RandomNumberGenerator.GetInt32(jOb.num1, jOb.num2)); break;
            case "add": sw.WriteLine(jOb.num1 + jOb.num2); break;
            case "subtract": sw.WriteLine(jOb.num1 - jOb.num2); break;
            default: sw.WriteLine("protocol error"); break;
        }
        sw.Flush();
    }
}