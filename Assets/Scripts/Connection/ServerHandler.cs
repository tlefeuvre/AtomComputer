using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using System.Text;
using System;
using System.IO;

public enum ConnectionType
{
    SERVER, 
    CLIENT
}

public class ServerHandler : MonoBehaviour
{
    public String Host = "localhost";
    public Int32 Port = 55000;
    public int codeToSend;

    private TcpListener listener = null;
    private TcpClient client = null;
    private NetworkStream ns = null;

    // Start is called before the first frame update
    void Awake()
    {
        listener = new TcpListener(Dns.GetHostEntry(Host).AddressList[1], Port);
        listener.Start();
        Debug.Log("is listening");

        if (listener.Pending())
        {
            client = listener.AcceptTcpClient();
            Debug.Log("Connected");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (client == null)
        {
            if (listener.Pending())
            {
                client = listener.AcceptTcpClient();
                Debug.Log("Connected");
            }
            else
            {
                return;
            }
        }
        else
        {
            ProcessMessage();
            if (Input.GetKeyDown(KeyCode.S))
            {
                SendMessage(codeToSend);
            }
        }
    }

    private void ProcessMessage()
    {
        ns = client.GetStream();

        if((ns != null) && (ns.DataAvailable))
        {
            StreamReader reader = new StreamReader(ns);
            string msg = reader.ReadLine();
            Debug.Log(msg);

            int code;
            if(int.TryParse(msg, out code))
            {
                SynchronizeManager.RaiseSyncRequest(code);
            }
        }
    }

    private void SendMessage(int code)
    {
        Byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(code + "\n");
        Debug.Log("Sending : " + code);
        client.GetStream().Write(sendBytes, 0, sendBytes.Length);
        Debug.Log("Sent");
    }

    private void OnApplicationQuit()
    {
        if (listener != null)
            listener.Stop();
    }
}