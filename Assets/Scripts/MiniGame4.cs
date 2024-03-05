using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame4 : MonoBehaviour
{
    public GameObject gameScene;
    public GameObject loadingScene;

    public bool startLoading;
    public Image loadingImage;
    public float width;

    private float widthPerS = 200;
    // Start is called before the first frame update
    void Start()
    {
        loadingScene.SetActive(false);
        gameScene.SetActive(true);
        width = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (startLoading)
        {
            loadingScene.SetActive(true);
            gameScene.SetActive(false);
            width += (widthPerS * Time.deltaTime);
            loadingImage.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 50);

            if(width > 400)
            {
                startLoading = false;
                width = 0;
            }
        }
        else
        {
            loadingScene.SetActive(false);
            gameScene.SetActive(true);

        }
    }

    public void StartLoading()
    {
        startLoading = true;
    }

}
