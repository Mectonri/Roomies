using ITI.Roomies.DAL;
using ITI.Roomies.WebApp.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ITI.Roomies.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize( AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class TransactionController : Controller
    {
        readonly TransactionGateway _transactionGateway;
        public TransactionController( TransactionGateway transactionGateway)
        {
            _transactionGateway = transactionGateway;
        }

        [HttpGet("getAllRoomieTransaction/{id}" )]
        public async Task<IActionResult> GetAllRoomieTrans( )
        {
            throw new NotImplementedException();
        }

        

    }
}
