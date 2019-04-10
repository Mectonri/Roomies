using System.Collections.Generic;
using System.Threading.Tasks;
using ITI.Roomies;
using ITI.Roomies.DAL;
using ITI.Roomies.WebApp.Authentication;
using ITI.Roomies.WebApp.Controllers;
using ITI.Roomies.WebApp.Models.RoomieModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITI.PrimarySchool.WebApp.Controllers
{
    [Route( "api/[controller]" )]
    [Authorize( AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme )]
    public class RoomieController : Controller
    {
        readonly RoomieGateway _roomieGateway;

        public RoomieController( RoomieGateway roomieGateway )
        {
            _roomieGateway = roomieGateway;
        }

        [HttpGet( "{id}", Name = "GetRoomie" )]
        public async Task<IActionResult> GetRoomieById( int id )
        {
            Result<RoomieData> result = await _roomieGateway.FindById( id );
            return this.CreateResult( result );
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoomie( [FromBody] RoomiesViewModel model )
        {
            Result<int> result = await _roomieGateway.CreateRoomie( model.FirstName, model.LastName, model.BirthDate, model.Phone, model.Email );
            return this.CreateResult( result, o =>
            {
                o.RouteName = "GetRoomie";
                o.RouteValues = id => new { id };
            } );
        }
    }
}
