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
    private int listIndex;
    private string passwordText;
    private FileType fileType;
    private bool isLock;

    public string fileName;
    void Start()
    {
        isLock = true;
       
        passwordObject.SetActive(false);
        warningObject.SetActive(false);
    }


    public void OpenFile()
    {
        if(fileType == FileType.LOCK && isLock)
        {
            warningObject.transform.SetParent(transform.parent.parent.parent, false);
            warningObject.transform.localPosition = new Vector3 (0.0f, warningObject.transform.localPosition.y, warningObject.transform.localPosition.z);
            warningObject.SetActive(true);
            StartCoroutine("closeWarning");

            if(fileName == "DOSSIER 2")
                ClientManager.instance.SendMessage(11);

            if (fileName == "DOSSIER 3")
                ClientManager.instance.SendMessage(12);
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
