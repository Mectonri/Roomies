using ITI.Roomies.DAL;
using ITI.Roomies.WebApp.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.Roomies.WebApp.Controllers
{
    [Route( "api/[controller]" )]
    [Authorize( AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme )]
    public class CategoryController : Controller
    {
        readonly CategoryGateway _categoryGateway;

        public CategoryController( CategoryGateway categoryGateway)
        {
            _categoryGateway = categoryGateway;
        }



    }
}
