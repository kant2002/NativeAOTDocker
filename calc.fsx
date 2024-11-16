#r "nuget: MathNet.Numerics, 5.0.0"
#r "nuget: MathNet.Numerics.FSharp, 5.0.0"

// Illustrates solving systems of simultaneous linear
// equations using the DenseMatrix and LUDecomposition classes 
// in the Numerics.NET.LinearAlgebra namespace of Numerics.NET.

#light

open System

open MathNet.Numerics
// The DenseMatrix and LUDecomposition classes reside in the 
// Numerics.NET.LinearAlgebra namespace.
open MathNet.Numerics.LinearAlgebra

// The license is verified at runtime. We're using a 30 day trial key here.
// For more information, see:
//     https://numerics.net/trial-key
//let licensed = Numerics.NET.License.Verify("64542-18980-57619-62268")

// A system of simultaneous linear equations is
// defined by a square matrix A and a right-hand
// side B, which can be a vector or a matrix.
//
// You can use any matrix type for the matrix A.
// The optimal algorithm is automatically selected.

[<Literal>]
let ConsoleInvariantGlobalization = 0
[<Literal>]
let ConsoleWithICU = 1
[<Literal>]
let ConsoleWithCompression = 2
[<Literal>]
let AppHttpClient = 3
[<Literal>]
let AppHttpClientWithSSL = 4
[<Literal>]
let AppWebApi = 5
[<Literal>]
let AppGrpcApi = 6
[<Literal>]
let AppNpgSqlAdoIcu = 7
[<Literal>]
let AppNpgSqlAdo = 8
[<Literal>]
let AppOpenSSLFiles = 9
[<Literal>]
let AppICU = 10
[<Literal>]
let AppNpgSqlSlimBuilderIcu = 11
[<Literal>]
let AppNpgSqlSlimBuilder = 12

[<Literal>] 
let ComponentRuntime = 0
[<Literal>] 
let ComponentGlobalizationSupport = 1
[<Literal>] 
let ComponentCompressionSupport = 2
[<Literal>] 
let ComponentHttpClient = 3
[<Literal>] 
let ComponentOpenSSLSupport = 4
[<Literal>] 
let ComponentWebApi = 5
[<Literal>] 
let ComponentGrpc = 6
[<Literal>] 
let ComponentNpgsqlAdoNet = 7
[<Literal>] 
let ComponentOpenSSLCertificates = 8
[<Literal>] 
let ComponentICUDatafiles = 9
[<Literal>] 
let ComponentNpgsqlBuilderSlim = 10

