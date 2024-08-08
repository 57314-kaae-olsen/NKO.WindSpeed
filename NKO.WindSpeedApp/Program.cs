// See https://aka.ms/new-console-template for more information
using NKO.WindSpeed.UltraSoundAnemometer;

Console.WriteLine("Hello, World!");

var ultraSound = new UltraSoundAnemometerFake();

var task = Task.Run(() => Thread.Sleep(10000));
Task.WaitAll(task);

Console.WriteLine(ultraSound.GetWindStatistics().ToString());


Console.WriteLine("Bye");