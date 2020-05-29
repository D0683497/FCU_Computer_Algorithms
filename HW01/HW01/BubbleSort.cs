using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace HW01
{
    public class BubbleSort
    {
        #region 排序法

        public static void Sort(List<int> randList)
        {
            int lenght = randList.Count;

            for (int i = 1; i <= lenght - 1; i++)
            {
                for (int j = 1; j <= lenght - i; j++)
                {
                    if (randList[j] < randList[j - 1])
                    {
                        Helpers.Swap(randList, j, j - 1);
                    }
                }
            }
        }

        #endregion

        public static void StartTestBubbleSort()
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
                    Sort(randList);
                    sw.Stop();

                    result.Add(new Helpers.Record
                    {
                        SortName = "Bubble Sort",
                        Seed = seed.ToString(),
                        Round = i.ToString(),
                        Time = sw.Elapsed.TotalSeconds.ToString()
                    });

                    Console.WriteLine($"({seed}) 第 {i} 回合 花費時間 {sw.Elapsed.TotalSeconds.ToString()} 秒");
                }
            }

            Console.WriteLine("Bubble Sort 結束");
            Console.WriteLine("開始將 Bubble Sort 結果寫入檔案");
            Helpers.WriteExcel(result);
            Console.WriteLine("寫檔完畢");
        }
    }
}