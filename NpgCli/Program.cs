using Npgsql;

var connectionString = Environment.GetEnvironmentVariable("ConnectionString") ?? "Host=localhost:32768;Username=postgres;Password=postgrespw";
Console.WriteLine("Connecting using connection string "+ connectionString);
await using var conn = new NpgsqlConnection(connectionString);
await conn.OpenAsync();

await using (var cmd = new NpgsqlCommand("SELECT 5", conn))
await using (var reader = await cmd.ExecuteReaderAsync())
{
while (await reader.ReadAsync())
    Console.WriteLine(reader.GetInt32(0));
}
