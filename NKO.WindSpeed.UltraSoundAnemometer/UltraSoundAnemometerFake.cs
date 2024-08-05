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

        public UltraSoundAnemometerFake()
        {
            double soundDistance = 0.2;  //TODO: OK ?
            _anemometerCore = new UltraSoundAnemometerCore<SoundDelayFakeSensor.SoundDelayFakeSensor> ();
            _anemometerCore.SetSoundDistance(soundDistance);
        }

        public Wind GetInstantaneousWind()
        {
            return _anemometerCore.GetInstantaneousWind ();
        }
    }
}
