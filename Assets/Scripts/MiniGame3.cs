using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame3 : MonoBehaviour
{
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
        for (int i = 0; i < manager.transform.childCount; i++)
        {
            if (manager.transform.GetChild(i).gameObject.GetComponent<FileButtonManager>())
            {

                if (manager.transform.GetChild(i).gameObject.GetComponent<FileButtonManager>().fileName == ModifyScriptable.instance.foldersNames[2])
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
        button.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(.5f);
        button.GetComponent<Image>().color = Color.white;

    }
}
