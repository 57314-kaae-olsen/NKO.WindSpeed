// See https://aka.ms/new-console-template for more information
using NKO.WindSpeed.SoundDelayFakeSensor;
using NKO.WindSpeed.UltraSoundAnemometer;

Console.WriteLine("Hello, World!");

bool FAKE = true;

if (FAKE)
{
    //-- Set fake wind
    double speed = 2.5;
    double orientationDegr = Math.Atan(4.0 / 3.0) * 180.0 / Math.PI;
    FakeWind.SetWind(speed, orientationDegr);

    double soundSpeed = 4.0;
    double soundDistance = 5.0;
    var anemometer = new UltraSoundAnemometerFake(soundSpeed, soundDistance);

    //-- Let the anemometer work some seconds
    var task = Task.Run(() => Thread.Sleep(10000));
    Task.WaitAll(task);

    //-- Get wind statistics from anemometer
    var windStat = anemometer.GetWindStatistics();

    //--Dump wind statistics to console
    Console.WriteLine(windStat.ToString());
    Console.WriteLine("Bye");
}
else
{
    try
    {
        var anemometer = new UltraSoundAnemometerHCSR04();
        while (true)
        {
            //-- Let the anemometer work some seconds
            var task = Task.Run(() => Thread.Sleep(10000));
            Task.WaitAll(task);

            //-- Get wind statistics from anemometer
            var windStat = anemometer.GetWindStatistics();

            //-- Dump wind statistics to console
            Console.WriteLine(windStat.ToString());
        }
    }
    catch(Exception ex)
    { 
        Console.WriteLine(ex.Message);
        Console.WriteLine(ex.StackTrace);
    }

}
