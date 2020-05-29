using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace HW01
{
    public class HeapSort
    {
        #region 排序法

        public static void Sort(List<int> randList)
        {
            int length = randList.Count;

            for (int i = length / 2 - 1; i >= 0; i--)
            {
                Heapify(randList, length, i);
            }
            for (int i = length - 1; i >= 0; i--)
            {
                Helpers.Swap(randList, 0, i);
                Heapify(randList, i, 0);
            }
        }

        private static void Heapify(List<int> list, int length, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            if (left < length && list[left] > list[largest])
            {
                largest = left;
            }
            if (right < length && list[right] > list[largest])
            {
                largest = right;
            }
            if (largest != i)
            {
                Helpers.Swap(list, i, largest);
                Heapify(list, length, largest);
            }
        }

        #endregion

        public static void StartTestHeapSort()
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
                        SortName = "Heap Sort",
                        Seed = seed.ToString(),
                        Round = i.ToString(),
                        Time = sw.Elapsed.TotalSeconds.ToString()
                    });

                    Console.WriteLine($"({seed}) 第 {i} 回合 花費時間 {sw.Elapsed.TotalSeconds.ToString()} 秒");
                }
            }

            Console.WriteLine("Heap Sort 結束");
            Console.WriteLine("開始將 Heap Sort 結果寫入檔案");
            Helpers.WriteExcel(result);
            Console.WriteLine("寫檔完畢");
        }
    }
}