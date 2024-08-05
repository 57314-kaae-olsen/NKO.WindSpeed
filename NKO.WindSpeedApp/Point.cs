using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKO.WindSpeedApp
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

            return Math.Sqrt(dx * dx + dy * dy);
        }
    }

    public class Vector
    {
        public double DX { get; private set;}
        public double DY { get; private set; }

        public Vector(Point fromPoint, Point toPpoint)
        {
            DX = toPpoint.X - fromPoint.X;
            DY = toPpoint.Y - fromPoint.Y;
        }

        public Vector(double dx, double dy)
        {
            DX = dx;
            DY = dy;
        }

        public Vector Multiply(double factor)
        {
            return new Vector(DX *= factor, DY *= factor);
        }


    }

    public interface ISensor
    {
        public double GetDelay();
        public double DistanceDelay { get; set; }

    }

    public class Sensor : ISensor 
    {
        public Sensor() //double distance)
        {
        }

        public double DistanceDelay { get; set; }

        public double GetDelay() { return DistanceDelay * DistanceDelay; }
    }

    public class UltraSoundAnemometer<T> where T : new()
    {
        private ISensor _sensor;
        public UltraSoundAnemometer()
        {
            _sensor = (ISensor) new T();
            _sensor.DistanceDelay = 999;
        }
        public double GetWindSpeed()
        {
            return _sensor.GetDelay();
         }
    }
}
