using System.IO.Compression;

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
