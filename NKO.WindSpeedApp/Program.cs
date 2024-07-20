// See https://aka.ms/new-console-template for more information
using NKO.WindSpeedApp;
using System.Numerics;
using Vector = NKO.WindSpeedApp.Vector;

Console.WriteLine("Hello, World!");


double windSpeed = 5;
double alphaDeg = 60;
double alphaRad = alphaDeg / 180.0 * Math.PI;

double l = 0.2;
SoundTravel soundTravel = new SoundTravel(l);
(double t1, double t2, double t3, double t4) = soundTravel.TravelTime(windSpeed, alphaDeg);
Console.WriteLine($" t1, t2, t1-t2 :   {t1}, {t2}, {t1 - t2}   \n t3, t4, t3-t4    {t3}, {t4}, {t3 - t4}");

double vs = soundTravel.SoundSpeed;


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



//var cosTeta = 0.5 * (vs * vs - vw * vw) / vw * (t1 - t2) / l;
//var teta = Math.Acos(cosTeta) * 180.0 / Math.PI;
//Console.WriteLine($"vw = {vw} {teta}");

//double dx1 = Math.Cos(alphaRad) * vw * t1;
//double dy1 = Math.Sin(alphaRad) * vw * t1 - l;
//double eq1l = Math.Sqrt(dx1 * dx1 + dy1 * dy1);
//double eq1r = vs * t1;

//Console.WriteLine($"eq1l == eq2r ?? {eq1l} {eq1r} ");


var A = new Point(0, 0);
var B = new Point(0, l);

Vector wind = new Vector(windSpeed * Math.Cos(alphaRad), windSpeed * Math.Sin(alphaRad));
Point PA = Point.Add(A, wind, t1);
double distBPA = Point.Distance(B, PA);
double distSound = vs* t1;
double diff = distBPA - distSound;

Console.WriteLine($" dist @ t1 : {distBPA} {distSound} {diff}");
Console.WriteLine();

