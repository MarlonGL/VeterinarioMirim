using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ItemType
{
    SHOWER,
    FOOD,
    SPRAY,
    HAND
}

public class Slot : MonoBehaviour, IDropHandler
{
    public ItemType type;
    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
                return transform.GetChild(0).gameObject;
            else
                return null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!item)
        {
            if (DragHandler.itemBeingDragged.GetComponent<DragHandler>().type == type)
            {
                DragHandler.itemBeingDragged.transform.SetParent(transform);
                DragHandler.itemBeingDragged.transform.transform.position = transform.position;
            }
        }
    }
}
