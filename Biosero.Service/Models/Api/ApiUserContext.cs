using Biosero.Service.Interfaces;
using System;
using System.Security.Claims;

namespace Biosero.Service.Models.Api
{
    public class ApiUserContext : IUserContext
    {
        private ClaimsPrincipal _user;

        public ApiUserContext(ClaimsPrincipal user)
        {
            _user = user;
        }

        public string GetName()
        {
            var name = _user.FindFirst("name").Value;
            var lastName = _user.FindFirst("lastName").Value;

            return $"{name} {lastName}";
        }

        public int GetId()
        {
            var id = _user.FindFirst("id").Value;
            return int.Parse(id);
        }
    }
}
