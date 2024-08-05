using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKO.WindSpeed.UltraSoundAnemometer
{
    public class Wind
    {
        public Wind(DateTime dateTime, double speed, double orientation)
        {
            DateTime = dateTime;
            Speed = speed;
            OrientationDegr = orientation; 
        }

        public DateTime DateTime { get; private set; }
        public double Speed { get; private set; }
        public double OrientationDegr { get; private set; }

        public double CompassDirectionDegr
        {
            get
            {
                return 90 - OrientationDegr;
                //return OrientationDegr / 180.0 * Math.PI;
            }
        }

        public override string ToString()  // // "Speed = 7.1 m/s Compass direction = 65 deg"
        {
            return $"Wind speed = {Speed:.0} m/s,   Compass direction = {CompassDirectionDegr} deg";
        }
    }
}
