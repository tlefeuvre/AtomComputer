using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ModifyScriptable : MonoBehaviour
{
    public List<string> foldersNames;
    public Architecture architecture;

    public List<FileTemplate> filesToRead = new List<FileTemplate>();

    public static ModifyScriptable instance;

    public GameObject unlockPopup;
    public TMP_Text unlockPopupText;
    private void Awake()
    {
        instance = this;
     
        filesToRead.Add(architecture.filesList[0]);
        ReadScriptable(architecture.filesList[0]);

        unlockPopup.SetActive(false);


    }
    private void ReadScriptable(FileTemplate file)
    {
        Debug.Log("file:" + file.name);
        foreach (FileTemplate child in file.childs)
        {
            Debug.Log("child:" + child.name);
            filesToRead.Add(child);
            if (child.type == FileType.FOLDER || child.type == FileType.LOCK)
                ReadScriptable(child);
            // on check si le fichier est dans la liste des fichiers a locks
            if (System.Array.IndexOf(foldersNames.ToArray(), child.name) != -1)
            {
                child.isLock = true;
                child.type = FileType.LOCK;
            }
        }
    }

    private void UnlockFolder(FileTemplate file,string folderName)
    {
        //Debug.Log("file:" + file.name);
        foreach (FileTemplate child in file.childs)
        {
            //Debug.Log("child:" + child.name);
            if(child.type == FileType.FOLDER || child.type == FileType.LOCK)
                UnlockFolder(child, folderName);

            // on check si le fichier est dans la liste des fichiers a locks
            if (folderName== child.name)
            {
                Debug.Log("DELOCKAGE DE :" + child.name);

                child.isLock = false;
                child.type = FileType.FOLDER;
            }
        }

        WindowManager.instance.NewWindow();
    }

    public void GetUnlockFolderName(string folderName)
    {
        UnlockFolder(architecture.filesList[0], folderName);
        unlockPopupText.text = folderName + " ouvert";
        unlockPopup.SetActive(true);
        StartCoroutine("closeUnlockPopup");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator closeUnlockPopup()
    {

        yield return new WaitForSeconds(4.0f);
        unlockPopup.SetActive(false);
    }
}



/*
 if( (child.type == FileType.FOLDER || child.type == FileType.LOCK) && child.isLock)
                {

                }
                else if ((child.type == FileType.FOLDER || child.type == FileType.LOCK) && !child.isLock)
                {

                }
 
 */