using AutoFixture;
using Biosero.Api.Controllers;
using Biosero.Data.Models;
using Biosero.Data.Repositories;
using Biosero.Service.Interfaces;
using Biosero.Service.Models;
using Biosero.Service.Models.Api;
using Biosero.Service.Services;
using Biosero.Service.Utilities;
using Biosero.Test.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Biosero.Test.Api
{
    [TestClass]
    public class BookControllerTests
    {
        private BookController _controller;
        private BookService _bookService;
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
            _bookData = FakeBookData.SetupFakeBookList(_bookId, _userId);

            _userData = FakeUserData.SetupFakeUsers(_userId);

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
        public async Task Detail_WillGetBooksById()
        {
            var apiResult = await _controller.Detail(_bookId) as ObjectResult;

            var book = (BookDto)apiResult.Value;

            Assert.AreEqual(_bookData[0].Title, book.Title);
            Assert.AreEqual(_bookData[0].Description, book.Description);
            Assert.AreEqual(_bookData[0].Id, book.Id);
            Assert.AreEqual(_bookData[0].Price, book.Price);
        }

        [TestMethod]
        public async Task Create_WillCreateNewBook()
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
        public void Create_WillPreventDarthVadorToPublishABook()
        {
            var fixture = new Fixture();

            var bookRequest = fixture.Build<BookRequest>()
                .Create();

            SetupUserId(FakeUserData._darthVaderUserId);

            Assert.ThrowsExceptionAsync<InvalidOperationException>(async () => await _controller.Create(bookRequest));
        }

        [TestMethod]
        public async Task Delete_WillUnpublishBook()
        {
            SetupUserId(_userId);

              var apiResult = await _controller.Delete(_bookId) as StatusCodeResult;

            Assert.AreEqual(200, apiResult.StatusCode);

            var afterDelete = await _controller.Detail(_bookId) as ObjectResult;
            var result = (BookDto)afterDelete.Value;

            Assert.IsFalse(result.IsPublished);
        }

        [TestMethod]
        public void Delete_WillBookNotFoundException_WhenBookIsNotFound()
        {
            Assert.ThrowsExceptionAsync<BookNotFoundException>(async () =>await _controller.Detail(998989));
        }

        [TestMethod]
        public async Task Update_WillUpdateBook()
        {
            var fixture = new Fixture();

            var newBook = fixture.Build<BookRequest>().Create();

            SetupUserId(_userId);

            var apiResult = await _controller.Create(newBook) as ObjectResult;

            var result = (BookDto)apiResult.Value;

            result.Title = "Some New Title";

            var updatedResponce = await _controller.Update(result) as ObjectResult;

            var book = (BookDto)updatedResponce.Value;

            Assert.AreEqual(result.Title, book.Title);
        }


        [TestMethod]
        public async Task List_WillGetListOfBooks()
        {
            var apiResult = await _controller.List(new BookSearchRequest()) as ObjectResult;

            Assert.AreEqual(200, apiResult.StatusCode);

            var result = (List<BookDto>)apiResult.Value;

            Assert.AreEqual(2, result.Count);
        }


        [TestMethod]
        public async Task List_FilterByTitle()
        {
            var serachRequest = new BookSearchRequest
            {
                Title = _bookData[0].Title
            };

            var apiResult = await _controller.List(serachRequest) as ObjectResult;

            Assert.AreEqual(200, apiResult.StatusCode);

            var result = (List<BookDto>)apiResult.Value;

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(_bookData[0].Title, result[0].Title);
            Assert.AreEqual(_bookData[0].Description, result[0].Description);
            Assert.AreEqual(_bookData[0].Price, result[0].Price);
        }

        [TestMethod]
        public async Task List_FilterByDescription()
        {
            var serachRequest = new BookSearchRequest
            {
                Description = _bookData[1].Description
            };

            var apiResult = await _controller.List(serachRequest) as ObjectResult;

            Assert.AreEqual(200, apiResult.StatusCode);

            var result = (List<BookDto>)apiResult.Value;

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(_bookData[1].Title, result[0].Title);
            Assert.AreEqual(_bookData[1].Description, result[0].Description);
            Assert.AreEqual(_bookData[1].Price, result[0].Price);
        }

        private void SetupUserId(int id)
        {
            _mockIUserContext.Setup(x => x.GetId()).Returns(id);
        }
    }
}
