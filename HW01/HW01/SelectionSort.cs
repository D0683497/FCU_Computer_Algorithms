using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace HW01
{
    public class SelectionSort
    {
        #region 排序法

        public static void Sort(List<int> randList)
        {
            int lenght = randList.Count;

            int smallest;

            for (int i = 0; i < lenght - 1; i++)
            {
                smallest = i;
                for (int j = i + 1; j < lenght; j++)
                {
                    if (randList[j] < randList[smallest])
                    {
                        smallest = j;
                    }
                }
                Helpers.Swap(randList, smallest, i);
            }
        }

        #endregion

        public static void StartTestSelectionSort()
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
                        SortName = "Selection Sort",
                        Seed = seed.ToString(),
                        Round = i.ToString(),
                        Time = sw.Elapsed.TotalSeconds.ToString()
                    });

                    Console.WriteLine($"({seed}) 第 {i} 回合 花費時間 {sw.Elapsed.TotalSeconds.ToString()} 秒");
                }
            }

            Console.WriteLine("Selection Sort 結束");
            Console.WriteLine("開始將 Selection Sort 結果寫入檔案");
            Helpers.WriteExcel(result);
            Console.WriteLine("寫檔完畢");
        }
    }
}