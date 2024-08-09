using NKO.WindSpeed.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKO.WindSpeed.UltraSoundAnemometer
{
    public class UltraSoundAnemometerHCSR04
    {
        private UltraSoundAnemometerCore<SoundDelayHCSR04Sensor.SoundDelayHCSR04Sensor> _anemometerCore;

        public UltraSoundAnemometerHCSR04()
        {
            _anemometerCore = new UltraSoundAnemometerCore<SoundDelayHCSR04Sensor.SoundDelayHCSR04Sensor>();

            // sound distance
            double soundDistance = 0.2;  //TODO: OK ?
            _anemometerCore.SetSoundDistance(soundDistance);

            // set tricker and echo pin 
            (ISoundDelaySensor sensorN, ISoundDelaySensor sensorS, ISoundDelaySensor sensorE, ISoundDelaySensor sensorW) = _anemometerCore.GetSensors();
            ((SoundDelayHCSR04Sensor.SoundDelayHCSR04Sensor)sensorN).SetPins(1, 2); // TODO:
            ((SoundDelayHCSR04Sensor.SoundDelayHCSR04Sensor)sensorS).SetPins(3, 4);
            ((SoundDelayHCSR04Sensor.SoundDelayHCSR04Sensor)sensorE).SetPins(5, 6);
            ((SoundDelayHCSR04Sensor.SoundDelayHCSR04Sensor)sensorW).SetPins(7, 8);
        }

        public Wind GetInstantaneousWind()
        {
            return _anemometerCore.GetInstantaneousWind();
        }

        public WindStat GetWindStatistics()
        {
            return _anemometerCore.GetWindStatistics();
        }


    }
}
