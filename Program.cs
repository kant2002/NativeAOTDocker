using System.Globalization;
using System.IO.Compression;

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
var parsed = int.TryParse("-120", NumberStyles.Integer, CultureInfo.InvariantCulture, out var reversedOffset);
Console.WriteLine($"Parsed -120 invariant culture: {parsed}, result is {reversedOffset}");
#if NO_GLOBALIZATION
Console.WriteLine($"Number 5.2 with invariant culture {num.ToString("F2")}");
#else
Console.WriteLine($"Number 5.2 in ru-RU locale {num.ToString("F2", System.Globalization.CultureInfo.GetCultureInfo("ru-ru"))}");
#endif

// Testing System.IO.Compression native libraries.
TestGzip();
TestBrotli();
TestDeflate();

static void TestGzip()
{
    var sourceStream = new MemoryStream();
    using (var stringWriter = new StreamWriter(sourceStream, leaveOpen: true))
    {
        stringWriter.WriteLine("This is text");
    }

    sourceStream.Position = 0;

    var destinationStream = new MemoryStream();
    using var compressor = new GZipStream(destinationStream, CompressionMode.Compress);
    sourceStream.CopyTo(compressor);
    destinationStream.Position = 0;

    Console.WriteLine($"Length of gzip archive is {destinationStream.Length}");
}

static void TestBrotli()
{
    var sourceStream = new MemoryStream();
    using (var stringWriter = new StreamWriter(sourceStream, leaveOpen: true))
    {
        stringWriter.WriteLine("This is text");
    }

    sourceStream.Position = 0;

    var destinationStream = new MemoryStream();
    using (var compressor = new BrotliStream(destinationStream, CompressionMode.Compress, leaveOpen: true))
    {
        sourceStream.CopyTo(compressor);
    }

    destinationStream.Position = 0;

    Console.WriteLine($"Length of brotli archive is {destinationStream.Length}");
}

static void TestDeflate()
{
    var sourceStream = new MemoryStream();
    using (var stringWriter = new StreamWriter(sourceStream, leaveOpen: true))
    {
        stringWriter.WriteLine("This is text");
    }

    sourceStream.Position = 0;

    var destinationStream = new MemoryStream();
    using (var compressor = new DeflateStream(destinationStream, CompressionMode.Compress, leaveOpen: true))
    {
        sourceStream.CopyTo(compressor);
    }

    destinationStream.Position = 0;

    Console.WriteLine($"Length of deflate archive is {destinationStream.Length}");
}
