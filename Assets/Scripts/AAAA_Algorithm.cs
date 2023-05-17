using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Linq;

public class AAAA_Algorithm : MonoBehaviour
{
    /*给你一个整数数组 nums ，请计算数组的 中心下标 。
     * 数组 中心下标 是数组的一个下标，其左侧所有元素相加的和等于右侧所有元素相加的和。
     * 如果中心下标位于数组最左端，那么左侧数之和视为 0 ，因为在下标的左侧不存在元素。这一点对于中心下标位于数组最右端同样适用。
     * 如果数组有多个中心下标，应该返回 最靠近左边 的那一个。如果数组不存在中心下标，返回 -1 。
     */
    public int PivotIndex(int[] nums)
    {
        int sum_total = 0;
        int sum_left = 0;
        int sum_right = 0;

        for (int i = 0; i < nums.Length; i++)
        {
            sum_total += nums[i];
        };

        sum_right = sum_total;
        for (int i = 0; i < nums.Length; i++)
        {
            sum_right -= nums[i];
            if (sum_left == sum_right)
            {
                return i;
            }
            else
            {
                sum_left += nums[i];
            }
        };

        return -1;
    }

    //二分查找
    public int SearchInsert(int[] nums, int target)
    {
        if (nums.Length < 1)
            return 0;

        int l_idx = 0;
        int r_idx = nums.Length - 1;
        while (l_idx <= r_idx)
        {
            int mid_idx = l_idx + (r_idx - l_idx) / 2;
            int mid_value = nums[mid_idx];
            //找到了
            if (mid_value == target)
            {
                return mid_idx;
            }

            //没找到
            if (mid_value < target)
            {
                l_idx = mid_idx + 1;
            }
            else if (mid_value > target)
            {
                r_idx = mid_idx - 1;
            }
        };

        return l_idx;
    }

    // 假定，所有 interval 是按照中间值递增的顺序有序排列的
    public int[][] MergeInterval(int[][] intervals)
    {
        if (intervals.Length < 1)
            return null;

        if (intervals.Length == 1)
            return intervals;

        List<int[]> l = new List<int[]>();
        int[] cur_interval = intervals[0];
        for (int i = 1; i < intervals.Length; i++)
        {
            if (cur_interval[1] >= intervals[i][0])
            {
                cur_interval = new int[] { cur_interval[0], intervals[i][1] };
            }
            else
            {
                l.Add(cur_interval);
                cur_interval = intervals[i];
            }
        }
        l.Add(cur_interval);
        return l.ToArray();
    }

    public void Rotate(int[][] matrix)
    {
        //先转置
        for (int i = 0; i < matrix.Length; i++)
        {
            for (int j = 0; j < i; j++)
            {
                int temp = matrix[i][j];

                matrix[i][j] = matrix[j][i];
                matrix[j][i] = temp;
            }
        }

        //再左右翻转
        for (int i = 0; i < matrix.Length; i++)
        {
            for (int j = 0; j < matrix.Length / 2; j++)
            {
                //swap matrix[i][j] & matrix[length - i][j]
                int temp = matrix[i][j];
                matrix[i][j] = matrix[i][matrix.Length - 1 - j];
                matrix[i][matrix.Length - 1 - j] = temp;
            }
        }
    }

    public void Rotate2(int[][] matrix)
    {

    }

    public static int[] FinDiaonalOrder(int[][] nums)
    {
        //if (nums.Length < 1 || nums[0].Length < 1)
        //    return null;

        int m = nums.Length;        //行数
        int n = nums[0].Length;     //列数

        //int m = nums.GetLength(0);        //行数
        //int n = nums.GetLength(1);        //列数

        int[] result = new int[m * n];

        int i = 0;  //行标
        int j = 0;  //列标
        int idx = 0;
        bool direction = true;

        while (true)
        {
            result[idx] = nums[i][j];

            if (direction)
            {
                //↗
                if (i == m - 1 && j == n - 1)         //遇到下界终止
                {
                    break;
                }
                else if (j == n - 1)     //遇到右边界下移
                {
                    i++;
                    direction = !direction;
                }
                else if (i == 0)         //遇到上边界右移
                {
                    j++;
                    direction = !direction;
                }                       //正常前进
                else
                {
                    i--;
                    j++;
                }
            }
            else
            {
                //↙
                if (i == m - 1 && j == n - 1)          //右界停止
                {
                    break;
                }
                else if (i == m - 1)      //下界右移
                {
                    j++;
                    direction = !direction;
                }
                else if (j == 0)         //左界下移着
                {
                    i++;
                    direction = !direction;
                }
                else                    //正常前进
                {
                    i++;
                    j--;
                }
            }

            idx++;
        }

        return result;
    }


    //多个字符串中的最长公共子串
    public static string LongestCommonPrefix(string[] strs)
    {
        StringBuilder sb = new StringBuilder();
        int idx = 0;
        bool done = false;
        while (!done)
        {
            char c;
            if (idx < strs[0].Length)
                c = strs[0][idx];
            else
                break;

            for (int i = 1; i < strs.Length; i++)
            {
                if (idx < strs[i].Length && (strs[i][idx] == c))
                {

                }
                else
                {
                    done = true;
                    break;
                }
            }

            if (!done)
            {
                idx++;
                sb.Append(c);
            }
        }

        return sb.ToString();
    }

