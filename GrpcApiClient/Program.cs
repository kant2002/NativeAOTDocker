using System.Net;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcApiClient;

ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;

// The port number must match the port of the gRPC server.
var options = new GrpcChannelOptions(){ UnsafeUseInsecureChannelCallCredentials = true };
var httpClientHandler = new HttpClientHandler();

// That's only to simplify showing that GRPC channel woks
// Remove from production code.
httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, _) => {
    return true;
};
options.HttpHandler = httpClientHandler; 

using var channel = GrpcChannel.ForAddress("https://localhost:8011", options);
var client = new Greeter.GreeterClient(channel);
var reply = await client.SayHelloAsync(
                  new HelloRequest { Name = "GreeterClient" });
Console.WriteLine("Greeting: " + reply.Message);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();
