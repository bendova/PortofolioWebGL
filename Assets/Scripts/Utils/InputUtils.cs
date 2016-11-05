using UnityEngine;
using System.Collections;

public class InputUtils
{
    public static bool IsMouseOverCollider(Collider2D collider)
    {
        bool isMouseOver = false;
        if (collider)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = collider.bounds.center.z;
            isMouseOver = collider.bounds.Contains(mousePos);
        }
        return isMouseOver;
    }

    public static bool IsLeftClickOnCollider(Collider2D collider)
    {
        bool isLeftClick = false;
        if (Input.GetMouseButtonDown(0))
        {
            isLeftClick = IsMouseOverCollider(collider);
        }
        return isLeftClick;
    }
}
