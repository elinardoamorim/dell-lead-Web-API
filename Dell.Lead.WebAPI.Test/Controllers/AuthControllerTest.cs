using Dell.Lead.WeApi.Business;
using Dell.Lead.WeApi.Controllers;
using Dell.Lead.WeApi.Data.VO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Dell.Lead.WeApi.Test.Controllers
{
    public class AuthControllerTest
    {
        private readonly Mock<ILoginBusiness> _mockLoginBusiness;

        public AuthControllerTest()
        {
            _mockLoginBusiness = new Mock<ILoginBusiness>();
        }

        public AuthController AuthController(Mock<ILoginBusiness> mockLoginBusiness)
        {
            return new AuthController(mockLoginBusiness.Object);
        }

        [Fact]
        public void SigninFailUser()
        {
            UserVO user = null;

            _mockLoginBusiness.Setup(x => x.ValidateCredentials(It.IsAny<UserVO>()));

            var authController = AuthController(_mockLoginBusiness);
            ActionResult<TokenVO> response = authController.Signin(user);
            BadRequestObjectResult result = (BadRequestObjectResult)response.Result;

            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Invalid user request", result.Value);
        }

        [Fact]
        public void SigninGenerateTokenFail()
        {
            var user = new UserVO()
            {
                Login = "anitta",
                Password = "123"
            };

            TokenVO token = null;

            _mockLoginBusiness.Setup(x => x.ValidateCredentials(It.IsAny<UserVO>())).Returns(token);

            var authController = AuthController(_mockLoginBusiness);
            ActionResult<TokenVO> response = authController.Signin(user);
            UnauthorizedResult result = (UnauthorizedResult)response.Result;

            Assert.Equal(401, result.StatusCode);
        }

        [Fact]
        public void SigninGenerateToken()
        {
            var user = new UserVO()
            {
                Login = "anitta",
                Password = "123"
            };

            var token = new TokenVO(authenticated: true, created: "2022-01-28 13:38:23",
                expiration: "2022-01-28 14:38:23",
                acessToken: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9"
                ,refreshToken: "N08Bj2j/HbHz8yU8EBR/");

            _mockLoginBusiness.Setup(x => x.ValidateCredentials(It.IsAny<UserVO>())).Returns(token);

            var authController = AuthController(_mockLoginBusiness);
            ActionResult<TokenVO> response = authController.Signin(user);
            OkObjectResult result = (OkObjectResult)response.Result;

            Assert.Equal(200, result.StatusCode);
            Assert.Equal(token, result.Value);
        }

        [Fact]
        public void RefreshTokenSuccess()
        {
            var refreshToken = new TokenRefreshVO()
            {
                RefreshToken = "N08Bj2j/HbHz8yU8EBR/",
                AcessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9"
            };

            var token = new TokenVO(authenticated: true, created: "2022-01-28 13:38:23",
                expiration: "2022-01-28 14:38:23",
                acessToken: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9"
                , refreshToken: "N08Bj2j/HbHz8yU8EBR/");

            _mockLoginBusiness.Setup(x => x.ValidateCredentials(It.IsAny<TokenRefreshVO>())).Returns(token);

            var authController = AuthController(_mockLoginBusiness);
            ActionResult<TokenVO> response = authController.Refresh(refreshToken);
            OkObjectResult result = (OkObjectResult)response.Result;

            Assert.Equal(200, result.StatusCode);
            Assert.Equal(token, result.Value);
        }
    }
}
