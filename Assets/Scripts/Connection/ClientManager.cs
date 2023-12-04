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

    TcpClient mySocket = null;
    NetworkStream theStream = null;



    public GameObject buttonconnect;
    public GameObject textip;
    public GameObject textport;

    // Start is called before the first frame update
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        instance = this;
        mySocket = new TcpClient();
        //SetupSocket();
    }
    public void SetHostIp(string ip)
    {
        Host = ip;
    }
    public void SetHostPort(string port)
    {
        Port = int.Parse(port);
    }
    private void Update()
    {
        if (!mySocket.Connected)
        {
            //SetupSocket();
        }
        else
        {
            buttonconnect.SetActive(false);
            textip.SetActive(false);
            textport.SetActive(false);

            ProcessMessage();
        }
    }

    public void SetupSocket()
    {
        Debug.Log(Host);
        Debug.Log(Port);
        try
        {
            mySocket.Connect(Host, Port);
        }
        catch (Exception e)
        {
            Debug.Log("Socket error: " + e);
        }
    }

    private void ProcessMessage()
    {
        theStream = mySocket.GetStream();

        if ((theStream != null) && (theStream.DataAvailable))
        {
            StreamReader reader = new StreamReader(theStream);
            string msg = reader.ReadLine();

            int code;
            if (int.TryParse(msg, out code))
            {
                SynchronizeManager.RaiseSyncRequest(code);
            }
        }
    }

    public void SendMessage(int text)
    {
        Byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(text + "\n");
        mySocket.GetStream().Write(sendBytes, 0, sendBytes.Length);
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