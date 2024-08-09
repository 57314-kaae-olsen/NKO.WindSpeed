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
            double soundSpeed = GetSoundSpeed(raw);

            //--Wind speed
            double dist2 = raw.SoundDistance * raw.SoundDistance;
            double windSpeedNS = Math.Sqrt(soundSpeed * soundSpeed - dist2 / (raw.DelayN * raw.DelayS));
            double windSpeedEW = Math.Sqrt(soundSpeed * soundSpeed - dist2 / (raw.DelayE * raw.DelayW));

            //Angle N->S & E->W
            double sinAlphaNS = (soundSpeed*soundSpeed * raw.DelayS * raw.DelayS - windSpeedNS * windSpeedNS * raw.DelayS * raw.DelayS - dist2)
                / (2 * raw.DelayS* raw.SoundDistance * windSpeedNS);
            double alphaRadNS = Math.Asin(sinAlphaNS);

            double cosAlphaEW = (soundSpeed * soundSpeed * raw.DelayW * raw.DelayW - windSpeedEW * windSpeedEW * raw.DelayW * raw.DelayW - dist2)
                / (2 * raw.DelayW * raw.SoundDistance * windSpeedEW);
            double alphaRadEW = Math.Acos(cosAlphaEW);

            return new Wind(raw.DateTime, windSpeedNS, alphaRadNS / Math.PI * 180.0);
          //return new Wind(raw.DateTime, windSpeedEW, alphaRadEW / Math.PI * 180.0);

        }

        public static double GetSoundSpeed(RawSensorData raw)
        {
            double soundSpeed = raw.SoundDistance / ((raw.DelayN + raw.DelayS + raw.DelayE + raw.DelayW) / 4);

            bool ok = true;
            double dist2 = raw.SoundDistance * raw.SoundDistance;
            do {
                double windSpeed2NS = soundSpeed * soundSpeed - dist2 / (raw.DelayN * raw.DelayS);
                double windSpeed2EW = soundSpeed * soundSpeed - dist2 / (raw.DelayE * raw.DelayW);

                double windSpeedNS = Math.Sqrt(soundSpeed * soundSpeed - dist2 / (raw.DelayN * raw.DelayS));
                double sinAlphaNS = (soundSpeed * soundSpeed * raw.DelayS * raw.DelayS - windSpeedNS * windSpeedNS * raw.DelayS * raw.DelayS - dist2)
                                    / (2 * raw.DelayS * raw.SoundDistance * windSpeedNS);

                double windSpeedEW = Math.Sqrt(soundSpeed * soundSpeed - dist2 / (raw.DelayE * raw.DelayW));
                double cosAlphaEW = (soundSpeed * soundSpeed * raw.DelayW * raw.DelayW - windSpeedEW * windSpeedEW * raw.DelayW * raw.DelayW - dist2)
                                / (2 * raw.DelayW * raw.SoundDistance * windSpeedEW);

                ok = windSpeed2NS > 0 && windSpeed2EW > 0 && sinAlphaNS < 1 && cosAlphaEW < 1;
                if (!ok)
                    soundSpeed += 0.01;
            }
            while (!ok) ;

            double ds = 1.0e-5;
            double diff = WindAngleDiff(raw, soundSpeed);
            while (Math.Abs(diff) > 1.0e-10)  //TODO: MaxIter
            {
                double diffP = WindAngleDiff(raw, soundSpeed + ds);
                double diffM = WindAngleDiff(raw, soundSpeed - ds);
                double slope = (diffP - diffM) / (2 * ds);
                soundSpeed -= diff / slope;
                diff = WindAngleDiff(raw, soundSpeed);
            }
            return soundSpeed;
        }

        private static double WindAngleDiff(RawSensorData raw, double soundSpeed)
        {
            double dist2 = raw.SoundDistance * raw.SoundDistance;
            double windSpeedNS = Math.Sqrt(soundSpeed * soundSpeed - dist2 / (raw.DelayN * raw.DelayS));
            double windSpeedEW = Math.Sqrt(soundSpeed * soundSpeed - dist2 / (raw.DelayE * raw.DelayW));

            double sinAlphaNS = (soundSpeed * soundSpeed * raw.DelayS * raw.DelayS - windSpeedNS * windSpeedNS * raw.DelayS * raw.DelayS - dist2)
                / (2 * raw.DelayS * raw.SoundDistance * windSpeedNS);
            double alphaRadNS = Math.Asin(sinAlphaNS);
            double alphaDegNS = alphaRadNS / Math.PI * 180;

            double cosAlphaEW = (soundSpeed * soundSpeed * raw.DelayW * raw.DelayW - windSpeedEW * windSpeedEW * raw.DelayW * raw.DelayW - dist2)
                / (2 * raw.DelayW * raw.SoundDistance * windSpeedEW);
            double alpphaRadEW = Math.Acos(cosAlphaEW);

            double diff = alpphaRadEW - alphaRadNS;

            return diff;
        }
    }
}