let m = Matrix<float>.Build.Dense(13, 11)
// Console + Invariant globalization
m[ConsoleInvariantGlobalization, ComponentRuntime] <- 1.0
// Console + ICU
m[ConsoleWithICU,ComponentRuntime] <- 1.0
m[ConsoleWithICU,ComponentGlobalizationSupport] <- 1.0
m[ConsoleWithICU,ComponentICUDatafiles] <- 1.0
// Console + Brotli + Deflate + Gzip
m[ConsoleWithCompression,ComponentRuntime] <- 1.0
m[ConsoleWithCompression,ComponentCompressionSupport] <- 1.0
// HttpClient
m[AppHttpClient,ComponentRuntime] <- 1.0
m[AppHttpClient,ComponentHttpClient] <- 1.0
// HttpClient + OpenSSL
m[AppHttpClientWithSSL,ComponentRuntime] <- 1.0
m[AppHttpClientWithSSL,ComponentHttpClient] <- 1.0
m[AppHttpClientWithSSL,ComponentOpenSSLSupport] <- 1.0
m[AppHttpClientWithSSL,ComponentOpenSSLCertificates] <- 1.0
// Web API
m[AppWebApi,ComponentRuntime] <- 1.0
m[AppWebApi,ComponentWebApi] <- 1.0
// Grpc API
m[AppGrpcApi,ComponentRuntime] <- 1.0
m[AppGrpcApi,ComponentGrpc] <- 1.0
// Npgsql ADO.NET + ICU
m[AppNpgSqlAdoIcu,ComponentRuntime] <- 1.0
m[AppNpgSqlAdoIcu,ComponentNpgsqlAdoNet] <- 1.0
m[AppNpgSqlAdoIcu,ComponentGlobalizationSupport] <- 1.0
m[AppNpgSqlAdoIcu,ComponentICUDatafiles] <- 1.0
// Npgsql ADO.NET + Invariant globalization
m[AppNpgSqlAdo,ComponentRuntime] <- 1.0
m[AppNpgSqlAdo,ComponentNpgsqlAdoNet] <- 1.0
// OpenSSL certificates
m[AppOpenSSLFiles,ComponentOpenSSLCertificates] <- 1.0
// Npgsql ADO.NET + Invariant globalization
m[AppICU,ComponentICUDatafiles] <- 1.0
// Npgsql ADO.NET + ICU
m[AppNpgSqlSlimBuilderIcu,ComponentRuntime] <- 1.0
m[AppNpgSqlSlimBuilderIcu,ComponentNpgsqlBuilderSlim] <- 1.0
m[AppNpgSqlSlimBuilderIcu,ComponentGlobalizationSupport] <- 1.0
m[AppNpgSqlSlimBuilderIcu,ComponentICUDatafiles] <- 1.0
// Npgsql ADO.NET + Invariant globalization
m[AppNpgSqlSlimBuilder,ComponentRuntime] <- 1.0
m[AppNpgSqlSlimBuilder,ComponentNpgsqlBuilderSlim] <- 1.0

// Console + ICU
let b1 = vector[
    1.29; // Console + Invariant globalization (nativeaot-scratch-invariant)
    35.14; // Console + ICU
    2.22; // Console + Brotli + Deflate + Gzip (nativeaot-scratch-compression)
    7.26; // HttpClient (nativeaot-scratch-http-client)
    12.16; // HttpClient + OpenSSL (nativeaot-scratch-openssl)
    21.94; // Web API (nativeaot-scratch-webapi)
    23.46; // Grpc API (nativeaot-scratch-grpcapi)
    50.98; // Npgsql ADO.NET + ICU (nativeaot-scratch-npgsql)
    17.08; // Npgsql ADO.NET + Invariant globalization (nativeaot-scratch-npgsql-noicu)
    0.;   // OpenSSL certificates
    0.;   // ICU data
    44.1; // NpgsqlSlimDataSourceBuilder + ICU (nativeaot-scratch-npgsql)
    10.2; // NpgsqlSlimDataSourceBuilder + Invariant globalization (nativeaot-scratch-npgsql-noicu)
]

Vector<float>.Build.Dense(11)
// Static immutable data
b1[AppOpenSSLFiles] <- 0.64
b1[AppICU] <- 29.4

//
// The Solve method
//

// The following solves m x = b1. The second 
// parameter specifies whether to overwrite the
// right-hand side with the result.
let x1 = m.Solve(b1)
printfn "| Component | Size |"
printfn "| ------------ | ----- |"
printfn "| Barebone runtime + console | %.2fMB |" x1[ComponentRuntime]
printfn "| ICU data | %.2fMB | " x1[ComponentICUDatafiles]
printfn "| Globalization support | %.2fMB |" x1[ComponentGlobalizationSupport]
printfn "| Brotli + Deflate + Gzip | %.2fMB |" x1[ComponentCompressionSupport]
printfn "| HttpClient | %.2fMB |" x1[ComponentHttpClient]
printfn "| OpenSSL | %.2fMB |" x1[ComponentOpenSSLSupport]
printfn "| OpenSSL certificates | %.2fMB |" x1[ComponentOpenSSLCertificates]
printfn "| Web API | %.2fMB |" x1[ComponentWebApi]
printfn "| Grpc API | %.2fMB |" x1[ComponentGrpc]
printfn "| Npgsql ADO.NET | %.2fMB |" x1[ComponentNpgsqlAdoNet]
printfn "| Npgsql Builder Slim | %.2fMB |" x1[ComponentNpgsqlBuilderSlim]