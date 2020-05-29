using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace HW01
{
    public class InsertionSort
    {
        #region 排序法

        public static void Sort(List<int> randList)
        {
            int lenght = randList.Count;

            for (int i = 0; i < lenght - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    if (randList[j - 1] > randList[j])
                    {
                        Helpers.Swap(randList, j - 1, j);
                    }
                }
            }
        }

        #endregion

        public static void StartTestInsertionSort()
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
                        SortName = "Insertion Sort",
                        Seed = seed.ToString(),
                        Round = i.ToString(),
                        Time = sw.Elapsed.TotalSeconds.ToString()
                    });

                    Console.WriteLine($"({seed}) 第 {i} 回合 花費時間 {sw.Elapsed.TotalSeconds.ToString()} 秒");
                }
            }

            Console.WriteLine("Insertion Sort 結束");
            Console.WriteLine("開始將 Insertion Sort 結果寫入檔案");
            Helpers.WriteExcel(result);
            Console.WriteLine("寫檔完畢");
        }
    }
}