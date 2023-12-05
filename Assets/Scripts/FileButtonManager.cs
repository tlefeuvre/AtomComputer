using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class FileButtonManager : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject fileNameObject;
    public GameObject passwordObject;
    public GameObject imageObject;
    private int listIndex;
    private string passwordText;
    private FileType fileType;
    private bool isLock;


    void Start()
    {
        isLock = true;
       
        passwordObject.SetActive(false);
        
    }


    public void OpenFile()
    {
      

        if ((fileType == FileType.SUCCESS  || fileType == FileType.LOCK) && isLock)
        {
            passwordObject.SetActive(true);
            if (string.Equals(passwordText, passwordObject.GetComponentInChildren<TMP_InputField>().text, StringComparison.OrdinalIgnoreCase))
                isLock = false;

            //if (passwordText == passwordObject.GetComponentInChildren<TMP_InputField>().text)
              //  isLock = false;
            
        }
        if(fileType == FileType.SUCCESS && !isLock)
        {
            WindowManager.instance.Victory();
            return;

        }

        if (isLock && fileType == FileType.SUCCESS)
            return;

        if (!isLock || fileType != FileType.LOCK )
        {
            int id = WindowManager.instance.arborescence.Count - 1;
            WindowManager.instance.arborescence.Add(WindowManager.instance.arborescence[id].childs[listIndex]);
            WindowManager.instance.NewWindow();
        }
       
    }

    public void SetParameters(Sprite image, int index, string name, FileType type, string password)
    {
        listIndex = index;
        imageObject.GetComponent<Image>().sprite = image;
        fileNameObject.GetComponent<TMP_Text>().text = name;
        fileType = type;
        passwordText = password;
    }
    public void Close()
    {
        WindowManager.instance.GoBack();
    }
    public void CloseInputFiled()
    {
        passwordObject.SetActive(false);
    }

}
