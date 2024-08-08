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
            _timer.Change(0, 10);

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

    }
}
