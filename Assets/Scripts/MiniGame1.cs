using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame1 : MonoBehaviour
{
    private int nbRow = 3;
    private int nbCol = 6;

    private List<GameObject> gameObjects = new List<GameObject>();
    private GameObject PressedButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NotSuccess(GameObject pressedButton)
    {
        //PressedButton = pressedButton;
        //StartCoroutine("CircleAnim");
       
        //on recup l'index du boutton presse
        int index = GetButtonIndex(pressedButton);
        StartCoroutine(ChangeColor(pressedButton));


    }
    public void IsSuccess()
    {
        ModifyScriptable.instance.GetUnlockFolderName(ModifyScriptable.instance.foldersNames[0]);

        GameObject manager = GameObject.FindGameObjectWithTag("FilesManager");
        for(int i =0; i < manager.transform.childCount; i++)
        {
            if (manager.transform.GetChild(i).gameObject.GetComponent<FileButtonManager>())
            {

                if (manager.transform.GetChild(i).gameObject.GetComponent<FileButtonManager>().fileName == ModifyScriptable.instance.foldersNames[0])
                {
                    int id = WindowManager.instance.arborescence.Count - 1;
                    WindowManager.instance.arborescence.Add(WindowManager.instance.arborescence[id].childs[manager.transform.GetChild(i).gameObject.GetComponent<FileButtonManager>().listIndex]);
                    WindowManager.instance.NewWindow();
                    gameObject.SetActive(false);
                    return;
                }
            }
        }
        



    }
    public int GetButtonIndex(GameObject button)
    {
        int index = 0;
        int childsCount = transform.childCount;

        for (int i = 0; i < childsCount; i++)
        {
            if (ReferenceEquals(button, transform.GetChild(i).gameObject))
                index = i;
        }

        return index;
    }

    IEnumerator ChangeColor(GameObject button)
    {
        button.GetComponent < Image >().color = Color.red;
        yield return new WaitForSeconds(.5f);
        button.GetComponent<Image>().color = Color.white;

    }
    IEnumerator CircleAnim()
    {
        int boucle = 0;

        int index = GetButtonIndex(PressedButton);
        List<GameObject> list = gameObjects;
        list.Add(gameObject.transform.GetChild(index).gameObject);

        while (list.Count != nbRow * nbCol )
        {
            for(int i =0; i<list.Count; i++)
            {
                Debug.Log("boucle = " + boucle);

                index = GetButtonIndex(list[i]);
                int newIndex = index + 1;
                if (newIndex >= 0 && newIndex < (nbRow * nbCol))
                {
                    Debug.Log("new index = " + newIndex);
                    if(!list.Contains(gameObject.transform.GetChild(newIndex).gameObject))
                        list.Add(gameObject.transform.GetChild(newIndex).gameObject);
                }

                newIndex = index - 1;

                if (newIndex >= 0 && newIndex < (nbRow * nbCol))
                {
                    Debug.Log("new newIndex = " + newIndex);

                    if (!list.Contains(gameObject.transform.GetChild(newIndex).gameObject))
                        list.Add(gameObject.transform.GetChild(newIndex).gameObject);
                }

                newIndex = index - nbCol;

                if (newIndex >= 0 && newIndex < (nbRow * nbCol))
                {
                    Debug.Log("new newIndex = " + newIndex);

                    if (!list.Contains(gameObject.transform.GetChild(newIndex).gameObject))
                        list.Add(gameObject.transform.GetChild(newIndex).gameObject);
                }

                newIndex = index + nbCol;

                if (newIndex >= 0 && newIndex < (nbRow * nbCol))
                {
                    Debug.Log("new newIndex = " + newIndex);

                    if (!list.Contains(gameObject.transform.GetChild(newIndex).gameObject))
                        list.Add(gameObject.transform.GetChild(newIndex).gameObject);
                }

                list[i].GetComponent<Image>().color = Color.red;
                if(i%4 == 0)
                {
                    yield return new WaitForSeconds(2.0f);

                }
            }
            boucle += 1;
           

        }
    }
    
}
