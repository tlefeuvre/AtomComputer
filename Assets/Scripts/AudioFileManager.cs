using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AudioFileManager : MonoBehaviour
{
    public TMP_Text fileName;

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
    public void SetParameters(string name)
    {
        fileName.text = name;
    }
}
