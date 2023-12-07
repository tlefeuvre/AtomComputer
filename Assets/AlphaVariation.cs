using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaVariation : MonoBehaviour
{
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();  
    }

    // Update is called once per frame
    void Update()
    {
        var tempColor = image.color;
        tempColor.a = Mathf.Clamp(Mathf.Sin(Time.time*5),.5f,1f);
        image.color = tempColor;
    }
}
