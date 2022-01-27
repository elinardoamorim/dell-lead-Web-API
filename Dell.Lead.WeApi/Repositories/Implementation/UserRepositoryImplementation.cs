using Dell.Lead.WeApi.Data.VO;
using Dell.Lead.WeApi.Models;
using Dell.Lead.WeApi.Models.Context;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Dell.Lead.WeApi.Repositories.Implementation
{
    public class UserRepositoryImplementation : IUserRepository
    {
        private readonly SqlServerContext _context;
        public UserRepositoryImplementation(SqlServerContext context)
        {
            _context = context;
        }

        public User Create(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return user;
        }
        public string ComputeHash(string password, SHA256CryptoServiceProvider algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(password);
            Byte[] hashedByte = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedByte);
        }

        public User RefreshUserInfo(User user)
        {
            if (!_context.Users.Any(u => u.Id.Equals(user.Id))) return null;
            var result = _context.Users.SingleOrDefault(p => p.Id.Equals(user.Id));
            if(result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                }
                catch(Exception)
                {
                    throw;
                }
            }

            return null;
        }

        public bool RevokeToken(string login)
        {
            var result = _context.Users.SingleOrDefault(u => u.Login.Equals(login));
            if (result == null) return false;

            User user = result;
            user.RefreshToken = null;
            user.Password = user.Password;

            _context.Entry(result).CurrentValues.SetValues(user);
            _context.SaveChanges();
            return true;
        }

        public User ValidateCredentials(UserVO user)
        {
            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());
            return _context.Users.FirstOrDefault(u => (u.Login.Equals(user.Login)) && (u.Password.Equals(pass)));
        }

        public User ValidateCredentials(string login)
        {
            return _context.Users.SingleOrDefault(u => u.Login.Equals(login));
        }
    }
}
