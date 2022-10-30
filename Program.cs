Console.WriteLine("Enter you name:");
var greeting = Console.ReadLine();
Console.WriteLine($"Hello {greeting}!");
double num = 5.2;
#if NO_GLOBALIZATION
Console.WriteLine($"There no globalization {num.ToString("F2")}");
#else
Console.WriteLine($"Number with globalization {num.ToString("F2", System.Globalization.CultureInfo.GetCultureInfo("ru-ru"))}");
#endif