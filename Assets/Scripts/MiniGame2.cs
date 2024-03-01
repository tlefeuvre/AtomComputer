using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

public class MiniGame2 : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public List<GameObject> UIPoints = new List<GameObject>();

    public List<GameObject> correctOrder = new List<GameObject>();

    public GameObject placeHoldersParent;

    private bool isUnlocked = false;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.positionCount = UIPoints.Count;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < UIPoints.Count; i++)
        {
            Vector3 newPos = UIPoints[i].transform.position * 102.4f;
            
            lineRenderer.SetPosition(i, newPos);
        }
        bool isOk = CheckOrder();
        ChangeColor(isOk);
        if(isOk && !isUnlocked)
        {
            isUnlocked = true;
            StartCoroutine("IsSuccess");
        }
    }

    IEnumerator IsSuccess()
    {
        yield return new WaitForSeconds(1.0f);
        ModifyScriptable.instance.GetUnlockFolderName(ModifyScriptable.instance.foldersNames[1]);

        GameObject manager = GameObject.FindGameObjectWithTag("FilesManager");
        for (int i = 0; i < manager.transform.childCount; i++)
        {
            if (manager.transform.GetChild(i).gameObject.GetComponent<FileButtonManager>())
            {

                if (manager.transform.GetChild(i).gameObject.GetComponent<FileButtonManager>().fileName == ModifyScriptable.instance.foldersNames[1])
                {
                    int id = WindowManager.instance.arborescence.Count - 1;
                    WindowManager.instance.arborescence.Add(WindowManager.instance.arborescence[id].childs[manager.transform.GetChild(i).gameObject.GetComponent<FileButtonManager>().listIndex]);
                    WindowManager.instance.NewWindow();
                    gameObject.SetActive(false);
                    break;
                }
            }
        }
    }

    public bool CheckOrder()
    {
        bool isOk = true;
        bool isRegularOrder = true;

        if (GameObject.ReferenceEquals(correctOrder[0].transform.GetChild(0).gameObject, UIPoints[0]))
            isRegularOrder = true;

        else if (GameObject.ReferenceEquals(correctOrder[0].transform.GetChild(0).gameObject, UIPoints[UIPoints.Count - 1]))
            isRegularOrder = false;
        else
            return false;

        for (int i =0;i< correctOrder.Count;i++)
        {
            if (correctOrder[i].transform.childCount <= 0)
                return false;

            if (isRegularOrder)
            {
                if (!GameObject.ReferenceEquals(correctOrder[i].transform.GetChild(0).gameObject, UIPoints[i]))
                    isOk = false;
            }
            else
            {
                if(!GameObject.ReferenceEquals(correctOrder[i].transform.GetChild(0).gameObject, UIPoints[UIPoints.Count - 1 - i]))
                    isOk = false;
            }
        }

        return isOk;
    }
    public void ChangeColor(bool isOk)
    {
        if (isOk)
        {
            for(int i = 0; i < placeHoldersParent.transform.childCount; i++)
            {
                if (placeHoldersParent.transform.GetChild(i).GetComponent<Image>())
                    placeHoldersParent.transform.GetChild(i).GetComponent<Image>().color = new Color32(25, 255, 0, 255);
            }

        }
        else
        {
            for (int i = 0; i < placeHoldersParent.transform.childCount; i++)
            {
                if (placeHoldersParent.transform.GetChild(i).GetComponent<Image>())
                    placeHoldersParent.transform.GetChild(i).GetComponent<Image>().color = new Color32(255, 145, 0, 255);
            }

        }
    }
}
