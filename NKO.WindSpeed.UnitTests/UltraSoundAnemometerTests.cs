using NKO.WindSpeed.Interfaces;
using NKO.WindSpeed.SoundDelayFakeSensor;
using NKO.WindSpeed.UltraSoundAnemometer;
using NuGet.Frameworks;

namespace NKO.WindSpeed.UnitTests
{
    [TestClass]
    public class UltraSoundAnemometerTests
    {
        [TestMethod]
        public void GetWindTest()
        {
            double speed = 2.5;
            double orientationDegr = Math.Atan(4.0 / 3.0) * 180.0 / Math.PI;
            FakeWind.SetWind(speed, orientationDegr);

            double soundSpeed = 4.0;
            double soundDistance = 5.0;

            UltraSoundAnemometerFake anemometer = new UltraSoundAnemometerFake(soundSpeed, soundDistance);

            Wind wind = anemometer.GetInstantaneousWind();

            Assert.IsNotNull(wind);
            Assert.AreEqual(speed, wind.Speed, 1.0e-8);
            Assert.AreEqual(orientationDegr, wind.OrientationDegr, 1.0e-8);
        }

    }
}
