using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ITI.Roomies.DAL
{
    public class TasksGateway
    {
        readonly string _connectionString;

        public TasksGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Result<TasksData>> FindTaskByCollocId( int collocId)
        {
            using( SqlConnection con = new SqlConnection( _connectionString ) )
            {
                TasksData task = await con.QueryFirstOrDefaultAsync<TasksData>(
                    @"select t.TaskId,
                             t.TaskName,
                             t.TaskDate,
                             t.State,
                             t.CollocId
                      from rm.tTasks t
                      where t.CollocId = @CollocId;",
                    new { CollocId = collocId } );

                if( task == null ) return Result.Failure<TasksData>( Status.NotFound, "No task found for this Collocation." );
                return Result.Success( task );
            }
        }

        public async Task<Result<TasksData>> FindByTaskId( int taskId)
        {
            using( SqlConnection con = new SqlConnection( _connectionString ) )
            {
                TasksData task = await con.QueryFirstOrDefaultAsync<TasksData>(
                    @"select t.TaskId,
                             t.TaskName,
                             t.TaskDate,
                             t.State,
                             t.CollocId
                      from rm.tTasks t
                      where t.TaskId = @TaskId;",
                    new { TaskId = taskId } );

                if( task == null ) return Result.Failure<TasksData>( Status.NotFound, "Task not found." );
                return Result.Success( task );
            }
        }

        public async Task<Result<int>> CreateTask (string taskName, DateTime taskDate, int collocId )
        {
            using( SqlConnection con = new SqlConnection( _connectionString ) )
            {
                var p = new DynamicParameters();
                p.Add( "@TaskName", taskName );
                p.Add( "@TaskDate", taskDate );
                p.Add( "@CollocId", collocId );
                p.Add( "@TaskId", dbType: DbType.Int32, direction: ParameterDirection.Output );
                p.Add( "@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue );
                await con.ExecuteAsync( "rm.sTasksCreate", p, commandType: CommandType.StoredProcedure );

                int status = p.Get<int>( "@Status" );
                if( status == 1 ) return Result.Failure<int>( Status.BadRequest, "A Task with this name already exists." );
                Debug.Assert( status == 0 );
                return Result.Success( Status.Created, p.Get<int>( "@TaskId" ) );
            }
        }

        public async Task<Result> Update (int taskId, string taskName, DateTime taskDate, bool state, int collocId, string taskDes)
        {
            using( SqlConnection con = new SqlConnection( _connectionString ) )
            {
                var p = new DynamicParameters();
                p.Add( "@TaskId", taskId );
                p.Add( "@TaskName", taskName );
                p.Add( "@TaskDate", taskDate );
                p.Add( "@State", state );
                p.Add( "@CollocId", collocId );
                p.Add( "@TaskDes", taskDes );
                p.Add( "@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue );
                await con.ExecuteAsync( "rm.sTasksUpdate", p, commandType: CommandType.StoredProcedure );
                int status = p.Get<int>( "@Status" );
                if( status == 1 ) return Result.Failure( Status.NotFound, "Task not found." );
                Debug.Assert( status == 0 );
                return Result.Success( Status.Ok );
            }
        }

        public async Task<Result> Delete( int taskId )
        {
            using( SqlConnection con = new SqlConnection( _connectionString ) )
            {
                var p = new DynamicParameters();
                p.Add( "@TaskId", taskId );
                p.Add( "@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue );
                await con.ExecuteAsync( "rm.sTasksDelete", p, commandType: CommandType.StoredProcedure );

                int status = p.Get<int>( "@Status" );
                if( status == 1 ) return Result.Failure( Status.NotFound, "Task not found." );

                Debug.Assert( status == 0 );
                return Result.Success();
            }
        }

    }
}
