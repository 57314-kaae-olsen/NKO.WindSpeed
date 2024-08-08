using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKO.WindSpeed.UltraSoundAnemometer
{
    internal class WinStatCalculator
    {
        public static WindStat GetWindStat(WindTimeSeries timeSeries)
        {
            var ts = timeSeries.Get2MinTimeSeries();

            if (ts==null || ts.Length == 0)
                return new WindStat(new Wind(DateTime.Now, Double.NaN, Double.NaN), new Wind(DateTime.Now, Double.NaN, Double.NaN));

            double wSum = 0.0;
            double oSum = 0.0;

            double wPeak = Double.MinValue;
            Wind? peakWind = null;

            foreach (Wind wind in ts) 
            {
                wSum += wind.Speed;
                double orient = wind.OrientationDegr;
                if (orient > 180) orient -= 360;
                oSum += orient;

                if (wind.Speed > wPeak)
                { 
                    wPeak = wind.Speed; 
                    peakWind = wind;
                }
            }
            double wMean = wSum / ts.Length;
            double oMean = oSum / ts.Length;
            DateTime timeMean = ts[ts.Length / 2].DateTime;
            Wind windMean = new Wind(timeMean, wMean, oMean);

            return new WindStat(windMean, peakWind);
        }
    }
}
