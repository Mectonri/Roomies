using System.Security.Claims;
using System.Threading.Tasks;
using ITI.Roomies.DAL;
using ITI.Roomies.WebApp.Authentication;
using ITI.Roomies.WebApp.Models.RoomieModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ITI.Roomies;

[Route( "api/[controller]" )]
[Authorize( AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme )]
public class CollocRoomController : Controller
{
    readonly CollRoomGateway _collRoomGateway;

    public CollocRoomController( CollRoomGateway collroomGateway )
    {
        _collRoomGateway = collroomGateway;
    }


    [HttpPost]
    public async Task<IActionResult> AddCollocationRoom( [FromBody] CollocRoomViewModel model, RoomiesData roomies )
    {
        Result<int> result = await _collRoomGateway.AddCollRoom( model.CollocId, roomies.RoomieId );
        return Ok( result );
    }
}
