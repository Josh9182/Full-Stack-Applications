using Isopoh.Cryptography.Argon2;
using System.Security.Cryptography;
using AutomiqoSoftware.ServiceResponses.AuthorizationWrappers;
using AutomiqoSoftware.Interfaces.AuthorizationInterfaces;
using System.Text;

namespace AutomiqoSoftware.Services.AuthorizationServices;

public class CryptoService : ICryptoService {
    // inject ILogger<CryptoService> interface for access to Microsofts logging capabilities

    public CryptoResponse<string> GenerateSalt() { // Salt generator, unique to user. 
        try {
            var saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create()) { /* Disposable construct 
                                                            which creates an rng, storing via var.
                                                            Rng stores 16 bytes worth of random numbers.
                                                            returning the salt as the rng converted to 
                                                            string characters equivalent to the numerical 
                                                            binary code.
                                                           */
                rng.GetBytes(saltBytes);
                var salt = Convert.ToBase64String(saltBytes); // will be instanced, result will be converted to byte.
                return CryptoResponse<string>.Success(salt);
            }
        }
        catch (CryptographicException ex) {
            // log ex.Message
            string errorMessage = $"Salt generation failed due to internal error. \n Error: ${ex.Message}";
            return CryptoResponse<string>.Failure(errorMessage);
        }
    }

    public CryptoResponse<string> HashPassword(string password, byte[] saltBytes) {
        try {
            var config = new Argon2Config {
                Type = Argon2Type.HybridAddressing, // Argon2id for hybrid protection
                Version = Argon2Version.Nineteen, // Most current version
                TimeCost = 4, // Recalculation iteration amount
                MemoryCost = 65536, // Amount of memory in kilobytes Argon2 will use
                Threads = 4, // Amount of CPU threads used to speed up hashing process
                Lanes = 4, // Amount of independent threads that can be run concurrently
                HashLength = 32, // Length of resulting hashed output in bytes. 
                Salt = saltBytes, // Salt = GenerateSalt() output
                Password = Encoding.UTF8.GetBytes(password) // Password = password input convereted into bytes.
            };
            using (var argon2 = new Argon2(config)) { // disposable block, initiate hash based off parameters
                var hash = argon2.Hash();
                string hashString = Convert.ToBase64String(hash.Buffer); // Converting 
                return CryptoResponse<string>.Success(hashString);
            }
        }
        catch (FormatException fx) {
            string errorMessage = $"Argon2 Configuration Error. \n Error: {fx.Message}";
            return CryptoResponse<string>.Failure(errorMessage);
        }
    }

    public CryptoResponse<bool> VerifyPassword(string password, byte[] saltBytes, string possibleHash) {/*
                                                                                         Bool method requiring password, salt, expected hash.
                                                                                         Password is hashed, if attempted hash = previous stored hash
                                                                                         method returns true, else failure.
                                                                                         */
        var hash = HashPassword(password, saltBytes);
        if (hash.Equals(possibleHash)) { // if user input = previous input then success
            return CryptoResponse<bool>.Success(true);
        }
        else { // else failure
            string errorMessage = $"Hashed password does not equal the possible hash. Please re-input.";
            return CryptoResponse<bool>.Failure(errorMessage);
        }
    }
}