using System.Globalization;

Console.WriteLine("With globalizations");

Console.WriteLine("Enter you name:");
var greeting = Console.ReadLine();
Console.WriteLine($"Hello {greeting}!");
double num = 5.2;

var parsed = int.TryParse("-120", NumberStyles.Integer, CultureInfo.InvariantCulture, out var reversedOffset);
Console.WriteLine($"Parsed -120 invariant culture: {parsed}, result is {reversedOffset}");
Console.WriteLine($"Number 5.2 in ru-RU locale {num.ToString("F2", System.Globalization.CultureInfo.GetCultureInfo("ru-ru"))}");
