using Biosero.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biosero.Data.Repositories
{
    public class UserRepository
    {

        private IList<User> _data;

        public UserRepository()
        {
            _data = GetPrePopulatedData();
        }

        public async Task<User> GetById(string userName, string password)
        {
            return await Task.Run(() => _data.SingleOrDefault(x => x.UserName == userName && x.Password == password));
        }

        private IList<User> GetPrePopulatedData()
        {
            var bookList = new List<User> {
                new User{ Id = 1, Email = "vader@gmail.com", FirstName = "_Darth", LastName = "Vader_", Password = "1" },
                new User{ Id = 2, Email = "email@gmail.com", FirstName = "FirstName", LastName = "LastName", Password = "2" }
            };

            return bookList;
        }
    }
}
