using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadinScreen : MonoBehaviour
{

    public bool startLoading;
    public Image loadingImage;
    public float width;

    private float widthPerS = 200;
    public AudioSource audioSource;
    public AudioClip audioClip;


    private void OnEnable()
    {
        width = 0;
        startLoading = true;
        audioSource.clip = audioClip;
        audioSource.Play();
    }


    // Update is called once per frame
    void Update()
    {
        if (startLoading)
        {

            width += (widthPerS * Time.deltaTime);
            loadingImage.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 40);

            if (width > 400)
            {
                startLoading = false;
                width = 0;
                gameObject.SetActive(false);
            }
        }
       
    }


}
