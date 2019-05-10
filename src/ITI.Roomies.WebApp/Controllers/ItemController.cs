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

        [HttpGet( "getItemList/{courseId}" )]
        public async Task<IActionResult> GetList(int courseId )
        {
            IEnumerable<ItemData> result = await _itemGateway.GetAll( courseId );
            return Ok( result );
        }

        [HttpPost]
        public async Task<IActionResult> AddItem( [FromBody] ItemViewModel model )
        {
            Result<int> result = await _itemGateway.CreateItem( model.RoomieId, model.ItemName, model.ItemPrice, model.CourseId);
            return this.CreateResult( result );
        }

        [HttpDelete("delete/{itemId}")]
        public async Task<IActionResult> DeleteItem(int itemId)
        {
            Result result = await _itemGateway.Delete(itemId);
            return this.CreateResult( result );
        }
    }
}
