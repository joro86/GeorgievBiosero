using AutoFixture;
using Biosero.Api.Controllers;
using Biosero.Data.Models;
using Biosero.Data.Repositories;
using Biosero.Service.Interfaces;
using Biosero.Service.Models;
using Biosero.Service.Models.Api;
using Biosero.Service.Services;
using Microsoft.AspNetCore.Mvc;
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
        private AuthenticationController _controller;
        private AuthenticationService _bookService;
        private UserRepository _bookRepository;
        private UserRepository _userRepository;

        private Mock<IUserContext> _mockIUserContext;

        private readonly int _bookId = 33;
        private IList<Book> _bookData;

        private readonly int _userId = 55;
        private IList<User> _userData;

        [TestInitialize]
        public void Init()
        {
          
        }

        [TestCleanup]
        public void Cleanup()
        {
         
        }

        [TestMethod]
        public async Task WillGetBooksById()
        {
       
        }



        private void SetupUserId(int id)
        {
            _mockIUserContext.Setup(x => x.GetId()).Returns(id);
        }

        private IList<Book> GetFakeBook(int id, int userId)
        {
            var fixture = new Fixture();

            var bookList = fixture.Build<Book>()
                .With(x => x.Id, id).With(x => x.Author, GetFakeUser(userId))
                .CreateMany(1)
                .ToList();

            return bookList;
        }

        private IList<User> GetFakeUsers(int id)
        {
            return new List<User> { GetFakeUser(id) };
        }

        private User GetFakeUser(int id)
        {
            var fixture = new Fixture();

            var book = fixture.Build<User>()
                .With(x => x.Id, id)
                .Create();

            return book;
        }
    }
}
