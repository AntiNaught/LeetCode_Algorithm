using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListAddImplement : MonoBehaviour
{
    private int[] array = new int[4];
    private int count;
    public void Add(int value)
    {
        count++;
        if (array.Length < count)
        {
            int[] newarr = new int[array.Length * 2];
            //copy array
            Array.Copy(array, newarr, array.Length);
            array = newarr;
        }

        array[count - 1] = value;
    }

    public void Remove(int index)
    {
        if (index < 0 || index > count - 1)
            throw new Exception("Index Outof Range Exception!");

        for (int i = index + 1; i < array.Length; i++)
        {
            array[i - 1] = array[i];
        }
    }

    public void MoveZeroToTail(int[] nums)
    {
        int zeroCount = 0;
        for (int i = 0; i < nums.Length-1; i++)
        {
            if (nums[i] == 0)
            {
                zeroCount++;
            }
            else
            {
                //往前挪动 zerocount 个位置
                nums[i - zeroCount] = nums[i];
                nums[i] = 0;
            }
        }
    }

    public void PrintCombine(int a, int b, char[] chars)
    {
        if (a == b)
        {
            Console.Write(a);
            Console.Write(',');
        }
        else
        {
            PrintCombine(a, a, chars);
            PrintCombine(a + 1, b, chars);

            PrintCombine(a + 1, b, chars);
            PrintCombine(a, a, chars);

            PrintCombine(b, b, chars);
            PrintCombine(a, b - 1, chars);

            PrintCombine(a, b - 1, chars);
            PrintCombine(b, b, chars);
            Console.Write('\n');
        }
    }

    //动态规划
    public void DPPrint(char[] chars)
    {
        //动态规划
        for (int i = 0; i < chars.Length; i++)
        {
            for (int j = i; j < chars.Length; j++)
            {
                PrintCombine(i, j, chars);
            }
        }
    }
}
