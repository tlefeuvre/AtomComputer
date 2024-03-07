using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggablePoint : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private GameObject customCursor;
    public Transform parentAfterDrag;

    private Image image;

    public string type;
    private void Start()
    {
        customCursor = GameObject.FindGameObjectWithTag("Cursor");
        image = GetComponent<Image>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.parent.parent.parent);
        transform.SetAsLastSibling();

        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        /*if (customCursor)
        {
            Vector3 newPos = customCursor.transform.localPosition;
            transform.localPosition = newPos;

        }*/
        Vector3 newPos = Input.mousePosition;
        newPos.z = 0;
        //transform.localPosition = newPos;
        /*Debug.Log(Input.mousePosition);
        Debug.Log(transform.parent);*/

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

        transform.position = worldPosition;


    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;

    }


}
