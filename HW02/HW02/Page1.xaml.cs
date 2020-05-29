using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace HW02
{
    /// <summary>
    /// Page1.xaml 的互動邏輯
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text != String.Empty)
            {
                int pointTotalNumber = Int32.Parse(textBox.Text);

                // 顯示進度條
                progressBarGrid.Visibility = Visibility.Visible;

                List<Point> points = GenerateRandomPoints(pointTotalNumber);

                // 換頁
                Page2 page2 = new Page2(points);
                this.NavigationService.Navigate(page2);
            }
        }

        /// <summary>
        /// 產生點的亂數
        /// </summary>
        /// <param name="pointTotalNumber"></param>
        /// <returns></returns>
        private List<Point> GenerateRandomPoints(int pointTotalNumber)
        {
            var result = new List<Point>();
            Random random = new Random();

            for (int i = 0; i < pointTotalNumber; i++)
            {
                // 進度條
                progressBar.Dispatcher.Invoke(() => progressBar.Value = i / (pointTotalNumber / 100.0), DispatcherPriority.Background);

                // 根據 Canvas 的 Width 跟 Height
                Point p = new Point(random.Next(0, 500), random.Next(0, 500));
                result.Add(p);
            }

            return result;
        }
    }
}
