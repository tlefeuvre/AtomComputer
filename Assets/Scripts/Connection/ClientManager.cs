using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using TMPro;
using UnityEngine;


public class ClientManager : MonoBehaviour
{
    public static ClientManager instance;

    public String Host = "localhost";
    public Int32 Port = 55000;
    public bool connectOnAwake;

    TcpClient mySocket = null;
    NetworkStream theStream = null;

    // Start is called before the first frame update
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        instance = this;
        mySocket = new TcpClient();
        if(connectOnAwake)
            SetupSocket();
    }

    private void Update()
    {
        if (!mySocket.Connected)
        {
            //SetupSocket();
        }
        else
        {
            ProcessMessage();
        }
    }

    public void SetupSocket()
    {
        Debug.Log("Trying to connect to : "+Host+":"+Port+" ...");
        try
        {
            mySocket.Connect(Host, Port);
            //Debug.Log(" - "+mySocket.Available);
            SendTcpMessage("3;1;1");
        }
        catch (Exception e)
        {
            Debug.Log("Socket error: " + e);
        }
        Debug.Log("Connected!");
    }

    private void ProcessMessage()
    {
        theStream = mySocket.GetStream();

        if ((theStream != null) && (theStream.DataAvailable))
        {
            StreamReader reader = new StreamReader(theStream);
            string msg = reader.ReadLine();
            SynchronizeManager.RaiseSyncRequest(msg);
        }
    }

    public void SendTcpMessage(string text)
    {
        Debug.Log("Sending \""+text+"\" ...");
        if (!mySocket.Connected)
        {
            Debug.Log("Can't send, socket not connected.");
            return;
        }
        Byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(text + "\n");
        mySocket.GetStream().Write(sendBytes, 0, sendBytes.Length);
        Debug.Log("Sent !");
    }

    private void OnApplicationQuit()
    {
        if (mySocket != null && mySocket.Connected)
            mySocket.Close();
    }

    public void SetIp(string ip)
    {
        Host = ip;
    }

    public bool IsConnected()
    {
        return mySocket.Connected;
    }
}