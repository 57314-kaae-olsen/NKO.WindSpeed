using Iot.Device.Ssd1351;
using System.Device.Gpio;
using System.Diagnostics;

namespace NKO.WindSpeed.SoundDelayHCSR04Sensor
{
    public class SoundDelayHCSR04Sensor : Interfaces.ISoundDelaySensor
    {
        private GpioPin _triggerPin { get; set; }
        private GpioPin _echoPin { get; set; }
        private Stopwatch _timeWatcher;

        public SoundDelayHCSR04Sensor()
        {
        }

        public void SetPins(int triggerPin, int echoPin)
        {
            GpioController controller = new GpioController();
            _timeWatcher = new Stopwatch();
            //initialize trigger pin.
            this._triggerPin = controller.OpenPin(triggerPin);
            this._triggerPin.SetPinMode(PinMode.Output);   //GpioPinDriveMode.Output);
            this._triggerPin.Write(PinValue.Low);  //GpioPinValue.Low);
            //initialize echo pin.
            this._echoPin = controller.OpenPin(echoPin);
            this._echoPin.SetPinMode(PinMode.Input);
        }

        public double GetDelay()
        {
            ManualResetEvent mre = new ManualResetEvent(false);
            mre.WaitOne(500);  //TODO: 500 ?
            _timeWatcher.Reset();
            //Send pulse
            this._triggerPin.Write(PinValue.High);
            mre.WaitOne(TimeSpan.FromMilliseconds(0.01));
            this._triggerPin.Write(PinValue.Low);
            return this.PulseIn(_echoPin, PinValue.High);
        }

        private double PulseIn(GpioPin echoPin, PinValue value)
        {
            var t = Task.Run(() =>
            {
                //Recieve pusle
                while (this._echoPin.Read() != value)
                {
                }
                _timeWatcher.Start();

                while (this._echoPin.Read() == value)
                {
                }
                _timeWatcher.Stop();
                double delay = _timeWatcher.Elapsed.TotalSeconds;
                return delay;
            });
            bool didComplete = t.Wait(TimeSpan.FromMilliseconds(100));
            if (didComplete)
            {
                return t.Result;
            }
            else
            {
                return 0.0;  //TODO: nullable
            }
        }
    }

    //public class HCSR04
    //{
    //    private GpioPin triggerPin { get; set; }
    //    private GpioPin echoPin { get; set; }
    //    private Stopwatch timeWatcher;

    //    public HCSR04(int triggerPin, int echoPin)
    //    {
    //        GpioController controller = GpioController.GetDefault();
    //        timeWatcher = new Stopwatch();
    //        //initialize trigger pin.
    //        this.triggerPin = controller.OpenPin(triggerPin);
    //        this.triggerPin.SetDriveMode(GpioPinDriveMode.Output);
    //        this.triggerPin.Write(GpioPinValue.Low);
    //        //initialize echo pin.
    //        this.echoPin = controller.OpenPin(echoPin);
    //        this.echoPin.SetDriveMode(GpioPinDriveMode.Input);
    //    }

    //    public double GetDistance()
    //    {
    //        ManualResetEvent mre = new ManualResetEvent(false);
    //        mre.WaitOne(500);
    //        timeWatcher.Reset();
    //        //Send pulse
    //        this.triggerPin.Write(GpioPinValue.High);
    //        mre.WaitOne(TimeSpan.FromMilliseconds(0.01));
    //        this.triggerPin.Write(GpioPinValue.Low);
    //        return this.PulseIn(echoPin, GpioPinValue.High);
    //    }

    //    private double PulseIn(GpioPin echoPin, GpioPinValue value)
    //    {
    //        var t = Task.Run(() =>
    //        {
    //            //Recieve pusle
    //            while (this.echoPin.Read() != value)
    //            {
    //            }
    //            timeWatcher.Start();

    //            while (this.echoPin.Read() == value)
    //            {
    //            }
    //            timeWatcher.Stop();
    //            //Calculating distance
    //            double distance = timeWatcher.Elapsed.TotalSeconds * 17000;
    //            return distance;
    //        });
    //        bool didComplete = t.Wait(TimeSpan.FromMilliseconds(100));
    //        if (didComplete)
    //        {
    //            return t.Result;
    //        }
    //        else
    //        {
    //            return 0.0;
    //        }
    //    }

    //}


}
