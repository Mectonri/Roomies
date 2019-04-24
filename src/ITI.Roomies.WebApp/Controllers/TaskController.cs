using System.Security.Claims;
using System.Threading.Tasks;
using ITI.Roomies.DAL;
using ITI.Roomies.WebApp.Authentication;
using ITI.Roomies.WebApp.Models.TaskModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITI.Roomies.WebApp.Controllers
{

    [Route( "api/[controller]" )]
    [Authorize( AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme )]
    public class TaskController : Controller
    {
        readonly TasksGateway _tasksGateway;
        readonly TaskRoomGateway _taskRoomGateway;

        public TaskController( TasksGateway tasksGateway, TaskRoomGateway taskRoomGateway )
        {
            _tasksGateway = tasksGateway;
            _taskRoomGateway = taskRoomGateway;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTasksByCollocIdAsync( int id )
        {
            Result<TasksData> result = await _tasksGateway.FindByCollocId(id );
            return this.CreateResult( result );
        }

        //[HttpPost]
        //public async Task<int> CreateTask( [FromBody] CollocViewModel model )
        //{
        //Result<int> result = await _collocGateway.CreateColloc( model.CollocName );
        //int userId = int.Parse( HttpContext.User.FindFirst( c => c.Type == ClaimTypes.NameIdentifier ).Value );
        //Result<int> result2 = await _collRoomGateway.AddCollRoom( result.Content, userId );

        //return result.Content;

        //}


    }
}
