using System.Threading.Tasks;
using ITI.Roomies.DAL;
using System;

namespace ITI.Roomies.WebApp.Services
{
    public class UserService
    {
        readonly UserGateway _userGateway;
        readonly PasswordHasher _passwordHasher;

        public UserService(UserGateway userGateway, PasswordHasher passwordHasher)
        {
            _userGateway = userGateway;
            _passwordHasher = passwordHasher;
        }

        public Task<Result<int>> CreatePasswordUser( string email, string password )
        {
            return _userGateway.CreatePasswordUser(email, _passwordHasher.HashPassword(password));
        }

        public Task<Result<int>> CreateRoomie( string firstName, string lastName, DateTime birthDate, string Phone, string email )
        {
            return _userGateway.CreateRoomie( firstName, lastName, birthDate, Phone, email );
        }

        public async Task<UserData> FindUser(string email, string password)
        {
            UserData user = await _userGateway.FindByEmail(email);
            if (user != null && _passwordHasher.VerifyHashedPassword(user.Password, password) == PasswordVerificationResult.Success)
            {
                return user;
            }

            return null;
        }
    }
}
