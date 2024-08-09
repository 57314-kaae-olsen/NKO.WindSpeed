
using NKO.WindSpeed.SoundDelayFakeSensor.Math;

namespace NKO.WindSpeed.SoundDelayFakeSensor
{
    public enum SensorDirection { NORTH, SOUTH, EAST, WEST };

    public class SoundDelayFakeSensor : Interfaces.ISoundDelaySensor
    {
        private double _distanceDelay;

        private Point? _fromPoint;
        private Point? _toPoint;

        double _soundSpeed;
        public SoundDelayFakeSensor()
        {
            _soundSpeed = 300;
        }

        public void SetDistance(double distanceDelay)
        {
            _distanceDelay = distanceDelay;
        }

        public void SetSoundDirection(SensorDirection direction) 
        { 
            if (_distanceDelay == 0) { throw new Exception("distanceDelay is null"); }

            switch (direction)
            {
                case SensorDirection.NORTH:
                    _fromPoint = new Point(0, -_distanceDelay / 2.0);
                    _toPoint = new Point(0, _distanceDelay / 2.0);
                    break;
                case SensorDirection.SOUTH:
                    _fromPoint = new Point(0, _distanceDelay / 2.0);
                    _toPoint = new Math.Point(0, -_distanceDelay / 2.0);
                    break;
                case SensorDirection.EAST:
                    _fromPoint = new Point(-_distanceDelay / 2.0, 0);
                    _toPoint = new Point(_distanceDelay / 2.0, 0);
                    break;
                case SensorDirection.WEST:
                    _fromPoint = new Point(_distanceDelay / 2.0, 0);
                    _toPoint = new Math.Point(-_distanceDelay / 2.0, 0);
                    break;
            }
        }

        public double GetDelay()
        {
            double windSpeed = FakeWind.FakeWindSpeed;
            double alphaDeg = FakeWind.FakeWindOrientationDegr;

            double alphaRad = alphaDeg / 180.0 * System.Math.PI;
            Vector wind = new Vector(windSpeed * System.Math.Cos(alphaRad), windSpeed * System.Math.Sin(alphaRad));

            double dt = 1.0e-6;

            double time = 0;
            {
                double d = double.MaxValue;
                do
                {
                    d = _GetDiffByTime(time, wind);
                    double dm = _GetDiffByTime(time - dt, wind);
                    double dp = _GetDiffByTime(time + dt, wind);
                    double slope = (dp - dm) / (2 * dt);
                    time = time - d / slope;
                } while (System.Math.Abs(d) > 1.0e-10);
            }


            return time; 
        }

        public double GetSoundSpeed()
        {
            return _soundSpeed;
        }

        private double _GetDiffByTime(double time, Vector wind)
        {
            //-- Sound center point
            Point center = Point.Add(_fromPoint, wind, time);

            //-- Distance from ToPoint from sound center point
            double distToP = Point.Distance(_toPoint, center);

            //-- sound wave travel distance
            double distSoundWave = _soundSpeed * time;

            //-- difference sound wave travel distance and ToPoint from sound center point
            double diff = distToP - distSoundWave;

            return diff;
        }

    }
}
