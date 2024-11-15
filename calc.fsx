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

// Let's start with a general matrix:
let m = 
    matrix
        [
            [1.0; 0.0; 0.0; 0.0; 0.0; 0.0; 0.0; 0.0; 0.0; 0.0]; // Console + Invariant globalization
            [1.0; 1.0; 0.0; 0.0; 0.0; 0.0; 0.0; 0.0; 0.0; 1.0]; // Console + ICU
            [1.0; 0.0; 1.0; 0.0; 0.0; 0.0; 0.0; 0.0; 0.0; 0.0]; // Console + Brotli + Deflate + Gzip
            [1.0; 0.0; 0.0; 1.0; 0.0; 0.0; 0.0; 0.0; 0.0; 0.0]; // HttpClient
            [1.0; 0.0; 0.0; 1.0; 1.0; 0.0; 0.0; 0.0; 1.0; 0.0]; // HttpClient + OpenSSL
            [1.0; 0.0; 0.0; 0.0; 0.0; 1.0; 0.0; 0.0; 0.0; 0.0]; // Web API
            [1.0; 0.0; 0.0; 0.0; 0.0; 0.0; 1.0; 0.0; 0.0; 0.0]; // Grpc API
            [1.0; 1.0; 0.0; 0.0; 0.0; 0.0; 0.0; 1.0; 0.0; 1.0]; // Npgsql ADO.NET + ICU + Globalization support
            [1.0; 0.0; 0.0; 0.0; 0.0; 0.0; 0.0; 1.0; 0.0; 0.0]; // Npgsql ADO.NET + Invariant globalization
            [0.0; 0.0; 0.0; 0.0; 0.0; 0.0; 0.0; 0.0; 1.0; 0.0]; // OpenSSL certificates
            [0.0; 0.0; 0.0; 0.0; 0.0; 0.0; 0.0; 0.0; 0.0; 1.0]; // ICU Data
        ]
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
    0.64;   // OpenSSL certificates
    29.4;   // ICU data
]

//
// The Solve method
//

// The following solves m x = b1. The second 
// parameter specifies whether to overwrite the
// right-hand side with the result.
let x1 = m.Solve(b1)
printfn "| Component | Size |"
printfn "| ------------ | ----- |"
printfn "| Barebone runtime + console | %.2fMB |" x1[0]
printfn "| ICU data | %.2fMB | " x1[9]
printfn "| Globalization support | %.2fMB |" x1[1]
printfn "| Brotli + Deflate + Gzip | %.2fMB |" x1[2]
printfn "| HttpClient | %.2fMB |" x1[3]
printfn "| OpenSSL | %.2fMB |" x1[4]
printfn "| OpenSSL certificates | %.2fMB |" x1[8]
printfn "| Web API | %.2fMB |" x1[5]
printfn "| Grpc API | %.2fMB |" x1[6]
printfn "| Npgsql ADO.NET | %.2fMB |" x1[7]