using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ImageFileManager : MonoBehaviour
{
    public Image image;
    public TMP_Text imageName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetParameters(Sprite img, string name)
    {
        image.sprite = img;
        imageName.text = name;
    }
    public void Close()
    {
        WindowManager.instance.GoBack();
    }
}
