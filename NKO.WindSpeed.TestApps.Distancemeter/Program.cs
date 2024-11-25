// See https://aka.ms/new-console-template for more information
using Iot.Device.Bh1745;
using Iot.Device.Bmxx80.PowerMode;
using Iot.Device.Bmxx80;
using Iot.Device.CharacterLcd;
using Iot.Device.Pcx857x;
using System.Device.Gpio;
using System.Device.I2c;
using NKO.WindSpeed.SoundDelayHCSR04Sensor;

Console.WriteLine("Hello, World!");

using I2cDevice i2cLcd = I2cDevice.Create(new I2cConnectionSettings(1, 0x27));
using var driver = new Pcf8574(i2cLcd);
using var lcd = new Lcd2004(registerSelectPin: 0,
                        enablePin: 2,
                        dataPins: new int[] { 4, 5, 6, 7 },
                        backlightPin: 3,
                        backlightBrightness: 0.1f,
                        readWritePin: 1,
                        controller: new GpioController(PinNumberingScheme.Logical, driver));

int triggerPin = 23;
int echoPin = 24;

SoundDelayHCSR04Sensor soundDelay = new SoundDelayHCSR04Sensor();
soundDelay.SetPins(triggerPin, echoPin);

//lcd.SetCursorPosition(0, 1);
//lcd.Write("By NKO  :-)"); 

// R = specific gas constant = 287.058 J / (kg · K) for dry air
double R = 287.058;
// https://www.engineeringtoolbox.com/specific-heat-ratio-d_602.html
//Specific Heat Ratio of Air at Standard Atmospheric Pressure in SI Units:
double kappa = 1.401;

double temp = 22 + 273.15; // Kelvin

double soundSpeed = Math.Sqrt(kappa * R * temp);
Console.WriteLine($"Sound speed: {soundSpeed:#.##} m/s");

while (true)
{
    //Console.Clear();

    double delaySec = soundDelay.GetDelay();
    double delayMicroSec= delaySec * 1000000;
    double distance = delaySec * soundSpeed / 2.0 ; 
    double distancecm = distance * 100; // cm

    Console.WriteLine($"Delay: {delayMicroSec:#.##} \u00B5 s");
    Console.WriteLine($"Distance: {distancecm:0.#} cm");

    lcd.Clear();
    lcd.SetCursorPosition(0, 2);
    lcd.Write($"Delay: {delayMicroSec:#.##} my s");
    lcd.SetCursorPosition(0, 3);
    lcd.Write($"Distance: {distancecm:0.#} cm");
    Thread.Sleep(300);
}

