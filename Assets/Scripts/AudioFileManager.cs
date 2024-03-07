using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AudioFileManager : MonoBehaviour
{
    public TMP_Text fileName;
    public TMP_Text audioLenght;
    public GameObject timeBar;
    public GameObject audioWaves;

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
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Close();
        }
    }
    public void Close()
    {
        WindowManager.instance.GoBack();
    }
    public void SetParameters(string name, float clipLenght, AudioClip clip, int id, bool sendable)
    {
        fileName.text = name;
        audioLenght.text = "Durée: " + clipLenght.ToString()+"s";
        timeBar.GetComponent<ProgressBar>().timeToFill = clipLenght;
        audioWaves.GetComponent<Visualizer>().audioClip = clip;

        idToSend = id;
        isSendable = sendable;
        toSend.SetActive(isSendable);


    }

    public void sendId()
    {
        sentPopup.SetActive(true);
        StartCoroutine("desactivatepopup");

        ServerHandler.Instance.SendMessage(idToSend);


    }
    IEnumerator desactivatepopup()
    {
        yield return new WaitForSeconds(1);
        sentPopup.SetActive(false);
    }

}
