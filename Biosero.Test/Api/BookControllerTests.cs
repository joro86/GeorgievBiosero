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
using System.Threading.Tasks;

namespace Biosero.Test.Api
{
    [TestClass]
    public class BookControllerTests
    {
        private  BookController _controller;
        private  BookService _bookService;
        private BookRepository _bookRepository;
        private UserRepository _userRepository;

        private Mock<IUserContext> _mockIUserContext;

        private readonly int _bookId = 33;
        private IList<Book> _bookData;

        private readonly int _userId = 55;
        private IList<User> _userData;

        [TestInitialize]
        public void Init()
        {
            _bookData = GetFakeBook(_bookId, _userId);

            _userData = GetFakeUsers(_userId);

            _bookRepository = new BookRepository(_bookData.ToList());
            _userRepository = new UserRepository(_userData.ToList());
            _mockIUserContext = new Mock<IUserContext>();

            _bookService = new BookService(_bookRepository, _userRepository, _mockIUserContext.Object);

            _controller = new BookController(_bookService);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockIUserContext.VerifyAll();
        }

        [TestMethod]
        public async Task WillGetBooksById()
        {
            var apiResult = await _controller.Detail(_bookId) as ObjectResult;

            var book = (BookDto)apiResult.Value;

            Assert.AreEqual(_bookData[0].Title, book.Title);
            Assert.AreEqual(_bookData[0].Description, book.Description);
            Assert.AreEqual(_bookData[0].Id, book.Id);
            Assert.AreEqual(_bookData[0].Price, book.Price);
        }


        [TestMethod]
        public async Task WillCreateNewBook()
        {
            var fixture = new Fixture();

            var bookRequest = fixture.Build<BookRequest>()
                .Create();

            SetupUserId(_userId);

            var apiResult = await _controller.Create(bookRequest) as ObjectResult;

            var book = (BookDto)apiResult.Value;

            Assert.AreEqual(bookRequest.Title, book.Title);
            Assert.AreEqual(bookRequest.Description, book.Description);
            Assert.AreEqual(bookRequest.Price, book.Price);
            Assert.AreEqual(_userData[0].FirstName, book.Author.FirstName);
            Assert.AreEqual(_userData[0].LastName, book.Author.LastName);
        }

        [TestMethod]
        public async Task WillDelete()
        {
            var fixture = new Fixture();

            var bookRequest = 
                fixture.Build<BookRequest>()
                .Create();

            SetupUserId(_userId);

            var apiResult = await _controller.Delete(_bookId) as ObjectResult;
        }

        private void SetupUserId(int id)
        {
            _mockIUserContext.Setup(x => x.GetId()).Returns(id);

        }

        private void SetupUserName(string name)
        {
            _mockIUserContext.Setup(x => x.GetName()).Returns(name);
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
