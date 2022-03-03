using AutoMapper;
using Biosero.Data.Repositories;
using Biosero.Service.Models;
using Biosero.Service.Utilities;
using System.Threading.Tasks;

namespace Biosero.Service.Services
{
    public class AuthenticationService
    {
        private readonly UserRepository _userRepository;
        private IMapper _autoMapper;

        public AuthenticationService(UserRepository userRepository)
        {
            _userRepository = userRepository;
            _autoMapper = MapperHelper.Mapper;
        }

        public async Task<UserDto> Login(string userName, string password)
        {
            var user = await _userRepository.GetById(userName, password);

            var result = _autoMapper.Map<UserDto>(user);

            return result;
        }
    }
}
