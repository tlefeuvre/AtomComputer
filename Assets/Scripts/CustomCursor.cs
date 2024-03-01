using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomCursor : MonoBehaviour
{
    public GameObject cursor;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        cursor.transform.SetAsLastSibling();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            /*Debug.Log("hit");
            Debug.Log(hit.transform.name);
            Debug.Log(hit.point);
            Debug.Log("-----");*/
            Vector3 newPos = hit.point * 102.4f;
            newPos.z = 0;
            cursor.transform.localPosition = newPos;
           // Debug.Log(newPos);


        }

        /*var eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        if (results.Where(r => r.gameObject.layer == 6).Count() > 0) //6 being my UILayer
        {
            Debug.Log("eventhit");

            Debug.Log(results[0].gameObject.name); //The UI Element
            Debug.Log("-----");

        }*/
    }
}
