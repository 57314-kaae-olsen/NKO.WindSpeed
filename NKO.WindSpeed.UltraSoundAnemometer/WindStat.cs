using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKO.WindSpeed.UltraSoundAnemometer
{
    public class WindStat
    {
        public WindStat( Wind windMean2min, Wind windPeak2min ) 
        { 
            WindMean2Min = windMean2min;
            WindPeak2Min = windPeak2min;
        }
        public Wind WindMean2Min { get; private set; }
        //Wind Wind10Min 

        public Wind WindPeak2Min { get; private set; }
        //Wind WIndPeak10Min

        public override string ToString()
        {
            return $"Mean 2 minutes:\n{WindMean2Min.ToString()}\n\n Peak 2minutes:\n{WindPeak2Min.ToString()} ";
        }
    }
}
