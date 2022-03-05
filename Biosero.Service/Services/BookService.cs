using AutoMapper;
using Biosero.Data.Models;
using Biosero.Data.Repositories;
using Biosero.Service.Filters;
using Biosero.Service.Interfaces;
using Biosero.Service.Models;
using Biosero.Service.Models.Api;
using Biosero.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biosero.Service.Services
{
    public class BookService
    {
        private readonly BookRepository _bookRepository;
        private readonly UserRepository _userRepository;
        private readonly IUserContext _userContext;
        private IMapper _autoMapper;

        public BookService(BookRepository bookRepository, UserRepository userRepository, IUserContext userContext)
        {
            _bookRepository = bookRepository;
            _userContext = userContext;
            _userRepository = userRepository;
            _autoMapper = MapperHelper.Mapper;
        }

        public async Task<List<BookDto>> GetFilteredBookList(BookSearchRequest searchRequest)
        {
             var query = await _bookRepository.GetData();
            
            var bookFilter = new BookFilter(searchRequest);

            var bookList = bookFilter.GetResult(query);

            var result = _autoMapper.Map<List<BookDto>>(bookList);

            return result;
        }

        public async Task<BookDto> GetBookDetail(int id)
        {
            var book = await _bookRepository.GetById(id);

            var result = _autoMapper.Map<BookDto>(book);

            return result;
        }

        public async Task<BookDto> CreateBook(BookRequest book)
        {
            var currentUserId = _userContext.GetId();

            if (await IsDarthVader(currentUserId))
            {
                throw new InvalidOperationException("_Darth Vader_ is unable to publish books");
            }

            var author = await _userRepository.GetById(currentUserId);

            var result = _autoMapper.Map<Book>(book);
            result.Author = author;

            var addedBook   = await _bookRepository.Add(result);
            var bookDto = _autoMapper.Map<BookDto>(addedBook);
            return bookDto;
        }

        public async Task<BookDto> Update(BookDto book)
        {
            var result = _autoMapper.Map<Book>(book);
            var addedBook = await _bookRepository.Update(result);
            var bookDto = _autoMapper.Map<BookDto>(addedBook);
            return bookDto;
        }

        public async Task Delete(int id)
        {
            var currentUserId = _userContext.GetId();

            var result = await _bookRepository.Delete(id, currentUserId);

            if(result == null)
            {
                throw new BookNotFoundException("Book can't be unpublished");
            }
        }


        private async Task<bool> IsDarthVader(int currentUserId)
        {
            var darthVaderId = await _userRepository.GetDarthVader();

            if (darthVaderId == currentUserId)
            {
                return true;
            }

            return false;
        }

    }
}
