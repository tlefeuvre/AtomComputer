using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.VersionControl;
public class FalconState : MonoBehaviour
{

    public TMP_Text falconStateText;
    public TMP_Text messageStateText;

    private static FalconState instance = null;
    public static FalconState Instance => instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

        // Initialisation du Game Manager...
    }
    private void Update()
    {
        transform.SetAsFirstSibling();
    }

    private void Start()
    {
        messageStateText.text = "";
    }

    public void SetFalconState(bool state)
    {
        if(state)
        {
            WindowManager.instance.HideHiddenFiles();
            falconStateText.text = "Falcon activated";
            falconStateText.color = Color.white;
        }
        else
        {
            WindowManager.instance.DisplayHiddenFiles();
            falconStateText.text = "Falcon desactivated";
            falconStateText.color = Color.red;


        }
    }
    public void WrongMessageRestart()
    {
        messageStateText.text = "Incorrect message received. Replace the sheet to start again.";
        falconStateText.color = Color.red;

    }
    public void GoodMessage()
    {
        messageStateText.text = "Message received.Move sheet to next level.";
        falconStateText.color = Color.green;


    }
}
