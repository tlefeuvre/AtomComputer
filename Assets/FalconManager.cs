using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalconManager : MonoBehaviour
{
    public GameObject Light_01;
    public GameObject Light_02;
    public GameObject sendButton;
    public GameObject switchButton;
    public bool canFalconRestart;

    public GameObject popup;

   
    // Start is called before the first frame update
    void Start()
    {
        popup.SetActive(false);

        FalconIsFinish();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) {
            FalconIsFinish();
        }
    }

    public void FalconIsFinish()
    {
        Debug.Log("FalconIsFinish");
        popup.SetActive(true);

        Light_01.GetComponent<MeshRenderer>().material.color = Color.red;
        Light_01.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.red);

        canFalconRestart = true;
        sendButton.SetActive(true);
        switchButton.transform.localRotation = Quaternion.Euler(45, -90, 0);


    }

    public void RestartFalcon()
    {
        Debug.Log("RestartFalcon");
        Light_01.GetComponent<MeshRenderer>().material.color = Color.yellow;
        Light_01.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.yellow);

        canFalconRestart = false;
        sendButton.SetActive(false);
        popup.SetActive(false);

        switchButton.transform.localRotation = Quaternion.Euler( 135, -90, 0);

    }
    private IEnumerator popupAnimator()
    {
        popup.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        popup.SetActive(false);

    }
}


