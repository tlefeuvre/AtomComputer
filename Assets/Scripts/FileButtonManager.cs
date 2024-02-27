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
    public GameObject warningObject;
    public GameObject imageObject;

    public GameObject miniGame1;
    public int listIndex;
    private string passwordText;
    private FileType fileType;
    private bool isLock;

    public string fileName;
    void Start()
    {
        isLock = true;

        miniGame1.SetActive(false);
        passwordObject.SetActive(false);
        warningObject.SetActive(false);
    }


    public void OpenFile()
    {
        Debug.Log("openfile");
  
        if(fileType == FileType.LOCK && isLock)
        {
            if(fileName == ModifyScriptable.instance.foldersNames[0])
            {
                Debug.Log("set active mini game " + isLock);

                miniGame1.transform.SetParent(transform.parent.parent.parent, false);
                miniGame1.transform.localPosition = new Vector3(0, 100, 0);
                miniGame1.SetActive(true);

            }
            /*warningObject.transform.SetParent(transform.parent.parent.parent, false);
            warningObject.transform.localPosition = new Vector3 (0.0f, warningObject.transform.localPosition.y, warningObject.transform.localPosition.z);
            warningObject.SetActive(true);
            StartCoroutine("closeWarning");*/

            if(fileName == ModifyScriptable.instance.foldersNames[0])
                ClientManager.instance.SendMessage(12);

            if (fileName == ModifyScriptable.instance.foldersNames[1])
                ClientManager.instance.SendMessage(14);

            /*if (fileName == "DOSSIER 2")
                ClientManager.instance.SendMessage(12);

            if (fileName == "DOSSIER 3")
                ClientManager.instance.SendMessage(14);*/
        }
        if (fileType == FileType.SUCCESS   && isLock)
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
            miniGame1.SetActive(false);
            Debug.Log("nouvelle fenetre " + isLock);

            int id = WindowManager.instance.arborescence.Count - 1;
            WindowManager.instance.arborescence.Add(WindowManager.instance.arborescence[id].childs[listIndex]);
            WindowManager.instance.NewWindow();
        }
       
    }
    public void SetLock(bool setLock)
    {
        isLock = setLock;
    }
    public void SetParameters(Sprite image, int index, string name, FileType type, string password)
    {
        listIndex = index;
        imageObject.GetComponent<Image>().sprite = image;
        fileNameObject.GetComponent<TMP_Text>().text = name;
        fileName = name;
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

    IEnumerator closeWarning()
    {
        yield return new WaitForSeconds(1.0f);
        warningObject.SetActive(false);
    }
}
