using Dell.Lead.WeApi.Data.VO;
using Dell.Lead.WeApi.Models;
using System.Security.Cryptography;

namespace Dell.Lead.WeApi.Repositories
{
    public interface IUserRepository
    {
        User Create(User user);
        User ValidateCredentials(UserVO user);
        User ValidateCredentials(string login);
        bool RevokeToken(string login);
        User RefreshUserInfo(User user);
        string ComputeHash(string password, SHA256CryptoServiceProvider algorithm);
    }
}
