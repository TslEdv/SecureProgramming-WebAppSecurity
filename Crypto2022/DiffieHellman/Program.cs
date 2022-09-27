using DiffieHellman;

var p = Helper.GetUserP();
var g = Helper.GetUserG();
var a = Helper.GetUserValueA();
var b = Helper.GetUserValueB();

var personX = Helper.ModPow(g, a, p);
var personY = Helper.ModPow(g, b, p);

Console.WriteLine($"PersonX received {personY} and computed: {Helper.ModPow(personX, b, p)}");

Console.WriteLine($"PersonY received {personX} computed: {Helper.ModPow(personY, a, p)}");
Helper.BruteForce(g,p,personX);
