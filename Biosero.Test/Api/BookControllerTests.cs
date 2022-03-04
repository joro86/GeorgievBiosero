using Biosero.Api.Controllers;
using Biosero.Data.Repositories;
using Biosero.Service.Interfaces;
using Biosero.Service.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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

        [TestInitialize]
        public void Init()
        {
            _bookRepository = new BookRepository();
            _userRepository = new UserRepository();
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
        public void WillGetBooks()
        {
            
        }



        private void SetupUserId(int id, string name)
        {
            _mockIUserContext.Setup(x => x.GetId()).Returns(id);

        }



        private void SetupUserName(string name)
        {
            _mockIUserContext.Setup(x => x.GetName()).Returns(name);
        }
    }
}
