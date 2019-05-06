using ITI.Roomies.DAL;
using ITI.Roomies.WebApp.Authentication;
using ITI.Roomies.WebApp.Models.CourseViewModels;
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

        public CourseController( CourseGateway courseGateway )
        {
            _courseGateway = courseGateway;
        }

        [HttpGet( Name = "GetGroceriesList")]
        public async Task<IActionResult> GetGroceriesList()
        {
            IEnumerable<CourseData> result = await _courseGateway.GetAll();
            return Ok( result );
        }

        [HttpGet( "{id}", Name = "GetAGroceryList")]
        public async Task<IActionResult> GetGroceryListById(int courseId)
        {
            Result<CourseData> result = await _courseGateway.FindById( courseId );
            return this.CreateResult( result );
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroceryList( [FromBody] CourseViewModel model)
        {
            Result<int> result = await _courseGateway.CreateGroceryList( model.CourseName, model.CourseDate, model.CollocId);
            return this.CreateResult( result, o =>
            {
                o.RouteName = "GetAGroceryList";
                o.RouteValues = id => new { id };
            } );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAGroceryList( int courseId, [FromBody] CourseViewModel model )
        {
            Result result = await _courseGateway.UpdateGroceryList( courseId, model.CourseName, model.CourseDate, model.CollocId );
            return this.CreateResult( result );
        }

        [HttpDelete( "{id}" )]
        public async Task<IActionResult> DeleteAgroceryList (int courseId)
        {
            Result result = await _courseGateway.DeleteGroceryList( courseId );
            return this.CreateResult( result );
        }
    }
}
