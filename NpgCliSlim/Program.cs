using Npgsql;

var connectionString = Environment.GetEnvironmentVariable("ConnectionString") ?? "Host=localhost:32768;Username=postgres;Password=postgrespw";
Console.WriteLine("Connecting using connection string "+ connectionString);
using var conn = new NpgsqlSlimDataSourceBuilder(connectionString).Build();

await using (var cmd = conn.CreateCommand("SELECT 5"))
await using (var reader = await cmd.ExecuteReaderAsync())
{
while (await reader.ReadAsync())
    Console.WriteLine(reader.GetInt32(0));
}
