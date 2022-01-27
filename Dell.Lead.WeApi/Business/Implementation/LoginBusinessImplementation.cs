using Dell.Lead.WeApi.Configurations;
using Dell.Lead.WeApi.Data.VO;
using Dell.Lead.WeApi.Repositories;
using Dell.Lead.WeApi.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Dell.Lead.WeApi.Business.Implementation
{
    public class LoginBusinessImplementation : ILoginBusiness
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private TokenConfiguration _configuration;
        private IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public LoginBusinessImplementation(TokenConfiguration tokenConfiguration, IUserRepository userRepository, ITokenService tokenService)
        {
            _configuration = tokenConfiguration;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }
        public bool RevokeToken(string login)
        {
            return _userRepository.RevokeToken(login);
        }

        public TokenVO ValidateCredentials(UserVO userCredentials)
        {
            var user = _userRepository.ValidateCredentials(userCredentials);
            if (user == null) return null;
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Login)
            };

            var acessToken = _tokenService.GenerateAcessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpiry);

            _userRepository.RefreshUserInfo(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            return new TokenVO(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                acessToken,
                refreshToken
                );
        }

        public TokenVO ValidateCredentials(TokenRefreshVO token)
        {
            var acessToken = token.AcessToken;
            var refreshToken = token.RefreshToken;
            var principal = _tokenService.GetPrincipalFromExpiredToken(acessToken);
            var login = principal.Identity.Name;
            var user = _userRepository.ValidateCredentials(login);

            if (user == null ||
                user.RefreshToken != refreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.Now) return null;

            acessToken = _tokenService.GenerateAcessToken(principal.Claims);
            refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            _userRepository.RefreshUserInfo(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            return new TokenVO(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                acessToken,
                refreshToken
                );
        }
    }
}
