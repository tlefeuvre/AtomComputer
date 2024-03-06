using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FalconState : MonoBehaviour
{

    public TMP_Text text;
    private static FalconState instance = null;
    public static FalconState Instance => instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

        // Initialisation du Game Manager...
    }
    private void Update()
    {
        transform.SetAsFirstSibling();
    }

    public void SetFalconState(bool state)
    {
        if(state)
        {
            WindowManager.instance.HideHiddenFiles();
            text.text = "Falcon activé";
            text.color = Color.white;
        }
        else
        {
            WindowManager.instance.DisplayHiddenFiles();
            text.text = "Falcon désactivé";
            text.color = Color.red;


        }
    }
}
