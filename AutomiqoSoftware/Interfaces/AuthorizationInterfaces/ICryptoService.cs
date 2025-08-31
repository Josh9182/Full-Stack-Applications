using AutomiqoSoftware.ServiceResponses.AuthorizationWrappers;

namespace AutomiqoSoftware.Interfaces.AuthorizationInterfaces;

public interface ICryptoService {
    CryptoResponse<string> GenerateSalt(); // 16 byte salt generator
    CryptoResponse<string> HashPassword(string password, byte[] saltBytes); /* Argon2 powered password hasher, 
                                                                                   pepper + password + salted ~ hashed */
    CryptoResponse<bool> VerifyPassword(string password, byte[] saltBytes, string possibleHash); /* Check to see if
                                                                                    the user input pass
                                                                                    matches the hashed pass
                                                                                    in database. */
}