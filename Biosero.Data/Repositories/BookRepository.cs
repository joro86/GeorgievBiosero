using AutoFixture;
using Biosero.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biosero.Data.Repositories
{
    public class BookRepository
    {

        private IQueryable<Book> _data;

        public BookRepository()
        {
            _data = GetFakeData().AsQueryable();
        }


        public  async Task<IQueryable<Book>> GetData()
        {
            return  await Task.Run(() => _data);
        }


        /// <summary>
        /// Using autofixture to generate fake data.
        /// In a real application, we are connecting to the Data source from here possible with EF.
        /// </summary>
        /// <returns></returns>
        private IList<Book> GetFakeData()
        {
            var id = 1;

            var fixture = new Fixture();

            var bookList = fixture.Build<Book>()
                .With(x => x.Id, ++id)
                .CreateMany(20)
                .ToList();

            return bookList;
        }

    }
}
