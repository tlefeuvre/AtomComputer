using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ImageFileManager : MonoBehaviour
{
    public Image image;
    public TMP_Text imageName;

    public GameObject toSend;
    public bool isSendable;
    public int idToSend;
    public GameObject sentPopup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Close();
        }
    }

    public void SetParameters(Sprite img, string name, int id, bool sendable)
    {
        image.sprite = img;
        imageName.text = name;

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
        sentPopup.SetActive(true);
        StartCoroutine("desactivatepopup");

        ClientManager.instance.SendMessage(idToSend);


    }
    IEnumerator desactivatepopup()
    {
        yield return new WaitForSeconds(1);
        sentPopup.SetActive(false);
    }

}
