using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.Roomies.DAL;
using ITI.Roomies.WebApp.Authentication;
using ITI.Roomies.WebApp.Models.ItemModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITI.Roomies.WebApp.Controllers
{
    [Route( "api/[controller]" )]
    [Authorize( AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme )]
    public class ItemController : Controller
    {

        readonly ItemGateway _itemGateway;

        public ItemController( ItemGateway itemGateway )
        {
            _itemGateway = itemGateway;
        }

        [HttpPost]
        public async Task<IActionResult> AddItem( [FromBody] ItemViewModel model )
        {
            Result<int> result = await _itemGateway.CreateItem( model.RoomieId, model.ItemName, model.ItemPrice, model.CourseId);
            return this.CreateResult( result );
        }
    }
}
