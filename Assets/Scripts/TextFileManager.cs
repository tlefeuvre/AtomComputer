using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFileManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text fileName;
    public TMP_Text fileContent;
    public GameObject toSend;
    public bool isSendable;
    public int idToSend;

    public GameObject sentPopup;

    void Start()
    {
        if(sentPopup)
            sentPopup.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Close();
        }
    }

    public void SetParameters(string name, string content, int id, bool sendable)
    {
        Debug.Log("set parameters text file");
        fileContent.text = content;
        fileName.text = name;
        idToSend = id;
        isSendable = sendable;
        toSend.SetActive(isSendable);
    }

    public void Close()
    {
        WindowManager.instance.GoBack();
    }
    public void sendId()
    {
        if(sentPopup)
            sentPopup.SetActive(true);
        StartCoroutine("desactivatepopup");
        ClientManager.instance.SendMessage(idToSend);
    }

    IEnumerator desactivatepopup()
    {
        yield return new WaitForSeconds(.5f);
        if (sentPopup)
            sentPopup.SetActive(false);
    }
    

}

