using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFileManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text fileName;
    public TMP_Text fileContent;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetParameters(string name, string content)
    {
        fileContent.text = content;
        fileName.text = name;
    }

    public void Close()
    {
        WindowManager.instance.GoBack();
    }
}

