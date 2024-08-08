using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKO.WindSpeed.UltraSoundAnemometer
{
    public class WindTimeSeries
    {
        private Queue<Wind> _windTS = new Queue<Wind>();
        private object _queueLock = new object();
        private int _maxAgeMin = 10;

        public void AddNnewWind(Wind wind)
        {
            lock (_queueLock)
            {
                _windTS.Enqueue(wind);
                var now = DateTime.Now;
                while (now - _windTS.First().DateTime > TimeSpan.FromMinutes(_maxAgeMin))
                {
                    _windTS.Dequeue();
                }
            }
        }

        public Wind[] Get2MinTimeSeries()
        {
            int twoMin = 2;
            return GetXMinTimeSeries(twoMin);
        }

        public Wind[] Get10MinTimeSeries()
        {
            int tenMin = 10;
            return GetXMinTimeSeries(tenMin);
        }

        public Wind[] GetXMinTimeSeries(int iMinutes)
            {
            lock (_queueLock)
            {
                List<Wind> list = new List<Wind>();
                var now = DateTime.Now;

                foreach (Wind wind in _windTS)
                {
                    if (now - _windTS.First().DateTime < TimeSpan.FromMinutes(iMinutes))
                        list.Add(wind);
                    else
                        break;
                }
                return list.ToArray();
            }
        }
    }
}
