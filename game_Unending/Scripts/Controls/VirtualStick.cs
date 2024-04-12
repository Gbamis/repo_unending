using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public float dragDistance;
    public Image container;
    public Image joystick;
    private Vector3 m_direction;
    public Vector3 direction
    {
        set { m_direction = value; }
        get { return m_direction; }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        OnDrag(pointerEventData);
    }

    //Detect if clicks are no longer registering
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        direction = Vector3.zero;
        joystick.rectTransform.anchoredPosition = Vector3.zero;
    }
    public void OnDrag(PointerEventData data)
    {
        Vector2 pos = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(container.rectTransform, data.position, data.pressEventCamera, out pos))
        {
            pos.x = (pos.x / container.rectTransform.sizeDelta.x);
            pos.y = (pos.y / container.rectTransform.sizeDelta.y);

            Vector2 refPivot = new Vector2(0.5f, 0.5f);
            Vector2 p = container.rectTransform.pivot;
            pos.x += p.x - 0.5f;
            pos.y += p.y - 0.5f;

            float x = Mathf.Clamp(pos.x, -1, 1);
            float y = Mathf.Clamp(pos.y, -1, 1);

            direction = new Vector3(x, 0, y);
            joystick.rectTransform.anchoredPosition = new Vector3(x * dragDistance, y * dragDistance);
        }
    }
}