using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AudioFileManager : MonoBehaviour
{
    public TMP_Text fileName;
    public TMP_Text audioLenght;
    public GameObject timeBar;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Close()
    {
        WindowManager.instance.GoBack();
    }
    public void SetParameters(string name, float clipLenght)
    {
        fileName.text = name;
        audioLenght.text = "Durée: " + clipLenght.ToString()+"s";
        timeBar.GetComponent<ProgressBar>().timeToFill = clipLenght;

    }
}
