using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlideBox : MonoBehaviour
{
    [SerializeField]
    BoxSpace.MoveDirection allow;

    [SerializeField]
    private Vector3 size;

    //Bounds bounds = new Bounds();

    private Vector3 position;

    private void UpdateBounds()
    {
        //bounds.size = size;
        //bounds.min = position;
    }

    public void Init(Vector3 logicPosition)
    {
        position = logicPosition;
        UpdateBounds();
    }

    public Bounds GetBounds()
    {
        return transform.GetComponent<Collider>().bounds;
    }

    private void OnMouseDrag()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mouseScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z);
        transform.position = Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}