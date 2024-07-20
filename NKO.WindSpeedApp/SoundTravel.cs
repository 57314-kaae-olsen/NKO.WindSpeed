using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKO.WindSpeedApp
{
    public class SoundTravel
    {
        private Point _A;
        private Point _B;
        private Point _C;
        private Point _D;

        public double SoundSpeed = 300;
        public SoundTravel(double distance)
        {
            _A = new Point(0, 0);
            _B = new Point(0, distance);

            _C = new Point(-distance / 2, 0);
            _D = new Point(+distance / 2, 0);
        }


        public (double t1, double t2, double t3, double t4) TravelTime(double windSpeed, double alphaDeg)
        {
            double alphaRad = alphaDeg / 180.0 * Math.PI;
            Vector wind = new Vector(windSpeed * Math.Cos(alphaRad), windSpeed * Math.Sin(alphaRad));

            double dt = 1.0e-6;


            // North - South
            double t1 = 0;
            {
                double d1 = double.MaxValue;
                do
                {
                    d1 = _GetDiffByTime_AB(t1, wind);
                    double dm1 = _GetDiffByTime_AB(t1 - dt, wind);
                    double dp1 = _GetDiffByTime_AB(t1 + dt, wind);
                    double slope1 = (dp1 - dm1) / (2 * dt);
                    t1 = t1 - d1 / slope1;
                } while (Math.Abs(d1) > 1.0e-10);
            }


            double t2 = 0;
            {
                double d2 = double.MaxValue;
                do
                {
                    d2 = _GetDiffByTime_BA(t2, wind);
                    double dm2 = _GetDiffByTime_BA(t2 - dt, wind);
                    double dp2 = _GetDiffByTime_BA(t2 + dt, wind);
                    double slope2 = (dp2 - dm2) / (2 * dt);
                    t2 = t2 - d2 / slope2;
                    //Console.WriteLine($"{time2} {d2} {slope2} {-d2 / slope2}");
                } while (Math.Abs(d2) > 1.0e-6);
            }

            // East - West
            double t3 = 0;
            {
                double d3 = double.MaxValue;
                do
                {
                    d3 = _GetDiffByTime_CD(t3, wind);
                    double dm3 = _GetDiffByTime_CD(t3 - dt, wind);
                    double dp3 = _GetDiffByTime_CD(t3 + dt, wind);
                    double slope3 = (dp3 - dm3) / (2 * dt);
                    t3 = t3 - d3 / slope3;
                } while (Math.Abs(d3) > 1.0e-10);
            }

            double t4 = 0;
            {
                double d4 = double.MaxValue;
                do
                {
                    d4 = _GetDiffByTime_DC(t4, wind);
                    double dm4 = _GetDiffByTime_DC(t4 - dt, wind);
                    double dp4 = _GetDiffByTime_DC(t4 + dt, wind);
                    double slope4 = (dp4 - dm4) / (2 * dt);
                    t4 = t4 - d4 / slope4;
                } while (Math.Abs(d4) > 1.0e-10);
            }

            return (t1, t2, t3, t4);
        }

        private double _GetDiffByTime_AB(double time, Vector wind)
        {
            Point PA = Point.Add(_A, wind, time);
            double distBPA = Point.Distance(_B, PA);
            double distSound = SoundSpeed * time;
            double diff = distBPA - distSound;

            return diff;
        }

        private double _GetDiffByTime_BA(double time, Vector wind)
        {
            Point PB = Point.Add(_B, wind, time);
            double distAPB = Point.Distance(_A, PB);
            double distSound = SoundSpeed * time;
            double diff = distAPB - distSound;

            return diff;
        }

        private double _GetDiffByTime_CD(double time, Vector wind)
        {
            Point PC = Point.Add(_C, wind, time);
            double distDPC = Point.Distance(_D, PC);
            double distSound = SoundSpeed * time;
            double diff = distDPC - distSound;

            return diff;
        }

        private double _GetDiffByTime_DC(double time, Vector wind)
        {
            Point PD = Point.Add(_D, wind, time);
            double distCPD = Point.Distance(_C, PD);
            double distSound = SoundSpeed * time;
            double diff = distCPD - distSound;

            return diff;
        }


    }
}
