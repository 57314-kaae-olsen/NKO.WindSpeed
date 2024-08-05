using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKO.WindSpeed.SoundDelayFakeSensor.Math
{
    public class Vector
    {
        public double DX { get; private set; }
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
}

