using AutoFixture;
using Biosero.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biosero.Data.Repositories
{
    public class BookRepository
    {
        private IQueryable<Book> _data;

        public BookRepository()
        {
            _data = GetPrePopulatedData().AsQueryable();
        }

        //I am using this to setup data for unit tests
        public BookRepository(List<Book> bookList)
        {
            _data = bookList.AsQueryable();
        }

        public async Task<IQueryable<Book>> GetData()
        {
            return await Task.Run(() => _data);
        }

        public async Task<Book> GetById(int id)
        {
            return await Task.Run(() => _data.SingleOrDefault(x => x.Id == id));
        }

        public async Task<Book> Add(Book book)
        {
            return await Task.Run(() =>
            {

                //This should be auto incremented by the DB.
                //but his will do for now!!
                var maxId = _data.Max(x => x.Id);
                book.Id = maxId;

                _data.Append(book);
                return book;
            });
        }

        public async Task<Book> Update(Book bookToAdd)
        {
            return await Task.Run(() =>
            {

                var book = _data.FirstOrDefault(x => x.Id == bookToAdd.Id);
                book = bookToAdd;
                return book;
            });
        }

        public async Task<Book> Delete(int id, int authorId)
        {
            return await Task.Run(() =>
            {
                var book = _data.FirstOrDefault(x => x.Id == id && x.Author.Id == authorId);

                if(book == null)
                {
                    return null; 
                }

                book.IsPublished = false;
                return book;
            });
        }

        private IList<Book> GetPrePopulatedData()
        {
            var bookList = new List<Book> {
                new Book {Id = 1, Title = "Book 1", Description = "Description 1", Author= new User {Id=1, FirstName = "FirstName 1", LastName = "LastName 1" }, CoverImage = "", Price = 44, IsPublished = true},
                new Book {Id = 2, Title = "Book 2", Description = "Description 2", Author= new User {Id=2, FirstName = "FirstName 2", LastName = "LastName 2" }, CoverImage = "", Price = 44, IsPublished = true},
                new Book {Id = 3, Title = "Book 3", Description = "Description 3", Author= new User {Id=3, FirstName = "FirstName 3", LastName = "LastName 3" }, CoverImage = "", Price = 44, IsPublished = true},
                new Book {Id = 4, Title = "Book 4", Description = "Description 4", Author= new User {Id=4, FirstName = "FirstName 4", LastName = "LastName 4" }, CoverImage = "", Price = 44, IsPublished = true},
                new Book {Id = 5, Title = "Book 5", Description = "Description 5", Author= new User {Id=5, FirstName = "FirstName 5", LastName = "LastName 5" }, CoverImage = "", Price = 44, IsPublished = true},
                new Book {Id = 6, Title = "Book 6", Description = "Description 6", Author= new User {Id=6, FirstName = "FirstName 6", LastName = "LastName 6" }, CoverImage = "", Price = 44, IsPublished = true},
                new Book {Id = 7, Title = "Book 7", Description = "Description 7", Author= new User {Id=7, FirstName = "FirstName 7", LastName = "LastName 7" }, CoverImage = "", Price = 44, IsPublished = true},
                new Book {Id = 8, Title = "Book 8", Description = "Description 8", Author= new User {Id=8, FirstName = "FirstName 8", LastName = "LastName 8" }, CoverImage = "", Price = 44, IsPublished = true},
                new Book {Id = 9, Title = "Book 9", Description = "Description 9", Author= new User {Id=9, FirstName = "FirstName 9", LastName = "LastName 9" }, CoverImage = "", Price = 44, IsPublished = true},
                new Book {Id = 10, Title = "Book 10", Description = "Description 10", Author= new User {Id=10 ,FirstName = "FirstName 10", LastName = "LastName 10" }, CoverImage = "", Price = 44, IsPublished = true}
            };

            return bookList;
        }
    }
}
