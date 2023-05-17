using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDire : MonoBehaviour
{
    public string moveDir = "";

    private void OnMouseDown()
    {
        Debug.Log(GetComponent<Collider>().bounds.center.ToString());
    }
}
