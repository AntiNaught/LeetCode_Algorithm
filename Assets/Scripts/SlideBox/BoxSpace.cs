using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using DG.Tweening;
using System.Threading;
using System;

//单位方块空间
struct UnitSpace
{
    public Bounds bounds;
    //空间被box覆盖的id，0表示没有box覆盖此空间
    public int BoxIdx;
}

public class BoxSpace : MonoBehaviour
{
    public bool interactive { private set; get; }
    const float cubeSize = 1.0f;

    //3*3 的空间
    const int SIDE_LEN = 3;
    UnitSpace[,,] map = new UnitSpace[3,3,3];
    public List<Poly2DRayTest> boxList = new List<Poly2DRayTest>();
    private List<Poly2DRayTest> blockList = new List<Poly2DRayTest>();

    Vector2 MapLogicPos2D(Vector3 position)
    {
        return Vector2.one;
    }

    public enum MoveDirection
    {
        X,
        NegetiveX,
        Y,
        NegetiveY,
        Z,
        NegetiveZ,
    }

    private Vector3 GetLogicVDirectionByEnum(MoveDirection direction)
    {
        switch (direction)
        {
            case MoveDirection.X:
                return new Vector3(1, 0, 0);
            case MoveDirection.NegetiveX:
                return new Vector3(-1, 0, 0);
            case MoveDirection.Y:
                return new Vector3(0, 1, 0);
            case MoveDirection.NegetiveY:
                return new Vector3(0, -1, 0);
            case MoveDirection.Z:
                return new Vector3(0, 0, 1);
            case MoveDirection.NegetiveZ:
                return new Vector3(0, 0, -1);
            default:
                return new Vector3(0, 0, 0);
        }
    }

    private static Dictionary<MoveDirection, Vector3> dirCache = new Dictionary<MoveDirection, Vector3>();

    //单位1的长度，在特定的角度下映射到2D view port 后的长度
    public static Vector3 GetRealDirectionByEnum(MoveDirection direction)
    {
        Vector3 dir;
        switch (direction)
        {
            case MoveDirection.X:
                if (dirCache.TryGetValue(direction, out dir))
                {
                    return dir;
                }
                else
                {
                    dir = new Vector3(-Mathf.Sqrt(2) / 2, -0.5f, 0);
                    dirCache.Add(direction, dir);
                    return dir;
                }
            case MoveDirection.NegetiveX:
                if (dirCache.TryGetValue(direction, out dir))
                {
                    return dir;
                }
                else
                {
                    dir = new Vector3(Mathf.Sqrt(2) / 2, 0.5f, 0);
                    dirCache.Add(direction, dir);
                    return dir;
                }
            case MoveDirection.Y:
                if (dirCache.TryGetValue(direction, out dir))
                {
                    return dir;
                }
                else
                {
                    dir = new Vector3(0, Mathf.Sqrt(2) / 2, 0);
                    dirCache.Add(direction, dir);
                    return dir;
                }
            case MoveDirection.NegetiveY:
                if (dirCache.TryGetValue(direction, out dir))
                {
                    return dir;
                }
                else
                {
                    dir = new Vector3(0, -Mathf.Sqrt(2) / 2, 0);
                    dirCache.Add(direction, dir);
                    return dir;
                }
            case MoveDirection.Z:
                if (dirCache.TryGetValue(direction, out dir))
                {
                    return dir;
                }
                else
                {
                    dir = new Vector3(Mathf.Sqrt(2) / 2, -0.5f, 0);
                    dirCache.Add(direction, dir);
                    return dir;
                }
            case MoveDirection.NegetiveZ:
                if (dirCache.TryGetValue(direction, out dir))
                {
                    return dir;
                }
                else
                {
                    dir = new Vector3(-Mathf.Sqrt(2) / 2, 0.5f, 0);
                    dirCache.Add(direction, dir);
                    return dir;
                }
            default:
                return new Vector3(0, 0, 0);
        }
    }

