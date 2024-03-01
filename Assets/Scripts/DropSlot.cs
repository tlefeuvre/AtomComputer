using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public bool  isMiniGame4 = false;
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
        if (type == "Folder")
            Debug.Log("folder");

        if (type == "Cursor")
            GameObject.FindGameObjectWithTag("Cursor").SetActive(false);
    }
}
