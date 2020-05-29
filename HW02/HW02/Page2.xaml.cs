using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace HW02
{
    /// <summary>
    /// Page2.xaml 的互動邏輯
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2(List<Point> points)
        {
            InitializeComponent();
            data.ItemsSource = points;

            /* Canvas 左上為原點，往右為 x 軸正向，往下為 y 軸正向 */
            // 畫點
            foreach (var i in points)
            {
                // 沒有畫點的功能，使用橢圓產生點
                Ellipse el = new Ellipse();
                el.Height = 2.0;
                el.Width = 2.0;
                el.Fill = System.Windows.Media.Brushes.Red;
                el.Stroke = System.Windows.Media.Brushes.Red;
                el.StrokeThickness = 1;
                Canvas.SetLeft(el, i.X);
                Canvas.SetTop(el, i.Y);

                Main_Canvas.Children.Add(el);
            }
        }

        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            List<Point> points = (List<Point>)data.ItemsSource;

            // ConvexHull 的點
            IList<Point> actual = ConvexHull.MakeHull(points);
            data.ItemsSource = actual;

            // 畫線
            for (int i = 0; i < actual.Count; i++)
            {
                if (i == actual.Count - 1)
                {
                    Line l = new Line();
                    l.Stroke = System.Windows.Media.Brushes.Green;
                    l.X1 = actual[i].X;
                    l.Y1 = actual[i].Y;
                    l.X2 = actual[0].X;
                    l.Y2 = actual[0].Y;
                    Main_Canvas.Children.Add(l);
                }
                else
                {
                    Line l = new Line();
                    l.Stroke = System.Windows.Media.Brushes.Green;
                    l.X1 = actual[i].X;
                    l.Y1 = actual[i].Y;
                    l.X2 = actual[i + 1].X;
                    l.Y2 = actual[i + 1].Y;
                    Main_Canvas.Children.Add(l);
                }
            }

            buttonNext.Visibility = Visibility.Collapsed;
            buttonReatsrt.Visibility = Visibility.Visible;
        }

        private void buttonReatsrt_Click(object sender, RoutedEventArgs e)
        {
            Page1 page1 = new Page1();
            this.NavigationService.Navigate(page1);
        }
    }
}
