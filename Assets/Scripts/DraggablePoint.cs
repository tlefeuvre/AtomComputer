using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggablePoint : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject customCursor;
    public Transform parentAfterDrag;

    private Image image;
    private void Start()
    {
        customCursor = GameObject.FindGameObjectWithTag("Cursor");
        image = GetComponent<Image>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root.GetChild(0));
        transform.SetAsLastSibling();
        Debug.Log(transform.parent);

        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (customCursor)
        {
            Vector3 newPos = customCursor.transform.localPosition;
            transform.localPosition = newPos;

        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;

    }


}
