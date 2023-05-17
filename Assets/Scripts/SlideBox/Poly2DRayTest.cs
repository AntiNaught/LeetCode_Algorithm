using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;
using UnityEditor;

public class Poly2DRayTest : MonoBehaviour
{
    [SerializeField]
    public BoxSpace.MoveDirection allowDirection;
    [SerializeField]
    public Vector3 min;

    [SerializeField]
    public Vector3 max;

    private Bounds mBounds;

    public Bounds bounds { get { return mBounds; } }

    Vector3 enterMousePos = Vector3.zero;

    bool clickTriggered = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Action<Poly2DRayTest, BoxSpace.MoveDirection> OnDragTriggered = null;

    Vector3 allowReal_dir = Vector3.one;
    bool moving = false;
    private void Awake()
    {
    }

    public void Init(Action<Poly2DRayTest, BoxSpace.MoveDirection> polyDrag)
    {
        clickTriggered = false;
        OnDragTriggered = polyDrag;
        allowReal_dir = BoxSpace.GetRealDirectionByEnum(allowDirection).normalized;

        mBounds = new Bounds(min + (max - min) / 2, (max - min) - new Vector3(0.01f, 0.01f, 0.01f));
        
    }

    private void OnMouseDrag()
    {
        //if (clickTriggered) return;

        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector3 deltaPos = mousePos - enterMousePos;
        //Vector3 dir = deltaPos.normalized;
        ////Debug.Log("shape drag" + allow_dir.ToString() + " || " + dir.ToString() + "||" + Vector3.Dot(dir, allow_dir).ToString());
        ////Debug.DrawLine(transform.position, transform.position + allowReal_dir, Color.red);
        ////Debug.DrawLine(transform.position, transform.position + deltaPos, Color.yellow);
        //if(Mathf.Abs( Vector3.Angle(dir, allowReal_dir)) < 45 && deltaPos.sqrMagnitude > 1.5f)
        ////if (Vector3.Dot(dir, allow_dir) > 0.7f && deltaPos.sqrMagnitude > 2.5)
        //{
        //    clickTriggered = true;
        //    if (OnDragTriggered != null)
        //    {
        //        OnDragTriggered(this, this.allowDirection);
        //    }
        //}
    }

    private void OnMouseDown()
    {
        Vector3 mousePos = Input.mousePosition;
        enterMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        GetComponent<SpriteRenderer>().color = Color.green;
    }

    private void OnMouseUp()
    {
        clickTriggered = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnMouseUpAsButton()
    {
        if (OnDragTriggered != null)
        {
            OnDragTriggered(this, this.allowDirection);
        }
    }

    public void SetInteractable(bool on)
    {
        GetComponent<Collider2D>().enabled = on;
    }

    public void MoveBounds(Vector3 logicDistance)
    {
        mBounds.center += logicDistance;
    }

    public void Shake()
    {
        GetComponent<SpriteRenderer>().color = Color.yellow;
    }
}
