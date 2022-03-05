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

        public UserRepository(IList<User> data)
        {
            _data = data;
        }

        public async Task<User> GetById(string userName, string password)
        {
            //In a real life application, this will be EF db calls. Faked this to be async sinse that is a better practice.
            return await Task.Run(() => 
            _data.SingleOrDefault(x => x.UserName == userName && x.Password == password)
            );
        }

        public async Task<User> GetById(int id)
        {
            //In a real life application, this will be EF db calls. Faked this to be async sinse that is a better practice.
            return await Task.Run(() =>
            _data.SingleOrDefault(x => x.Id == id)
            );
        }

        public async Task<int> GetDarthVader()
        {
            return await Task.Run(() =>
            {
                var user = _data.SingleOrDefault(x => x.FirstName == "_Darth" && x.LastName == "Vader_");

                if(user == null)
                {
                    return 0;
                }

                return user.Id;
            }
           );
        }

        private IList<User> GetPrePopulatedData()
        {
            var bookList = new List<User> {
                new User{ Id = 1, Email = "vader@gmail.com", FirstName = "_Darth", LastName = "Vader_", UserName= "darth", Password = "vader" },
                new User{ Id = 2, Email = "email@gmail.com", FirstName = "FirstName", LastName = "LastName", UserName= "email", Password = "password" },
                new User{ Id = 3, Email = "test@gmail.com", FirstName = "test", LastName = "test",  UserName= "test", Password = "password" }
            };

            return bookList;
        }
    }
}
