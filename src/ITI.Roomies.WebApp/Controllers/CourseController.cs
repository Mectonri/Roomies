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
    public class CourseController : Controller
    {
        readonly CourseGateway _courseGateway;

        public CourseController(CourseGateway courseGateway)
        {
            _courseGateway = courseGateway;
        }
    }
}
