using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;



public class TestSync : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI feedbackText;

    public UnityEvent ReconnectedFalcon;
    public UnityEvent f1, f2, f3, f4, f5;

    public UnityEvent WrongMessage;
    public UnityEvent GoodMessage;
    private void OnEnable()
    {
        SynchronizeManager.OnSyncRequest += ProcessCode;
    }

    private void OnDisable()
    {
        SynchronizeManager.OnSyncRequest -= ProcessCode;
    }

    public void ProcessCode(string msg)
    {
        int code;
        if (!int.TryParse(msg.Split(';')[0], out code))
            return;

        switch (code)
        {
            case 0:
                GoodMessage.Invoke();
                break;
            case 1: //start draw
                WrongMessage.Invoke();
                break;
           
        }
    }
}
