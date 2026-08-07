using NKO.WindSpeed.Interfaces;
using NKO.WindSpeed.SoundDelayFakeSensor;
using NuGet.Frameworks;

namespace NKO.WindSpeed.UnitTests
{
    [TestClass]
    public class SoundDelayFakeSensorTests
    {
        [TestMethod]
        public void GetDelayTest()
        {
            FakeWind.SetWind(10, 30);

            ISoundDelaySensor fakeSensor = new SoundDelayFakeSensor.SoundDelayFakeSensor();

            double distanceSound = 0.2;
            ((SoundDelayFakeSensor.SoundDelayFakeSensor)fakeSensor).SetDistance(distanceSound);
            ((SoundDelayFakeSensor.SoundDelayFakeSensor)fakeSensor).SetSoundDirection(SoundDelayFakeSensor.SensorDirection.NORTH);

            double delay = fakeSensor.GetDelay();

            //-- check the distances by value of the delay
            SoundDelayFakeSensor.Math.Point fromPoint = new SoundDelayFakeSensor.Math.Point(0, -0.5 * distanceSound);
            SoundDelayFakeSensor.Math.Point toPoint = new SoundDelayFakeSensor.Math.Point(0,  0.5 * distanceSound);

            double alphaDeg = FakeWind.FakeWindOrientationDegr;
            double alphaRad = alphaDeg / 180.0 * Math.PI;
            double windSpeed = FakeWind.FakeWindSpeed;
            SoundDelayFakeSensor.Math.Vector windVector = new SoundDelayFakeSensor.Math.Vector(windSpeed * Math.Cos(alphaRad), windSpeed * Math.Sin(alphaRad));
            SoundDelayFakeSensor.Math.Point windPoint = SoundDelayFakeSensor.Math.Point.Add(fromPoint, windVector, delay);
            double distToPointWindPoint = SoundDelayFakeSensor.Math.Point.Distance(toPoint, windPoint);
            double distSound = ((SoundDelayFakeSensor.SoundDelayFakeSensor)fakeSensor).GetSoundSpeed() * delay;

            Assert.AreEqual(distSound, distToPointWindPoint, 1.0e-8);
        }

        [TestMethod]
        public void GetDelayTest_NORTH()
        {
            double vs = 4.0;
            double orientDegr = Math.Atan(4.0 / 3.0) * 180.0 / Math.PI;

            double distanceSound = 5.0;

            FakeWind.SetWind(2.5, orientDegr);

            ISoundDelaySensor fakeSensor = new SoundDelayFakeSensor.SoundDelayFakeSensor();
            ((SoundDelayFakeSensor.SoundDelayFakeSensor)fakeSensor).SetSoundSpeed(vs);

            ((SoundDelayFakeSensor.SoundDelayFakeSensor)fakeSensor).SetDistance(distanceSound);
            ((SoundDelayFakeSensor.SoundDelayFakeSensor)fakeSensor).SetSoundDirection(SoundDelayFakeSensor.SensorDirection.NORTH);

            double delay = fakeSensor.GetDelay();

            //-- check the distances by value of the delay
            SoundDelayFakeSensor.Math.Point fromPoint = new SoundDelayFakeSensor.Math.Point(0, -0.5 * distanceSound);
            SoundDelayFakeSensor.Math.Point toPoint = new SoundDelayFakeSensor.Math.Point(0, 0.5 * distanceSound);

            double alphaDeg = FakeWind.FakeWindOrientationDegr;
            double alphaRad = alphaDeg / 180.0 * Math.PI;
            double windSpeed = FakeWind.FakeWindSpeed;
            SoundDelayFakeSensor.Math.Vector windVector = new SoundDelayFakeSensor.Math.Vector(windSpeed * Math.Cos(alphaRad), windSpeed * Math.Sin(alphaRad));
            SoundDelayFakeSensor.Math.Point windPoint = SoundDelayFakeSensor.Math.Point.Add(fromPoint, windVector, delay);
            double distToPointWindPoint = SoundDelayFakeSensor.Math.Point.Distance(toPoint, windPoint);
            double distSound = ((SoundDelayFakeSensor.SoundDelayFakeSensor)fakeSensor).GetSoundSpeed() * delay;

            Assert.AreEqual(distSound, distToPointWindPoint, 1.0e-8);
        }

        [TestMethod]
        public void GetDelayTest_SOUTH()
        {
            double vs = 4.0;
            double orientDegr = Math.Atan(4.0 / 3.0) * 180.0 / Math.PI;

            double distanceSound = 5.0;

            FakeWind.SetWind(2.5, orientDegr);

            ISoundDelaySensor fakeSensor = new SoundDelayFakeSensor.SoundDelayFakeSensor();
            ((SoundDelayFakeSensor.SoundDelayFakeSensor)fakeSensor).SetSoundSpeed(vs);

            ((SoundDelayFakeSensor.SoundDelayFakeSensor)fakeSensor).SetDistance(distanceSound);
            ((SoundDelayFakeSensor.SoundDelayFakeSensor)fakeSensor).SetSoundDirection(SoundDelayFakeSensor.SensorDirection.SOUTH);

            double delay = fakeSensor.GetDelay();

            //-- check the distances by value of the delay
            SoundDelayFakeSensor.Math.Point fromPoint = new SoundDelayFakeSensor.Math.Point(0, 0.5 * distanceSound);
            SoundDelayFakeSensor.Math.Point toPoint = new SoundDelayFakeSensor.Math.Point(0, -0.5 * distanceSound);

            double alphaDeg = FakeWind.FakeWindOrientationDegr;
            double alphaRad = alphaDeg / 180.0 * Math.PI;
            double windSpeed = FakeWind.FakeWindSpeed;
            SoundDelayFakeSensor.Math.Vector windVector = new SoundDelayFakeSensor.Math.Vector(windSpeed * Math.Cos(alphaRad), windSpeed * Math.Sin(alphaRad));
            SoundDelayFakeSensor.Math.Point windPoint = SoundDelayFakeSensor.Math.Point.Add(fromPoint, windVector, delay);
            double distToPointWindPoint = SoundDelayFakeSensor.Math.Point.Distance(toPoint, windPoint);
            double distSound = ((SoundDelayFakeSensor.SoundDelayFakeSensor)fakeSensor).GetSoundSpeed() * delay;

            Assert.AreEqual(distSound, distToPointWindPoint, 1.0e-8);
        }



    }

    //https://testgrid.io/blog/nunit-vs-xunit-vs-mstest/
}