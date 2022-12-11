#if NO_GLOBALIZATION
Console.WriteLine("There no globalization");
#else
Console.WriteLine("With globalizations");
#endif

Console.WriteLine("Enter you name:");
var greeting = Console.ReadLine();
Console.WriteLine($"Hello {greeting}!");
double num = 5.2;

Console.WriteLine("Querying first website:");
using var client = new HttpClient();
var content = await client.GetStringAsync("http://info.cern.ch/");
Console.WriteLine(content);
#if NO_GLOBALIZATION
Console.WriteLine($"Number 5.2 with invariant culture {num.ToString("F2")}");
#else
Console.WriteLine($"Number 5.2 in ru-RU locale {num.ToString("F2", System.Globalization.CultureInfo.GetCultureInfo("ru-ru"))}");
#endif
