using System;

namespace HW01
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var name in Config.SortList)
            {
                switch (name)
                {
                    case "Bubble Sort":
                        Console.WriteLine($"開始 {name}");
                        BubbleSort.StartTestBubbleSort();
                        break;
                    case "Selection Sort":
                        Console.WriteLine($"\n\n開始 {name}");
                        SelectionSort.StartTestSelectionSort();
                        break;
                    case "Insertion Sort":
                        Console.WriteLine($"\n\n開始 {name}");
                        InsertionSort.StartTestInsertionSort();
                        break;
                    case "Quick Sort":
                        Console.WriteLine($"\n\n開始 {name}");
                        QuickSort.StartTestQuickSort();
                        break;
                    case "Heap Sort":
                        Console.WriteLine($"\n\n開始 {name}");
                        HeapSort.StartTestHeapSort();
                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine("請按任意鍵退出");
            Console.ReadLine();
        }
    }
}
