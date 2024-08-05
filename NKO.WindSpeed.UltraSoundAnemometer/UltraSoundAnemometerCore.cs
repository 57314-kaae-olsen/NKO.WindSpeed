using NKO.WindSpeed.Interfaces;

namespace NKO.WindSpeed.UltraSoundAnemometer
{
    public class UltraSoundAnemometerCore<T> where T : new()
    {
        WindSensorDevice<T> _windSensorDevice;
        public UltraSoundAnemometerCore()
        {
            _windSensorDevice = new WindSensorDevice<T>();

        }

        public void SetSoundDistance(double soundDistance)
        {
            _windSensorDevice.SetSoundDistance(soundDistance);
        }

        public Wind GetInstantaneousWind()
        {
            return _windSensorDevice.GetInstantaneousWind();
        }
    }
}
