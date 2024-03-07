using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomCursor : MonoBehaviour
{
    public GameObject cursor;
    void Start()
    {
        //Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        cursor.transform.SetAsLastSibling();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            Vector3 newPos = hit.point * 102.4f;
            newPos.z = 0;
            cursor.transform.localPosition = newPos;
        }

    }
}
