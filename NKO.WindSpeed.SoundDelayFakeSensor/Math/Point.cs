using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NKO.WindSpeed.SoundDelayFakeSensor.Math
{
    public class Point
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Point Add(Point point, Vector vector, double multiply)
        {
            return new Point(point.X + vector.DX * multiply, point.Y + vector.DY * multiply);
        }

        public static double Distance(Point point1, Point point2)
        {
            double dx = point2.X - point1.X;
            double dy = point2.Y - point1.Y;

            return System.Math.Sqrt(dx * dx + dy * dy);
        }

        public static bool AreEqual(Point point1, Point point2, double tol = 1.0e-8) 
        {
            return System.Math.Abs(Point.Distance(point1, point2)) < tol; 
        }
    }
}
