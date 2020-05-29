using System.Collections.Generic;
using System.IO;

namespace HW01
{
    public class Config
    {
        public static List<string> SortList = new List<string> { "Bubble Sort", "Selection Sort", "Insertion Sort", "Quick Sort", "Heap Sort" };

        // public static List<int> SeedList = new List<int> { 50000, 100000, 150000, 200000, 250000, 300000 };

        public static List<int> SeedList = new List<int> { 50, 100, 150, 200, 250, 300 };

        public static int Round = 25;

        /// <summary>
        /// 輸出檔案名稱與路徑
        /// </summary>
        public static string FileName = Path.Combine(Directory.GetCurrentDirectory(), "D0683497_HW01.xlsx");
    }
}