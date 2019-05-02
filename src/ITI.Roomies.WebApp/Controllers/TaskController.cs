using System.Collections.Generic;
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

        // Renvoie toutes les tâches liées à une collocation
        [HttpGet( "getByCollocId/{id}" )]
        public async Task<IActionResult> GetTasksByCollocIdAsync( int id )
        {
            IEnumerable<TasksData> result = await _tasksGateway.FindTaskByCollocId(id );
            return this.Ok( result );
        }

        // Renvoie toutes les tâches liées à un Roomie
        [HttpGet( "getByRoomieId" )]
        public async Task<IActionResult> GetTasksByRoomieIdAsync()
        {
            int userId = int.Parse( HttpContext.User.FindFirst( c => c.Type == ClaimTypes.NameIdentifier ).Value );
            IEnumerable<TasksData> result = await _tasksGateway.FindTaskByRoomieId( userId );
            return this.Ok( result );
        }

        //[HttpPost]
        //public async Task<int> CreateTask( [FromBody] CollocViewModel model )
        //{
        //Result<int> result = await _collocGateway.CreateColloc( model.CollocName );
        //int userId = int.Parse( HttpContext.User.FindFirst( c => c.Type == ClaimTypes.NameIdentifier ).Value );
        //Result<int> result2 = await _collRoomGateway.AddCollRoom( result.Content, userId );

        //return result.Content;

        //}

        // Création de tâches depuis le modèle, ne prend pas en compte la description
        [HttpPost("createTaskSansDesc")]
        public async Task<IActionResult> createTaskSansDescAsync( [FromBody] TaskViewModel model )
        {
            Result<int> result = await _tasksGateway.CreateTask( model.TaskName, model.TaskDate, model.collocId);

            // Si aucune erreur d'exécution, ajoute la tâches avec les roomies à tiTaskRoom
            if( !result.HasError )
            {
                for( int i = 0; i < model.roomiesId.Length; i++ )
                {
                    await _taskRoomGateway.AddTaskRoom( result.Content, model.roomiesId[i] );
                }
            }

            // TO DO : mettre le bon return
            return Ok( 0 );
        }
    }
}
