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
    public GameObject switchPrefab;
    public GameObject popup;

    public bool isDrawing;
    private MeshRenderer lightMeshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        popup.SetActive(false);
        lightMeshRenderer = Light_01.GetComponent<MeshRenderer>();
        FalconIsFinish();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) {
            FalconIsFinish();
        }

        if (!isDrawing)
        {
            lightMeshRenderer.material.SetColor("_EmissionColor", Color.red*Mathf.Clamp(Mathf.Cos(Time.time*20),0,1)*10);

        }
        else
        {
            //lightMeshRenderer.material.SetColor("_EmissionColor", Color.yellow * Mathf.Clamp(Mathf.Cos(Time.time * 5), 0, 1) * 10);

        }
    }

    public void FalconIsFinish()
    {
        Debug.Log("FalconIsFinish");
        popup.SetActive(true);
        //switchPrefab.GetComponent<Outline>().enabled = true;
        switchPrefab.GetComponent<Outline>().OutlineColor = Color.green;

        isDrawing = false;
        lightMeshRenderer.material.color = Color.red;
        lightMeshRenderer.material.SetColor("_EmissionColor", Color.red);

        canFalconRestart = true;
        sendButton.SetActive(true);
        switchButton.transform.localRotation = Quaternion.Euler(45, -90, 0);


    }

    public void RestartFalcon()
    {
        //switchPrefab.GetComponent<Outline>().enabled = false;
        switchPrefab.GetComponent<Outline>().OutlineColor = Color.red;

        isDrawing = true;

        Debug.Log("RestartFalcon");
        lightMeshRenderer.material.color = Color.yellow;
        lightMeshRenderer.material.SetColor("_EmissionColor", Color.yellow);

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


