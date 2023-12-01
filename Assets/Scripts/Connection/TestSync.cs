using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TestSync : MonoBehaviour
{

    //[SerializeField]
    // private TextMeshProUGUI feedbackText;
    [SerializeField]
    private Image feedback;

    public UnityEvent FalconDone;


    private void OnEnable()
    {
        SynchronizeManager.OnSyncRequest += ProcessCode;
    }

    private void OnDisable()
    {
        SynchronizeManager.OnSyncRequest -= ProcessCode;
    }

    public void ProcessCode(int code)
    {
        Debug.Log(code);
        switch (code)
        {
           
            case 0:
                //feedback.color = Color.white;
                //FalconDone.Invoke();
               //feedbackText.text = "0"; 
                break;
            case 1:
                FalconDone.Invoke();

                //feedback.color = Color.red;
               // feedbackText.text = "1"; 
                break;
            case 2:
                //feedback.color = Color.yellow;
                //feedbackText.text = "2";
                break;
        }
    }
}
