using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKO.WindSpeed.SoundDelayFakeSensor
{
    public static class FakeWind
    {
        public static double FakeWindSpeed { get; set; }
        public static double FakeWindOrientationDegr { get; set; }

        public static void SetWind(double speed, double orientationDegr)
        {
            FakeWindSpeed = speed;
            FakeWindOrientationDegr = orientationDegr;
        }
    }
}
