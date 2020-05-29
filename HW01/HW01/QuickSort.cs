using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace HW01
{
    public class QuickSort
    {
        #region 排序法

        // https://dotblogs.com.tw/kennyshu/2009/10/24/11270

        public static void Sort(List<int> randList, int left, int right)
        {
            if (left < right)
            {
                int i = left;
                int j = right + 1;
                while (true)
                {
                    while (i + 1 < randList.Count && randList[++i] < randList[left]) ;
                    while (j - 1 > -1 && randList[--j] > randList[left]) ;
                    if (i >= j)
                    {
                        break;
                    }
                    Helpers.Swap(randList, i, j);
                }
                Helpers.Swap(randList, left, j);
                Sort(randList, left, j - 1);
                Sort(randList, j + 1, right);
            }
        }

        #endregion

        public static void StartTestQuickSort()
        {
            Stopwatch sw = new Stopwatch();
            List<Helpers.Record> result = new List<Helpers.Record>();

            foreach (int seed in Config.SeedList)
            {
                for (int i = 1; i <= Config.Round; i++)
                {
                    List<int> randList = Helpers.GenderateRadomNumbers(seed);

                    sw.Reset();
                    sw.Start();
                    Sort(randList, 0, randList.Count - 1);
                    sw.Stop();

                    result.Add(new Helpers.Record
                    {
                        SortName = "Quick Sort",
                        Seed = seed.ToString(),
                        Round = i.ToString(),
                        Time = sw.Elapsed.TotalSeconds.ToString()
                    });

                    Console.WriteLine($"({seed}) 第 {i} 回合 花費時間 {sw.Elapsed.TotalSeconds.ToString()} 秒");
                }
            }

            Console.WriteLine("Quick Sort 結束");
            Console.WriteLine("開始將 Quick Sort 結果寫入檔案");
            Helpers.WriteExcel(result);
            Console.WriteLine("寫檔完畢");
        }
    }
}