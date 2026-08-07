using NKO.WindSpeed.Interfaces;
using NKO.WindSpeed.SoundDelayFakeSensor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKO.WindSpeed.UltraSoundAnemometer
{
    public class UltraSoundAnemometerFake
    {
        private UltraSoundAnemometerCore<SoundDelayFakeSensor.SoundDelayFakeSensor> _anemometerCore;

        public UltraSoundAnemometerFake(double soundSpeed, double soundDistance)
        {
            _anemometerCore = new UltraSoundAnemometerCore<SoundDelayFakeSensor.SoundDelayFakeSensor> ();

            _anemometerCore.SetSoundDistance(soundDistance);

            (ISoundDelaySensor sensorN, ISoundDelaySensor sensorS, ISoundDelaySensor sensorE, ISoundDelaySensor sensorW) = _anemometerCore.GetSensors();
            ((SoundDelayFakeSensor.SoundDelayFakeSensor)sensorN).SetDistance(soundDistance);
            ((SoundDelayFakeSensor.SoundDelayFakeSensor)sensorS).SetDistance(soundDistance);
            ((SoundDelayFakeSensor.SoundDelayFakeSensor)sensorE).SetDistance(soundDistance);
            ((SoundDelayFakeSensor.SoundDelayFakeSensor)sensorW).SetDistance(soundDistance);

            ((SoundDelayFakeSensor.SoundDelayFakeSensor)sensorN).SetSoundSpeed(soundSpeed);
            ((SoundDelayFakeSensor.SoundDelayFakeSensor)sensorS).SetSoundSpeed(soundSpeed);
            ((SoundDelayFakeSensor.SoundDelayFakeSensor)sensorE).SetSoundSpeed(soundSpeed);
            ((SoundDelayFakeSensor.SoundDelayFakeSensor)sensorW).SetSoundSpeed(soundSpeed);

            ((SoundDelayFakeSensor.SoundDelayFakeSensor)sensorN).SetSoundDirection(SensorDirection.NORTH);
            ((SoundDelayFakeSensor.SoundDelayFakeSensor)sensorS).SetSoundDirection(SensorDirection.SOUTH);
            ((SoundDelayFakeSensor.SoundDelayFakeSensor)sensorE).SetSoundDirection(SensorDirection.EAST);
            ((SoundDelayFakeSensor.SoundDelayFakeSensor)sensorW).SetSoundDirection(SensorDirection.WEST);
        }

        public Wind GetInstantaneousWind()
        {
            return _anemometerCore.GetInstantaneousWind ();
        }

        public WindStat GetWindStatistics()
        {
            return _anemometerCore.GetWindStatistics();
        }

    }
}
