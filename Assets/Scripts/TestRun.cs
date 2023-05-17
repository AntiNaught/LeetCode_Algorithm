using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

public class TestRun : MonoBehaviour
{
    public int[] intarray;
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }


    void teststring()
    {
        string[] strs =
        {
            "flower",
            "flow",
            "flo",
        };

        string prefix = AAAA_Algorithm.LongestCommonPrefix(strs);
        Debug.Log("是 " + prefix);
    }

    void test回文子串()
    {
        //string s = "zxcvbabababvccgg";
        string s = "a";
        string result = AAAA_Algorithm.LongestPalindrome(s);
        Debug.Log("是 :: " + result);
        
    }

    // Start is called before the first frame update
    void StartRun()
    {
        //int[,] a =
        //{
        //    { 1, 2, 3, 1 },
        //    { 4, 5, 6, 1 },
        //    { 7, 8, 9, 1 },
        //};

        ////int[] result = AAAA_Algorithm.FinDiaonalOrder(a);
        ////for (int i = 0; i < result.Length; i++)
        //{
        //    //Debug.Log(result[i].ToString() + ",");
        //}

        //teststring();

        //test回文子串();

        //Debug.Log(AAAA_Algorithm.NumberOfSteps(123));

        //string ransomeNote = "aa";
        //string magazine = "aab";
        //AAAA_Algorithm.CanConstruct(ransomeNote, magazine);

        //string a = "abc";
        //a.Remove(0);

        //AAAA_Algorithm.RemoveDuplicates(intarray);

        //AAAA_Algorithm.Rotate(intarray, 3);

        int[] result = AAAA_Algorithm.PlusOne(intarray);
        StringBuilder sb = new StringBuilder();
        foreach (var value in result)
        {
            sb.Append(value.ToString() + ",");
        }
        Debug.Log(sb.ToString());

        Debug.Log(int.MaxValue);

        List<int> list = new List<int>();
        List<string> lstr = list.Select<int, string>(v => v.ToString()).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("这还不给我执行么");
            StartRun();
        }
    }
}
