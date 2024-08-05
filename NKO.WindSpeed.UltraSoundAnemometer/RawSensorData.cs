using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKO.WindSpeed.UltraSoundAnemometer
{
    internal class RawSensorData
    {
        public DateTime DateTime { get; private set; }
        public double SoundDistance { get; private set; }
        public double DelayN { get; private set; }
        public double DelayS { get; private set; }
        public double DelayE { get; private set; }
        public double DelayW { get; private set; }

        public RawSensorData(DateTime dateTime, double soundDistance, double delayN, double delayS, double delayE, double delayW) 
        {
            DateTime = dateTime;
            SoundDistance = soundDistance;
            DelayN = delayN;
            DelayS = delayS;
            DelayE = delayE;
            DelayW = delayW;
        }
    }
}
