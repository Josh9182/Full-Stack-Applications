using AutomiqoSoftware.ServiceResponses.AuthorizationWrappers;
using AutomiqoSoftware.Interfaces.AuthorizationInterfaces;
using AutomiqoSoftware.Sessions;
using AutomiqoSoftware.DTOs.AuthorizationDTO.Users;
using AutomiqoSoftware.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace AutomiqoSoftware.Services.AuthorizationServices;

public class AuthService : IAuthService {
    private readonly AppDbContext _dataBase;
    private readonly ICryptoService _cryptoService;

    public AuthService(AppDbContext dataBase, ICryptoService cryptoService) {
        _dataBase = dataBase;
        _cryptoService = cryptoService;
    }

    /* Async method, returning true or false based off registration status. 
       Requires email, username, and password input. If found in db ❌ else ✅
    */
    public async Task<CryptoResponse<string>> RegisterUser(UserDto dto) {
        if (_dataBase.User.Any(u => u.UserEmail == dto.UserEmail || u.Username == dto.Username)) {
            string errorMessage = $"User has already registered in the past with a valid username or email.";
            return CryptoResponse<string>.Failure(errorMessage);
        }
        else {
            try {
                CryptoResponse<string> salt = _cryptoService.GenerateSalt(); // <string> salt value
                var saltBytes = Convert.FromBase64String(salt.Data); /* converts string back into bytes 
                                                                    for HashPassword using .Data 
                                                                    generic type
                                                                    */
                var hashedPass = _cryptoService.HashPassword(dto.Password, saltBytes);
                if (string.IsNullOrEmpty(hashedPass.Data)) {
                    throw new ArgumentNullException($"{nameof(hashedPass)} cannot be null");
                }

                var user = new User {
                    UserEmail = dto.UserEmail,
                    Username = dto.Username,
                    HashedPass = hashedPass.Data,
                    Salt = salt.Data
                };

                _dataBase.User.Add(user); // 
                await _dataBase.SaveChangesAsync();
                return CryptoResponse<string>.Success(salt.Data);
            }
            catch (Exception ex) {
                string errorMessage = $"Failure to salt user. \n Error: {ex.Message}";
                return CryptoResponse<string>.Failure(errorMessage);
            }
        }
    }

    public async Task<bool> UserLogin(UserDto dto) {
        var user = await _dataBase.User.FirstOrDefaultAsync(u => u.Username == dto.Username || u.UserEmail == dto.UserEmail);

        if (user == null) {
            return false;
        }
        else {
            var saltBytes = Convert.FromBase64String(user.Salt);
            var validationHash = _cryptoService.HashPassword(dto.Password, saltBytes);
            if (validationHash.Data == user.HashedPass) {
                return true;
            }
            else {
                return false;
            }
        } 
    }
}