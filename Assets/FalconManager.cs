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

   
    // Start is called before the first frame update
    void Start()
    {
        RestartFalcon();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FalconIsFinish()
    {
        Debug.Log("FalconIsFinish");
        Light_01.GetComponent<MeshRenderer>().material.color = Color.green;
        Light_01.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.green);

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
        switchButton.transform.localRotation = Quaternion.Euler( 135, -90, 0);

    }
}
