using AutoFixture;
using Biosero.Api.Controllers;
using Biosero.Api.Models;
using Biosero.Api.Utilities;
using Biosero.Data.Models;
using Biosero.Data.Repositories;
using Biosero.Service.Interfaces;
using Biosero.Service.Models;
using Biosero.Service.Models.Api;
using Biosero.Service.Services;
using Biosero.Test.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Biosero.Test.Api
{
    [TestClass]
    public class AuthenticationControllerTests
    {
        private AuthController _controller;
        private AuthenticationService _authenticationService;
        private UserRepository _userRepository;

        private Mock<IJwtHandler> _mockJwtHandler;

        private readonly int _userId = 55;
        private IList<User> _userData;

        [TestInitialize]
        public void Init()
        {
            _userData = FakeUserData.SetupFakeUsers(_userId);

            _mockJwtHandler = new Mock<IJwtHandler>();

              _userRepository = new UserRepository(_userData.ToList());
            _authenticationService = new AuthenticationService(_userRepository);
            _controller = new AuthController(_mockJwtHandler.Object, _authenticationService);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockJwtHandler.VerifyAll();
        }

        [TestMethod]
        public async Task Login_WhenInvalidParametersArePassed()
        {
            var loginRequest = new LoginRequest
            {
            };
           
            var afterlogin = await _controller.Login(loginRequest) as ObjectResult;

            var result = (AuthResponseDto)afterlogin.Value;

            Assert.AreEqual(400, afterlogin.StatusCode.Value);
            Assert.IsFalse(result.IsAuthSuccessful);
        }

        [TestMethod]
        public async Task Login_WillAuthenticateUser()
        {
            var loginRequest = new LoginRequest 
            {
                    Username = _userData[0].UserName,
                    Password = _userData[0].Password
            };
            var jwtToken = "jqtTokes";

            _mockJwtHandler.Setup(x => x.GenerateToken(It.IsAny<UserDto>())).Returns(jwtToken);

            var afterlogin = await _controller.Login(loginRequest) as ObjectResult;

            var result = (AuthResponseDto)afterlogin.Value;

            Assert.AreEqual(200, afterlogin.StatusCode.Value);
            Assert.IsTrue(result.IsAuthSuccessful);
            Assert.AreEqual(jwtToken, result.Token);
        }

        [TestMethod]
        public async Task Login_WillNOTAuthenticateUsee_WHenUserNameANdPasswordIsNotvalid()
        {
            var loginRequest = new LoginRequest
            {
                Username = "Sone user",
                Password = "sone unknown password"
            };

            var afterlogin = await _controller.Login(loginRequest) as ObjectResult;

            var result = (AuthResponseDto)afterlogin.Value;

            Assert.AreEqual(400, afterlogin.StatusCode.Value);
            Assert.IsFalse(result.IsAuthSuccessful);
        }
    }
}
