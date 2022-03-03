using AutoMapper;
using Biosero.Data.Models;
using Biosero.Data.Repositories;
using Biosero.Service.Filters;
using Biosero.Service.Models;
using Biosero.Service.Models.Api;
using Biosero.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Biosero.Service.Services
{
    public class BookService
    {
        private readonly BookRepository _bookRepository;
        private IMapper _autoMapper;

        public BookService(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
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



        public async Task<BookDto> CreateBook(BookDto book)
        {


        }



        public async Task<BookDto> Update(BookDto book)
        {

       
        }


        public async Task Delete(int id)
        {

        
        }

    }
}