    //返回字符串中的最长回文子串
    public static string LongestPalindrome(string s)
    {
        if (s.Length < 1)
            return null;

        StringBuilder sb_result = new StringBuilder();
        sb_result.Append(s[0]);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < s.Length; i++)
        {
            {
                int idx_l = i - 1;
                int idx_r = i + 1;
                sb.Clear();
                sb.Append(s[i]);
                while (idx_l >= 0 && idx_r <= s.Length - 1)
                {
                    if (s[idx_l] == s[idx_r])
                    {
                        sb.Insert(0, s[idx_l]);
                        sb.Append(s[idx_r]);
                        idx_l--;
                        idx_r++;
                    }
                    else
                        break;
                }
                if (sb.Length > sb_result.Length)
                {
                    sb_result.Clear();
                    sb_result.Append(sb);
                }

            }

            {
                int idx_l = i;
                int idx_r = i + 1;
                sb.Clear();
                while (idx_l >= 0 && idx_r <= s.Length - 1)
                {
                    if (s[idx_l] == s[idx_r])
                    {
                        sb.Insert(0, s[idx_l]);
                        sb.Append(s[idx_r]);
                        idx_l--;
                        idx_r++;
                    }
                    else
                        break;
                }
                if (sb.Length > sb_result.Length)
                {
                    sb_result.Clear();
                    sb_result.Append(sb);
                }

            }
        }
        return sb_result.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int a = PivotIndex(new int[] { 1, -1, -2, 100, 3, -1, -100, 10 });
            print("haha : " + a.ToString());
        }
    }

    public static int[] RunningSum(int[] nums)
    {
        int[] ret = new int[nums.Length];
        ret[0] = nums[0];
        for (int i = 1; i < nums.Length; i++)
        {
            ret[i] = nums[i] + ret[i - 1];
        }
        return ret;
    }

    //给你一个非负整数 num ，请你返回将它变成 0 所需要的步数。
    //如果当前数字是偶数，你需要把它除以 2 ；否则，减去 1 。
    public static int NumberOfSteps(int num)
    {
        /*
        int step = 0;
        while (num>0)
        {
            if (num % 2 > 0)
                num -= 1;
            else
                num /= 2;
            step++;
        }
        return step;
         */

        //位运算，转成二进制后，1 的个数
        int bitmask = 1;
        int step = 0;
        while (bitmask <= num)
        {
            step += (num & bitmask) > 0 ? 2 : 1;
            bitmask <<= 1;
        }

        return step - 1 > 0 ? step - 1 : 0;
    }

    public int MaximumWealth(int[][] accounts)
    {
        int max = 0;
        for (int i = 0; i < accounts.Length; i++)
        {
            int sum = 0;
            for (int j = 0; j < accounts[i].Length; j++)
            {
                sum += accounts[i][j];
            }
            max = sum > max ? sum : max;
        }
        return max;
    }

    public IList<string> FizzBuzz(int n)
    {
        List<string> ret = new List<string>();
        for (int i = 1; i <= n; i++)
        {
            if (i % 3 == 0 && i % 5 == 0)
                ret.Add("FizzBuzz");
            else if (i % 3 == 0)
                ret.Add("Fizz");
            else if (i % 5 == 0)
                ret.Add("Buzz");
            else
                ret.Add(i.ToString());
        }
        return ret;
    }

    public ListNode MiddleNode(ListNode head)
    {
        //int halfCount = 0;
        //ListNode cur = head;
        //while (cur != null && cur.next != null)
        //{
        //    cur = cur.next;
        //    cur = cur.next;
        //    halfCount++;
        //}

        //cur = head;
        //for (int i = 0; i < halfCount; i++)
        //{
        //    cur = cur.next;
        //}
        //return cur;

        //快慢指针
        ListNode slow = head, fast = head;
        while (fast != null && fast.next != null)
        {
            slow = slow.next;
            fast = fast.next.next;
        }
        return slow;
    }

    /* 333 赎金信
     * 给你两个字符串：ransomNote 和 magazine ，判断 ransomNote 能不能由 magazine 里面的字符构成。
        如果可以，返回 true ；否则返回 false 。
        magazine 中的每个字符只能在 ransomNote 中使用一次。

        来源：力扣（LeetCode）
        链接：https://leetcode.cn/problems/ransom-note
        著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。
     */
    public static bool CanConstruct(string ransomNote, string magazine)
    {
        //粗暴解法，遍历 ransonNote, 每个字符去 magazine 中找，找到一个移出一个
        //for (int i = 0; i < ransomNote.Length; i++)
        //{
        //    char c = ransomNote[i];
        //    if (magazine.Contains(c))
        //    {
        //        magazine = magazine.Remove(magazine.IndexOf(c), 1);
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        //return true;

        //for (int i = 0; i < magazine.Length; i++)
        //{
        //    char c_m = magazine[i];
        //    StringBuilder sb = new StringBuilder(ransomNote);
        //    for (int j = sb.Length - 1; j > -1; j++)
        //    {
        //        char c = sb[i];
        //        if (c == c_m)
        //        {
        //            sb.Remove(i, 1);
        //            if (sb.Length < 1)
        //                return true;
        //        }
        //    }
        //}

        //return false;

        int[] magazineArr = new int[26];
        foreach (var x in magazine)
        {
            magazineArr[x - 'a']++;
        }

        foreach (var x in ransomNote)
        {
            if (magazineArr[x - 'a'] < 1) return false;
            magazineArr[x - 'a']--;
        }
        return true;


    }

    /*
     * 给你一个 升序排列 的数组 nums ，请你 原地 删除重复出现的元素，使每个元素 只出现一次 ，返回删除后数组的新长度。元素的 相对顺序 应该保持 一致 。
     * 由于在某些语言中不能改变数组的长度，所以必须将结果放在数组nums的第一部分。更规范地说，如果在删除重复项之后有 k 个元素，那么 nums 的前 k 个元素应该保存最终结果。
     */
    public static int RemoveDuplicates(int[] nums)
    {
        int idx = 1;
        //双指针
        for (int i = 1; i < nums.Length; i++)
        {
            int v = nums[i - 1];

            while (true)
            {
                if (idx < nums.Length)
                {
                    if (nums[idx] == v)
                        idx++;
                    else
                        break;
                }
                else
                {
                    return i;
                }
            }
            nums[i] = nums[idx];
        }
        return nums.Length;
    }

    private static void reverse(int[] nums, int start, int end)
    {
        for (int i = 0; i < (end - start + 1) / 2; i++)
        {
            int l = start + i;
            int r = end - i;
            int tmp = nums[l];
            nums[l] = nums[r];
            nums[r] = tmp;
        }
    }

    //给定一个整数数组 nums，将数组中的元素向右轮转 k 个位置，其中 k 是非负数。
    public static void Rotate(int[] nums, int k)
    {
        if (!(nums?.Length > 0))
            return;

        k %= nums.Length;
        if (k < 1)
            return;

        reverse(nums, 0, nums.Length - 1);
        reverse(nums, 0, k - 1);
        reverse(nums, k, nums.Length - 1);
    }

    public static int[] PlusOne(int[] digits)
    {
        List<int> list = new List<int>();

        int add = 1;
        for (int i = 0; i < digits.Length; i++)
        {
            int value = digits[digits.Length - 1 - i];
            value += add;
            add = value / 10;
            list.Insert(0, value % 10);
        }
        if (add > 0)
            list.Insert(0, add);

        return list.ToArray<int>();
    }

    //给定一个数组 nums，编写一个函数将所有 0 移动到数组的末尾，同时保持非零元素的相对顺序。
    //请注意 ，必须在不复制数组的情况下原地对数组进行操作。
    public static void MoveZeroes(int[] nums)
    {
        int zCount = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if(nums[i] == 0)
            {
                zCount++;
            }
            else
            {
                nums[i - zCount] = nums[i];
            }
        }

        //将末尾置0
        for (int i = nums.Length - zCount; i < nums.Length; i++)
        {
            nums[i] = 0;
        }
    }

    //public static int[] TwoSum(int[] nums, int target)
    //{

    //}

    public IList<int> PreorderTraversal(TreeNode root)
    {
        List<int> ret = new List<int>();
        if (root == null) return ret;

        //递归遍历法
        //PreOrderVisit(root, (node) => { ret.Add(node.val); });

        //非递归遍历法
        Stack<TreeNode> stack = new Stack<TreeNode>();
        stack.Push(root);
        while (stack.Count > 0)
        {
            TreeNode n = stack.Pop();
            ret.Add(n.val);

            if (n.right != null) stack.Push(n.right);
            if (n.left != null) stack.Push(n.left);
        }

        return ret;
    }

    public void PreOrderVisit(TreeNode node, Action<TreeNode> visit)
    {
        if (node == null) return;
        visit(node);
        PreOrderVisit(node.left, visit);
        PreOrderVisit(node.right, visit);
    }

    public IList<int> InorderTraversal(TreeNode root)
    {
        List<int> ret = new List<int>();

        //递归遍历法
        //InOrderVisit(root, (node) => { ret.Add(node.val); });

        //非递归遍历
        Stack<TreeNode> stack = new Stack<TreeNode>();
   
        return ret;
    }

    public void InOrderVisit(TreeNode node, Action<TreeNode> visit)
    {
        if (node == null) return;
        InOrderVisit(node.left, visit);
        visit(node);
        InOrderVisit(node.right, visit);
    }
}

public class Solution
{

}

public class ListNode
{
    public int val;
    public ListNode next;
    public ListNode(int val = 0, ListNode next = null)
    {
        this.val = val;
        this.next = next;
    }
}
public class TreeNode
{
    public int val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }
}

