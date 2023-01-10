Console.WriteLine("Querying first website:");
using var client = new HttpClient();
var content = await client.GetStringAsync("http://info.cern.ch/");
Console.WriteLine(content);

Console.WriteLine("Querying website over HTTPS:");
content = await client.GetStringAsync("https://smallsrv.com/");
Console.WriteLine(content.Substring(0, 300));
