using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnMouseOverObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool onMouseOver = false;

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        onMouseOver = true;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        onMouseOver = false;
    }
}
