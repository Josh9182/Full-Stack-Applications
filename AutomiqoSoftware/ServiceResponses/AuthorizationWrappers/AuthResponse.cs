using AutomiqoSoftware.Sessions;
using AutomiqoSoftware.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace AutomiqoSoftware.ServiceResponses.AuthorizationWrappers;

public class AuthResponse<T> /* : IAuthResponse */ {

    public bool IsSuccess { get; }
    public string? ErrorMessage { get; }
    public CryptoResponse<T> CryptoResponse { get; } 

    private AuthResponse(bool isSuccess, CryptoResponse<T> cryptoResponse, string? errorMessage = null) {
        IsSuccess = isSuccess;
        CryptoResponse = cryptoResponse ??
            throw new ArgumentNullException($"{nameof(cryptoResponse)} cannot be null");
        ErrorMessage = errorMessage;
    }

    public static AuthResponse<T> Success(CryptoResponse<T> cryptoResponse) {
        return new AuthResponse<T>(true, cryptoResponse);
    }

    public static AuthResponse<T> Failure(string ErrorMessage, CryptoResponse<T> cryptoResponse) {
        return new AuthResponse<T>(false, cryptoResponse, ErrorMessage);
    }

}