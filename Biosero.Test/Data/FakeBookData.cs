using AutoFixture;
using Biosero.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Biosero.Test.Data
{
    public static class FakeBookData
    {
        public static IList<Book> SetupFakeBookList(int id, int userId)
        {
            var bookList = new List<Book>
            {
                GetFakeBook(id, userId),
                GetFakeBook(++ id, ++userId)
            };

            return bookList;
        }

        public static Book GetFakeBook(int id, int userId)
        {
            var fixture = new Fixture();

            var book = fixture.Build<Book>()
                .With(x => x.Id, id).With(x => x.Author, FakeUserData.GetFakeUser(userId))
                .Create();

            return book;
        }
    }
}
