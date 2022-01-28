﻿using Dell.Lead.WeApi.Business;
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
    public class UserControllerTest
    {
        private readonly Mock<IUserBusiness> _mockUserBusiness;

        public UserControllerTest()
        {
            _mockUserBusiness = new Mock<IUserBusiness>();
        }

        private UserController UserController(Mock<IUserBusiness> mockUserBusiness)
        {
            return new UserController(mockUserBusiness.Object);
        }

        [Fact]
        public void Create()
        {
            var user = new UserVO()
            {
                Login = "anitta",
                Password = "12345"
            };

            _mockUserBusiness.Setup(x => x.Create(It.IsAny<UserVO>())).Returns(true);

            var userController = UserController(_mockUserBusiness);
            ActionResult<UserVO> response = userController.Create(user);
            OkResult result = (OkResult)response.Result;

            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void CreateUserErro()
        {
            UserVO user = null;

            _mockUserBusiness.Setup(x => x.Create(It.IsAny<UserVO>())).Returns(false);

            var controllerUser = UserController(_mockUserBusiness);
            ActionResult<UserVO> response = controllerUser.Create(user);
            BadRequestObjectResult result = (BadRequestObjectResult)response.Result;

            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Invalid user request", result.Value);
            
        }

        [Fact]
        public void FailCreateUser()
        {
            var user = new UserVO()
            {
                Login = "anitta",
                Password = "12345"
            };

            _mockUserBusiness.Setup(x => x.Create(It.IsAny<UserVO>())).Returns(false);

            var controllerUser = UserController(_mockUserBusiness);
            ActionResult<UserVO> response = controllerUser.Create(user);
            BadRequestObjectResult result = (BadRequestObjectResult)response.Result;

            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Failed to register the user", result.Value);

        }

    }

}
