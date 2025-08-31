using Isopoh.Cryptography.SecureArray;
using System.Collections.Generic;
using AutomiqoSoftware.ServiceResponses.AuthorizationWrappers;

namespace AutomiqoSoftware.Interfaces.AuthorizationInterfaces;

public interface ICryptoResponse<T> {
    static abstract CryptoResponse<T> Success(T data);
    static abstract CryptoResponse<T> Failure(string errorMessage);
}