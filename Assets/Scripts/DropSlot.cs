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
            Debug.Log("folder");

        if (type == "Disk")
            Debug.Log("Disk");

        if (type == "Save")
            Debug.Log("Save");

        if (type == "Defender")
            Debug.Log("Defender");

        if (type == "Cursor")
            GameObject.FindGameObjectWithTag("Cursor").SetActive(false);
    }
}
