using Biosero.Data.Models;
using Biosero.Service.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biosero.Service.Filters
{
    public class BookFilter
    {

        private readonly BookSearchRequest _bookSearchRequest;

        public BookFilter(BookSearchRequest filterRequest)
        {
            _bookSearchRequest = filterRequest;
        }


        public IList<Book> GetResult(IQueryable<Book> query)
        {
                query = ApplyFIlters(query);
                return query.ToList();
        }

        private IQueryable<Book> ApplyFIlters(IQueryable<Book> query)
        {

            if(!string.IsNullOrEmpty( _bookSearchRequest.Title))
            {
                query = query.Where(x => x.Title.ToLower().Contains(_bookSearchRequest.Title.ToLower()));
            }

            if (!string.IsNullOrEmpty(_bookSearchRequest.Description))
            {
                query = query.Where(x => x.Description.ToLower().Contains(_bookSearchRequest.Description.ToLower()));
            }

            if (!string.IsNullOrEmpty(_bookSearchRequest.Authour))
            {
                query = query.Where(x => 
                    x.Author.FirstName.ToLower().Contains(_bookSearchRequest.Authour.ToLower()) ||
                     x.Author.LastName.ToLower().Contains(_bookSearchRequest.Authour.ToLower())
                    );
            }

            return query;
        }
    }
}
