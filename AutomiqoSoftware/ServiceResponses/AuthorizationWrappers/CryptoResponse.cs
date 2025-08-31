using AutomiqoSoftware.Interfaces.AuthorizationInterfaces;

namespace AutomiqoSoftware.ServiceResponses.AuthorizationWrappers;

public class CryptoResponse<T> : ICryptoResponse<T> {
    public bool IsSuccess { get; }
    public string? ErrorMessage { get; }
    public T? Data { get; }

    public CryptoResponse(bool isSuccess, T? data, string? errorMessage = null) {
        IsSuccess = isSuccess;

        Data = data;

        if (!isSuccess && string.IsNullOrWhiteSpace(errorMessage)) {
            throw new ArgumentNullException(nameof(errorMessage), "Error message cannot be null or empty when the response indicates failure.");
        }

        ErrorMessage = errorMessage;
    }

    public static CryptoResponse<T> Success(T data) {
        return new CryptoResponse<T>(true, data);
    }

    public static CryptoResponse<T> Failure(string errorMessage) {
        return new CryptoResponse<T>(false, default, errorMessage);
    }


}