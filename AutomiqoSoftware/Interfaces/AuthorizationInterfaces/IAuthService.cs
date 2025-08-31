using AutomiqoSoftware.ServiceResponses.AuthorizationWrappers;
using AutomiqoSoftware.DTOs.AuthorizationDTO.Users;


namespace AutomiqoSoftware.Interfaces.AuthorizationInterfaces;

public interface IAuthService {
    abstract Task<CryptoResponse<string>> RegisterUser(UserDto dto); /* Login method, 
                                                                   checking DB credentials */
    abstract Task<bool> UserLogin(UserDto dto); /* Regsistration method, 
                                                                          checking DB credentials */
}