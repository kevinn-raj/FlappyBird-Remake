using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonBehaviour : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    RectTransform rect;
    public int moveDistance = 5;

    void Start() { 
    
        rect = GetComponent<RectTransform>();
    }



    //GetComponent<RectTransform>();
    void moveButton()
    {
        rect.anchoredPosition += new Vector2(moveDistance, -moveDistance);
        Debug.Log("Down");
        //show text
    }

    void moveButtonBack()
    {
        rect.anchoredPosition -= new Vector2(moveDistance, -moveDistance);
        Debug.Log("Up");
        //show text
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        moveButton();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        moveButtonBack();
    }
}
