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
    public class CollocationController : Controller
    {
        readonly CollocGateway _collocGateway;

        public CollocationController( CollocGateway collocGateway )
        {
            _collocGateway = collocGateway;
        }


        [HttpPost]
        public async Task<IActionResult> CreateColloc( [FromBody] CollocViewModel model )
        {
            int userId = int.Parse( HttpContext.User.FindFirst( c => c.Type == ClaimTypes.NameIdentifier ).Value );
            Result<int> result = await _collocGateway.CreateColloc( model.CollocName );
            return this.CreateResult( result );
        }


    }
}
