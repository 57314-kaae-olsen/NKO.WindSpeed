﻿// See https://aka.ms/new-console-template for more information
using NKO.WindSpeedApp;
using System.Numerics;
using Vector = NKO.WindSpeedApp.Vector;

Console.WriteLine("Hello, World!");


var ultraSound = new UltraSoundAnemometer<Sensor>();
double xx = ultraSound.GetWindSpeed();

double windSpeed = 5;
double alphaDeg = 60;
double alphaRad = alphaDeg / 180.0 * Math.PI;

double l = 0.2;
double soundSpeed = 300;
SoundDelayCalculator soundDelayCalc = new SoundDelayCalculator(l, soundSpeed);
(double t1, double t2, double t3, double t4) = soundDelayCalc.Delay(windSpeed, alphaDeg);
Console.WriteLine($" t1, t2, t1-t2 :   {t1}, {t2}, {t1 - t2}   \n t3, t4, t3-t4    {t3}, {t4}, {t3 - t4}");

double vs = soundSpeed;

double vsByTMean34 = l / ((t3 + t4) / 2);
Console.WriteLine($"SoundSpeed 3->4 = {vs}   SoundSpeedByTMean = {vsByTMean34}   Pct = {(vsByTMean34 - vs) / vs * 100.0}");


double vsByTMean12 = l / ((t1 + t2) / 2);
Console.WriteLine($"SoundSpeed 1->2 = {vs}   SoundSpeedByTMean = {vsByTMean12}   Pct = {(vsByTMean12 - vs) / vs * 100.0}");


double vsByTMean1234 = l / ((t1 + t2 + t3 + t4) / 4);
Console.WriteLine($"SoundSpeed 1->4= {vs}   SoundSpeedByTMean = {vsByTMean1234}   Pct = {(vsByTMean1234- vs)/vs*100.0}");


// East west 
double helpEW = vs * vs  - l * l / (t1 * t2);
double vwEW = Math.Sqrt(helpEW);
double cosAlfaEW = (vs * vs * t2 * t2 - vwEW * vwEW * t2 * t2 - l * l) / (2 * t2 * l * vwEW);
double alfaEW = Math.Acos(cosAlfaEW) * 180.0 / Math.PI;
Console.WriteLine($"EAST-WEST vw = {vwEW} alfa = {alfaEW}");


// NORTH SOUTH
double helpNS = vs * vs - l * l / (t3 * t4);
double vwNS = Math.Sqrt(helpNS);
double sinAlfaNS = (vs * vs * t4 * t4 - vwNS * vwNS * t4 * t4 - l * l) / (2 * t4 * l * vwNS);
double alfaNS = Math.Asin(sinAlfaNS) * 180.0 / Math.PI;
Console.WriteLine($"NORTH SOUTH vw = {vwNS} alfa = {alfaNS}");

var A = new Point(0, 0);
var B = new Point(0, l);

Vector wind = new Vector(windSpeed * Math.Cos(alphaRad), windSpeed * Math.Sin(alphaRad));
Point PA = Point.Add(A, wind, t1);
double distBPA = Point.Distance(B, PA);
double distSound = vs* t1;
double diff = distBPA - distSound;

Console.WriteLine($" dist @ t1 : {distBPA} {distSound} {diff}");
Console.WriteLine();

