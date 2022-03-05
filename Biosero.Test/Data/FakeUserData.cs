using AutoFixture;
using Biosero.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Biosero.Test.Data
{
    public static class FakeUserData
    {
        public static readonly int _darthVaderUserId = 66;

        public static IList<User> SetupFakeUsers(int id)
        {
            return new List<User> {
                GetFakeUser(id),
                GetFakeUser(_darthVaderUserId)
            };
        }

        public static User GetFakeUser(int id)
        {
            var fixture = new Fixture();

            var book = fixture.Build<User>()
                .With(x => x.Id, id)
                .Create();

            return book;
        }
    }
}
