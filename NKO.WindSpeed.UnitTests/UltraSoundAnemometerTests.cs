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
            double speed = 10;
            double orientationDegr = 30;
            FakeWind.SetWind(speed, orientationDegr);

            UltraSoundAnemometerFake anemometer = new UltraSoundAnemometerFake();

            Wind wind = anemometer.GetInstantaneousWind();

            Assert.IsNotNull(wind);
            Assert.AreEqual(speed, wind.Speed, 1.0e-8);
            Assert.AreEqual(orientationDegr, wind.OrientationDegr, 1.0e-8);
        }

    }
}
