using System.Security.Claims;
using System.Threading.Tasks;
using ITI.Roomies.DAL;
using ITI.Roomies.WebApp.Authentication;
using ITI.Roomies.WebApp.Models.RoomieModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITI.Roomies.WebApp.Controllers
{

    [Route( "api/[controller]" )]
    [Authorize( AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme )]
    public class UserController : Controller
    {
        readonly UserGateway _userGateway;

        public UserController( UserGateway userGateway )
        {
            _userGateway = userGateway;
        }


        [HttpGet]
        public async Task<IActionResult> getUsersById( [FromBody] int UserId )
        {
            int userId = int.Parse( HttpContext.User.FindFirst( c => c.Type == ClaimTypes.NameIdentifier ).Value );
            Result<UserData> result = await _userGateway.FindById( userId );
            return this.CreateResult( result );
        }


    }
}
