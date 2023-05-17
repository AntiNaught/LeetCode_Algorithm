using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PrintBounds : MonoBehaviour
{
    Collider[] colliders;
    // Start is called before the first frame update
    float getPri(Bounds bounds)
    {
        return bounds.min.x + bounds.min.y + bounds.min.z;
    }

    void Start()
    {
        colliders = GetComponentsInChildren<Collider>();

        Array.Sort<Collider>(colliders, (a, b) =>
        {
            float a_total = a.bounds.center.x + a.bounds.center.y + a.bounds.center.z;
            float b_total = b.bounds.center.x + b.bounds.center.y + b.bounds.center.z;
            return Mathf.CeilToInt((a_total - b_total)*10);
        });

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].transform.SetSiblingIndex(i);
            colliders[i].transform.name = (i+1).ToString();
        }
    }

    void LogAllBoudnsInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("[\n");
        for (int i = 0; i < colliders.Length; i++)
        {
            Bounds b = colliders[i].bounds;
            sb.Append("\t");
            //sb.AppendFormat("[{0,-2}] = ",i);
            sb.Append("{");
            sb.Append("center: {");
            sb.AppendFormat("x:{0,-3},y:{1,-3},z:{2,-3}", b.center.x, b.center.y, b.center.z); ;
            sb.Append("}, size: {");
            sb.AppendFormat("x:{0,-3},y:{1,-3},z:{2,-3}", b.size.x, b.size.y, b.size.z);
            sb.Append("},");
            sb.AppendFormat("dir : \"{0}\"", colliders[i].GetComponent<MoveDire>().moveDir);
            sb.Append("},\n");
        }
        sb.Append("]\n");
        Debug.Log(sb.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LogAllBoudnsInfo();
        }
    }
}
