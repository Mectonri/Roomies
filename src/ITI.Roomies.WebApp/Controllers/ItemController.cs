using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
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
        public async Task<IActionResult> GetItemsFromList( int courseId )
        {
            IEnumerable<ItemData> result = await _itemGateway.GetAllItemFromList( courseId );
            return Ok( result );
        }

        //[HttpGet( "getRItems/{courseTempId}")]
        //public async Task<IActionResult> GetRItemsFromTemplate( int courseTempId)
        //{
        //    IEnumerable<RItemData> result = await _itemGateway.GetAllRItemFromTemplate( courseTempId );
        //    return Ok( result );
        //}

        [HttpGet( "{itemId}" )]
        public async Task<IActionResult> GetItemByItemId( int itemId )
        {
            Result<ItemData> result = await _itemGateway.FindById( itemId );
            return Ok( result );
        }

        //[HttpGet( "getRItem/{rItemId}")]
        //public async Task<IActionResult> GetRItemByRItemId( int rItemId)
        //{
        //    Result<RItemData> result = await _itemGateway.FindRItemById( rItemId );
        //    return Ok( result );
        //}

        [HttpPost("addItem")]
        public async Task<IActionResult> AddItem( [FromBody] ItemViewModel model )
        {
            if( model.IsRepeated )
            {
                Result result = await _itemGateway.CreateRItem( model.ItemPrice, model.ItemName, model.CourseId, model.RoomieId );
                return this.CreateResult( result );
            }
            else
            {
                int userId = int.Parse( HttpContext.User.FindFirst( c => c.Type == ClaimTypes.NameIdentifier ).Value );
                Result result = await _itemGateway.CreateItem( model.ItemPrice, model.ItemName, model.CourseId, userId );
                return this.CreateResult( result );
            }
        }

        [HttpDelete( "{itemId}" )]
        public async Task<IActionResult> DeleteItem( int itemId )
        {
            Result result = await _itemGateway.Delete( itemId );
            return this.CreateResult( result );
        }

        //[HttpDelete("deleteRItem/{rItemId}")]
        //public async Task<IActionResult> DeleteRItem( int rItemId)
        //{
        //    Result result = await _itemGateway.DeleteRItem( rItemId );
        //    return this.CreateResult( result );
        //}

        [HttpPut( "updateItem" )]
        public async Task<IActionResult> UpdateItemAsync([FromBody] ItemViewModel model)
        {
            int userId = int.Parse( HttpContext.User.FindFirst( c => c.Type == ClaimTypes.NameIdentifier ).Value );
            Result result = await _itemGateway.Update( model.ItemId, model.ItemPrice, model.ItemName, model.CourseId, userId );
            return this.CreateResult( result );
        }

        //[HttpPut("updateRItem/{RItemId}")]
        //public async Task<IActionResult> UpdateRItem( [FromBody] ItemViewModel model)
        //{
        //    Result result = await _itemGateway.UpdateRItem( model.ItemId, model.ItemPrice, model.ItemName, model.CourseId, model.CollocId );
        //    return this.CreateResult( result );
        //}

        // Met à jour l'état de l'item renseignée
        //[HttpPost( "updateItemBought/{id}/{bought}" )]
        //public async Task<IActionResult> updateItemBoughtAsync( int id, bool bought )
        //{
        //    Result result = await _itemGateway.UpdateItemBought( id, bought );
        //    return this.Ok( result );
        //}
    }
}
