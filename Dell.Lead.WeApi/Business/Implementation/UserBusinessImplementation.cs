using Dell.Lead.WeApi.Data.Converter.Converter;
using Dell.Lead.WeApi.Data.VO;
using Dell.Lead.WeApi.Models;
using Dell.Lead.WeApi.Repositories;
using System;
using System.Security.Cryptography;

namespace Dell.Lead.WeApi.Business.Implementation
{
    public class UserBusinessImplementation : IUserBusiness
    {

        private readonly IUserRepository _userRepository;
        private readonly UserConverter _converter;

        public UserBusinessImplementation(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _converter = new UserConverter();
        }
        public UserVO Create(UserVO userVO)
        {
            var pass = _userRepository.ComputeHash(userVO.Password, new SHA256CryptoServiceProvider());

            var user = new User()
            {
                Login = userVO.Login,
                Password = pass,
                RefreshTokenExpiryTime = DateTime.Now.AddDays(7)
            };

            user = _userRepository.Create(user);

            return _converter.Parse(user);
        }

        public UserVO FindById(long id)
        {
            return _converter.Parse(_userRepository.FindById(id));
        }
    }
}
