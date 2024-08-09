// See https://aka.ms/new-console-template for more information
using NKO.WindSpeed.SoundDelayFakeSensor;
using NKO.WindSpeed.UltraSoundAnemometer;

Console.WriteLine("Hello, World!");

bool FAKE = true;

if (FAKE)
{
    //-- Set fake wind
    double speed = 10;
    double orientationDegr = 30;
    FakeWind.SetWind(speed, orientationDegr);

    var anemometer = new UltraSoundAnemometerFake();

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
