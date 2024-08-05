using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKO.WindSpeed.Interfaces
{
    public enum SensorDirection { NORTH, SOUTH, EAST, WEST };

    public interface ISoundDelaySensor
    {
        public void SetSoundDirection(SensorDirection direction);
        public void SetDistance(double distanceDelay);

        public double GetDelay();
    }
}