    public void Init()
    {
        //拿到所有的2D cube

        //初始化地图
        for (int i = 0; i < SIDE_LEN; i++)
        {
            for (int j = 0; j < SIDE_LEN; j++)
            {
                for (int k = 0; k < SIDE_LEN; k++)
                {
                    Vector3 center = new Vector3();
                    center.x = (i + 1) * cubeSize / 2;
                    center.y = (j + 1) * cubeSize / 2;
                    center.z = (k + 1) * cubeSize / 2;
                    map[i, j, k] = new UnitSpace();
                    map[i, j, k].bounds = new Bounds(center, Vector3.one);
                    map[i, j, k].BoxIdx = 0;

                    GameObject debug_go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    debug_go.name = (i+1).ToString() + "," + j.ToString() + "," + k.ToString();
                    debug_go.transform.position = new Vector3(i, j, k) * 2;
                    SlideBox box = debug_go.AddComponent<SlideBox>();
                    //blist.Add(box);
                    map[i, j, k].BoxIdx = boxList.Count - 1;
                    Debug.Log("创建一个啊");
                }
            }
        }

        interactive = true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="box">被点击要移动的box</param>
    /// <param name="check">用来做相交测试的 Bounds </param>
    /// <param name="step">实际可以移动的单位</param>
    /// <param name="block">被哪个block拦阻</param>
    /// <returns></returns>
    bool CheckIntersection(Poly2DRayTest box, Bounds check, out Poly2DRayTest block)
    {
        for (int i = 0; i < boxList.Count; i++)
        {
            if (box == boxList[i])
                continue;
            bool intersect = check.Intersects(boxList[i].bounds);
            if (intersect)
            {
                block = boxList[i];
                return true;
            }
        }
        block = null;
        return false;
    }

    IEnumerator BoxTryMoveRoutine(Poly2DRayTest box, MoveDirection direction)
    {
        Bounds tmpBounds = box.bounds;
        Vector3 logicDir = GetLogicVDirectionByEnum(direction);
        bool intersect = false;
        int step = 0;
        Poly2DRayTest block = null;
        for (int i = 0; i < SIDE_LEN - 1; i++)
        {
            tmpBounds.center += logicDir;
            intersect = CheckIntersection(box, tmpBounds, out block);
            if (intersect)
            {
                step = i;
                break;
            }
        }

        step = intersect ? step : 10;

        //可移动步数
        if (step > 0)
        {
            Vector3 distance = GetRealDirectionByEnum(direction) * step;
            float duration = step * 0.2f;
            moving = true;
            box.transform.DOMove(box.transform.position + distance, duration, false).SetEase(Ease.Linear).OnComplete(() => moving = false);
            box.MoveBounds(logicDir * step);
        }

        //最终是否被阻挡
        if (intersect)
        {
            // 等待移动完成
            yield return new WaitUntil(() => { return !moving; });

            blockList.Clear();
            // 开始搞后面的动画
            Poly2DRayTest shakeBox = block;
            while (shakeBox != null)
            {
                shakeBox.Shake();
                blockList.Add(shakeBox);
                tmpBounds = shakeBox.bounds;
                tmpBounds.center += logicDir;
                intersect = CheckIntersection(shakeBox, tmpBounds, out shakeBox);
            }
            Debug.Log("需要晃动的有几个" + blockList.Count.ToString());
        }
        else
        {
            step = 10;
        }

        Debug.LogFormat("碰撞检测 :: {0} step :{1}", intersect, step);
    }

    private bool moving = false;
    void OnBoxStartMove(Poly2DRayTest box, MoveDirection direction)
    {
        //if (moving) return;

        StartCoroutine(BoxTryMoveRoutine(box, direction));
    }

    private void Awake()
    {
        Debug.Log("这玩意也没有给我开始么");
        //Init();

        for (int i = 0; i < boxList.Count; i++)
        {
            boxList[i].Init(OnBoxStartMove);
        }

        Bounds a = new Bounds(new Vector3(0.5f, 0.5f, 0.5f), Vector3.one);
        Bounds b = new Bounds(new Vector3(0.5f, 1.500f, 0.5f), Vector3.one);
        Debug.Log(a.Intersects(b));
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawCube(new Vector3(0.5f, 0.5f, 0.5f), Vector3.one);
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawCube(new Vector3(0.5f, 1.5001f, 0.5f), Vector3.one);
    }

    //private void Update()
    //{
    //    //Debug.DrawLine(Vector3.zero, Vector3.one, Color.red);

    //    //返回一条射线从摄像机通过一个屏幕点
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hitInfo = new RaycastHit();
    //    //（射线的起点和方向，hitonfo将包含碰到碰撞器的更多信息，射线的长度）有碰撞时，返回真
    //    if (Physics.Raycast(ray, out hitInfo, 100))
    //    {
    //        //显示检测到的碰撞物体的世界坐标
    //        print(hitInfo.point);
    //        Debug.DrawLine(transform.position, hitInfo.point, Color.red);
    //    }
    //}
}