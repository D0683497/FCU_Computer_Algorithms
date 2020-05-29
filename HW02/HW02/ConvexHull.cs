using System;
using System.Collections.Generic;
using System.Linq;

namespace HW02
{
    // http://www.csie.ntnu.edu.tw/~u91029/ConvexHull.html#4
    public sealed class ConvexHull
    {
        // Returns a new list of points representing the convex hull of
        // the given set of points. The convex hull excludes collinear points.
        // This algorithm runs in O(n log n) time.
        public static IList<Point> MakeHull(IList<Point> points)
        {
            List<Point> newPoints = new List<Point>(points);
            // X 小 排到到 大，若相同看 Y (小到大)
            newPoints.Sort();
            return MakeHullPresorted(newPoints);
        }
        // Returns the convex hull, assuming that each points[i] <= points[i + 1]. Runs in O(n) time.
        public static IList<Point> MakeHullPresorted(IList<Point> points)
        {
            if (points.Count <= 1)
            {
                return new List<Point>(points);
            }

            // Andrew's monotone chain algorithm. Positive y coordinates correspond to "up"
            // as per the mathematical convention, instead of "down" as per the computer
            // graphics convention. This doesn't affect the correctness of the result.

            // 找逆時鐘的
            List<Point> upperHull = new List<Point>();
            foreach (Point p in points)
            {
                while (upperHull.Count >= 2)
                {
                    Point q = upperHull[upperHull.Count - 1];
                    Point r = upperHull[upperHull.Count - 2];
                    // p 是目前的點
                    // 向量 rq 內積 向量 rp，大於零表示從 rq 到 rp 為逆時針旋轉，去掉
                    if ((q.X - r.X) * (p.Y - r.Y) >= (q.Y - r.Y) * (p.X - r.X))
                    {
                        upperHull.RemoveAt(upperHull.Count - 1);
                    }
                    else
                    {
                        break;
                    }
                }
                upperHull.Add(p);
            }
            // 去掉最後一點，lowerHull 會從這點開始
            upperHull.RemoveAt(upperHull.Count - 1);

            IList<Point> lowerHull = new List<Point>();
            for (int i = points.Count - 1; i >= 0; i--)
            {
                Point p = points[i];
                while (lowerHull.Count >= 2)
                {
                    Point q = lowerHull[lowerHull.Count - 1];
                    Point r = lowerHull[lowerHull.Count - 2];
                    // p 是目前的點
                    // 向量 rq 內積 向量 rp，大於零表示從 rq 到 rp 為逆時針旋轉，去掉
                    if ((q.X - r.X) * (p.Y - r.Y) >= (q.Y - r.Y) * (p.X - r.X))
                    {
                        lowerHull.RemoveAt(lowerHull.Count - 1);
                    }
                    else
                    {
                        break;
                    }
                }
                lowerHull.Add(p);
            }
            // 去掉最後一點，也就是原點
            lowerHull.RemoveAt(lowerHull.Count - 1);

            // 合併
            if (!(upperHull.Count == 1 && Enumerable.SequenceEqual(upperHull, lowerHull)))
            {
                upperHull.AddRange(lowerHull);
            }

            return upperHull;
        }
    }

    public class Point : IComparable<Point>
    {
        /// <summary>
        /// X 軸
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Y 軸
        /// </summary>
        public double Y { get; set; }

        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        // https://docs.microsoft.com/zh-tw/dotnet/api/system.icomparable.compareto?view=netcore-3.1
        // 小於零 這個執行個體在排序次序中會在 other 之前。
        // 大於零 這個執行個體在排序順序中會跟在 other 之後。
        // 零 這個執行個體在排序次序中的位置和 other 相同。
        public int CompareTo(Point other)
        {
            if (X < other.X)
            {
                return -1;
            }
            else if (X > other.X)
            {
                return +1;
            }
            else if (Y < other.Y)
            {
                return -1;
            }
            else if (Y > other.Y)
            {
                return +1;
            }
            else
            {
                return 0;
            }
        }

    }
}