using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public bool  isMiniGame4 = false;
    public GameObject minigame4;
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DraggablePoint draggablePoint = dropped.GetComponent<DraggablePoint>();
        draggablePoint.parentAfterDrag = transform;

        if (isMiniGame4)
        {
            WeaponManager(draggablePoint.type);

        }
       
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WeaponManager(string type)
    {
        if (minigame4)
        {
            minigame4.GetComponent<MiniGame4>().StartLoading();
        }
        if (type == "Folder")
        {
            Debug.Log("folder");
            ServerHandler.Instance.SendMessage(21);
        }

        if (type == "Disk")
        {
            Debug.Log("Disk");
            ServerHandler.Instance.SendMessage(22);

        }

        if (type == "Save")
        {
            Debug.Log("Save");
            ServerHandler.Instance.SendMessage(23);

        }

        if (type == "Defender")
        {
            Debug.Log("Defender");
            ServerHandler.Instance.SendMessage(24);

        }

        if (type == "Cursor")
        {
            GameObject.FindGameObjectWithTag("Cursor").SetActive(false);
            ServerHandler.Instance.SendMessage(25);

        }
    }
}
