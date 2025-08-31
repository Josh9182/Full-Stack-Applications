using System.Security.Principal;
using AutomiqoSoftware.Models.Users;
using AutomiqoSoftware.DTOs.AuthorizationDTO.Users;

namespace AutomiqoSoftware.Interfaces.AuthorizationInterfaces;

public interface IOAuthService {
    Task<UserDto?> ValidateGoogleTokenAsync(string idToken); /* Asynchronous methods validating 
                                                                    id / access tokens, requests will be
                                                                    in the format of a UserDto, find format
                                                                    via DTOs\UserDto.cs

                                                                    If request fails, DTO will return null,
                                                                    noticiable by the "?".
                                                                    */
    Task<UserDto?> ValidateGithubTokenAsync(string accessToken);
    Task<UserDto?> ValidateFacebookTokenAsync(string accessToken);
    Task<UserDto?> ValidateLinkedinTokenAsync(string accessToken);
    Task<UserDto?> ValidateYahooTokenAsync(string accessToken);
}