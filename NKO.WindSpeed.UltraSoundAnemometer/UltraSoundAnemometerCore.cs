using NKO.WindSpeed.Interfaces;

namespace NKO.WindSpeed.UltraSoundAnemometer
{
    public class UltraSoundAnemometerCore<T> where T : new()
    {
        WindSensorDevice<T> _windSensorDevice;

        WindTimeSeries _winTimeSeries = new WindTimeSeries();

        Timer _timer; 

        public UltraSoundAnemometerCore()
        {
            _windSensorDevice = new WindSensorDevice<T>();
            _timer = new Timer(OnTimer, null, Timeout.Infinite, Timeout.Infinite);
            _timer.Change(0, 100);   //TODO: 100 milli seconds => 10 Hz
        }

        public void SetSoundDistance(double soundDistance)
        {
            _windSensorDevice.SetSoundDistance(soundDistance);
        }

        public Wind GetInstantaneousWind()
        {
            return _windSensorDevice.GetInstantaneousWind();
        }

        public WindStat GetWindStatistics()
        {
            return WinStatCalculator.GetWindStat(_winTimeSeries);
        }

        private void OnTimer(object state)
        {
            Wind newWind = GetInstantaneousWind();
            _winTimeSeries.AddNnewWind(newWind);
        }

        public (ISoundDelaySensor sensorN, ISoundDelaySensor sensorS, ISoundDelaySensor sensorE, ISoundDelaySensor sensorW) GetSensors()
        {
            return _windSensorDevice.GetSensors();
        }

    }
}
