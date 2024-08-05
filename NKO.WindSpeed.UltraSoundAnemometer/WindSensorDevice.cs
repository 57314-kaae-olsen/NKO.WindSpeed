using NKO.WindSpeed.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKO.WindSpeed.UltraSoundAnemometer
{
    public class WindSensorDevice<T> where T : new()
    {
        private ISoundDelaySensor _sensorN;
        private ISoundDelaySensor _sensorS;
        private ISoundDelaySensor _sensorE;
        private ISoundDelaySensor _sensorW;


        private double _soundDistance;
        public WindSensorDevice()
        {
            _sensorN = (ISoundDelaySensor)new T();
            _sensorS = (ISoundDelaySensor)new T();
            _sensorE = (ISoundDelaySensor)new T();
            _sensorW = (ISoundDelaySensor)new T();
        }

        public void SetSoundDistance(double soundDistance)
        {
            _soundDistance = soundDistance;

            _sensorN.SetDistance(_soundDistance);
            _sensorS.SetDistance(_soundDistance);
            _sensorE.SetDistance(_soundDistance);
            _sensorW.SetDistance(_soundDistance);

            _sensorN.SetSoundDirection(SensorDirection.NORTH);
            _sensorS.SetSoundDirection(SensorDirection.SOUTH);
            _sensorE.SetSoundDirection(SensorDirection.EAST);
            _sensorW.SetSoundDirection(SensorDirection.WEST);
        }

        public Wind GetInstantaneousWind()
        {
            RawSensorData rawData = new RawSensorData(DateTime.Now, _soundDistance, _sensorN.GetDelay(), _sensorS.GetDelay(), _sensorE.GetDelay(), _sensorW.GetDelay());

            return RawSensorDataConverter.ConvertToWind(rawData);
        }

    }
}
