using ITI.Roomies.DAL;
using ITI.Roomies.WebApp.Authentication;
using ITI.Roomies.WebApp.Models.TransactionViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ITI.Roomies.WebApp.Controllers
{
    [Route( "api/[controller]" )]
    [Authorize( AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme )]
    public class TransactionController : Controller
    {
        readonly TransactionGateway _transactionGateway;
        readonly BudgetGateway _budgetGateway;
        public TransactionController( TransactionGateway transactionGateway, BudgetGateway budgetGateway )
        {
            _transactionGateway = transactionGateway;
            _budgetGateway = budgetGateway;
        }

        [HttpPost( "createTransaction" )]
        public async Task<IActionResult> CreateTransaction( [FromBody] TransactionViewModel model )
        {
            //int rRoomieId = model.RoomieId;
            //if( rRoomieId == 0 )
            //{
            //    BudgetData budget = await _budgetGateway.FindBudgetByCollocIdAndDate( model.CollocId, model.Date );

            //    rRoomieId = int.Parse( HttpContext.User.FindFirst( c => c.Type == ClaimTypes.NameIdentifier ).Value );
            //    Result<int> result = await _transactionGateway.CreateTransacBudget( model.Price, model.Date, budget.BudgetId, rRoomieId );
            //    return this.CreateResult( result, o =>
            //    {
            //        o.RouteName = "GetTransacBudgetId";
            //        o.RouteValues = transacBudgetId => new { transacBudgetId };
            //    } );
            //}
            //else
            //{
            //    int sRoomieId = int.Parse( HttpContext.User.FindFirst( c => c.Type == ClaimTypes.NameIdentifier ).Value );
            //    Result<int> result = await _transactionGateway.CreateTransacDepense( (int)model.Price, model.Date, sRoomieId, rRoomieId );
            //    return this.CreateResult( result, o =>
            //    {
            //        o.RouteName = "GetTransacDepense";
            //        o.RouteValues = tDepenseId => new { tDepenseId };
            //    } );
            //}
            throw new NotImplementedException();
        }

        [HttpPost( "createTDepense" )]
        public async Task<IActionResult> CreateTDepense( [FromBody] TransacDepenseViewModel model )
        {
            int sRoomieId = int.Parse( HttpContext.User.FindFirst( c => c.Type == ClaimTypes.NameIdentifier ).Value );
            Result<int> result = await _transactionGateway.CreateTransacDepense( model.Price, model.Date, sRoomieId, model.RRoomieId );
            return  Ok(result) ;
        }


        [HttpPost("createTBudget")]
        public async Task<IActionResult> CreateTBudget( [FromBody]TransacBudgetViewModel model )
        {
            int RoomieId = int.Parse( HttpContext.User.FindFirst( c => c.Type == ClaimTypes.NameIdentifier ).Value );
            Result<int> result = await _transactionGateway.CreateTransacBudget( model.Price, model.Date, model.CategoryId, RoomieId );
            return Ok( result );
           
        }


        #region TransacBudget

        [HttpGet( "getAllTransacBudget" )]
        public async Task<IActionResult> GetAllTransBudget()
        {
            int roomieId = int.Parse( HttpContext.User.FindFirst( c => c.Type == ClaimTypes.NameIdentifier ).Value );
            IEnumerable<TransacBudgetData> result = await _transactionGateway.GetAllTransacBudget( roomieId );
            return Ok( result );
        }

        [HttpGet( "getTransacBudget/{transacBudgetId}", Name = "GetTransacBudget" )]
        public async Task<IActionResult> GetTransBudget( int transacBudgetId )
        {
            Result<TransacBudgetData> result = await _transactionGateway.FindTBudgetById( transacBudgetId );
            return this.CreateResult( result );
        }

        [HttpPut( "updateTransacBudget/{transacBudget}" )]
        public async Task<IActionResult> UpdateTransacBudget( [FromBody] TransacBudgetData model )
        {
            Result result = await _transactionGateway.UpdateTransacBudget( model.TBudgetId, model.Price, model.Date, model.BudgetId, model.RoomieId );
            return this.CreateResult( result );
        }

        [HttpDelete( "deleteTBudget/{tBudgetId}" )]
        public async Task<IActionResult> DeleteTransacBudget( int tBudget )
        {
            Result result = await _transactionGateway.DeleteTransacBudget( tBudget );
            return this.CreateResult( result );
        }
        #endregion

        #region TrasancDepense

        [HttpGet( "GetAllTransacDepense" )]
        public async Task<IActionResult> GetAllTransDepense()
        {
            int roomieId = int.Parse( HttpContext.User.FindFirst( c => c.Type == ClaimTypes.NameIdentifier ).Value );
            IEnumerable<TransacDepenseData> result = await _transactionGateway.GetAllTransacDepense( roomieId );
            return Ok( result );
        }

        [HttpGet( "GetTransacDepense/{transacDepenseId}", Name = "GetTransacDepense" )]
        public async Task<IActionResult> getTransacDepense( int tDepenseId )
        {
            Result<TransacDepenseData> result = await _transactionGateway.FindTDepenseById( tDepenseId );
            return this.CreateResult( result );
        }


        [HttpPut( "updateTDepense" )]
        public async Task<IActionResult> UpdateTransacDepense( [FromBody] TransacDepenseViewModel model )
        {
            Result result = await _transactionGateway.UpdateTransacDepense( model.TDepenseId, model.Price, model.Date, model.SRoomieId, model.RRoomieId );
            return this.CreateResult( result );
        }

        [HttpDelete( "deleteTDepense/{tDepenseId}" )]
        public async Task<IActionResult> DeleteTransacDepense( int tDepenseId )
        {
            Result result = await _transactionGateway.DeleteTransacDepense( tDepenseId );
            return this.CreateResult( result );
        }


        #endregion
    }
}
