using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKO.WindSpeed.UltraSoundAnemometer
{
    internal class RawSensorDataConverter
    {
        public static Wind ConvertToWind(RawSensorData raw)
        {
            double dist2 = raw.SoundDistance * raw.SoundDistance;
            //double soundSpeed = raw.SoundDistance / ((raw.DelayN + raw.DelayS + raw.DelayE + raw.DelayW) / 4);
            double soundSpeed = 300; //TODO:  ? ? ?

            double windSpeedNS = Math.Sqrt(soundSpeed * soundSpeed - dist2 / (raw.DelayN * raw.DelayS));
            double windSpeedEW = Math.Sqrt(soundSpeed * soundSpeed - dist2 / (raw.DelayE * raw.DelayW));

            double sinAlphaNS = (soundSpeed*soundSpeed * raw.DelayS * raw.DelayS - windSpeedNS * windSpeedNS * raw.DelayS * raw.DelayS - dist2)
                / (2 * raw.DelayS* raw.SoundDistance * windSpeedNS);
            double alpphaRadNS = Math.Asin(sinAlphaNS);

            double sinAlphaEW = (soundSpeed * soundSpeed * raw.DelayW * raw.DelayW - windSpeedEW * windSpeedEW * raw.DelayW * raw.DelayW - dist2)
                / (2 * raw.DelayW * raw.SoundDistance * windSpeedEW);
            double alpphaRadEW = Math.Asin(sinAlphaEW);

            if (Math.Abs(alpphaRadNS) < Math.Abs(alpphaRadEW))  //TODO: IMPORTANT
            {
                return new Wind(raw.DateTime, windSpeedNS, alpphaRadNS / Math.PI * 180.0);
            }
            else
            {
                return new Wind(raw.DateTime, windSpeedEW, alpphaRadEW / Math.PI * 180.0);
            }

        }
    }
}
