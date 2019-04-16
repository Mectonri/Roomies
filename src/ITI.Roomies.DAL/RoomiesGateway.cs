using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using Dapper;

namespace ITI.Roomies.DAL
{
    public class RoomiesGateway
    {
        readonly string _connectionString;

<<<<<<< HEAD:src/ITI.Roomies.DAL/RoomieGateway.cs
        

        public RoomieGateway( string connectionString )
=======
        public RoomiesGateway( string connectionString )
>>>>>>> Jean:src/ITI.Roomies.DAL/RoomiesGateway.cs
        {
            _connectionString = connectionString;
        }
        public async Task<Result<RoomiesData>> FindById( int roomieId )
        {
            using( SqlConnection con = new SqlConnection( _connectionString ) )
<<<<<<< HEAD:src/ITI.Roomies.DAL/RoomieGateway.cs
            {
                RoomieData roomie = await con.QueryFirstOrDefaultAsync<RoomieData>(
=======
            { 
                RoomiesData roomie = await con.QueryFirstOrDefaultAsync<RoomiesData>(
>>>>>>> Jean:src/ITI.Roomies.DAL/RoomiesGateway.cs
                    @"select s.RoomieId,
                             s.FirstName,
                             s.LastName,
                             s.BirthDate,
                             s.Phone,
                             s.Email
                      from rm.tRoomies s
                      where s.RoomieId = @RoomieId;",
                    new { RoomieId = roomieId } );

<<<<<<< HEAD:src/ITI.Roomies.DAL/RoomieGateway.cs
                if( roomie == null ) return Result.Failure<RoomieData>( Status.NotFound, "Roomie not found." );
=======
                if( roomie == null ) return Result.Failure<RoomiesData>( Status.NotFound, "Student not found." );
>>>>>>> Jean:src/ITI.Roomies.DAL/RoomiesGateway.cs
                return Result.Success( roomie );
            }
        }

<<<<<<< HEAD:src/ITI.Roomies.DAL/RoomieGateway.cs
        public async Task<Result<int>> CreateRoomie( string firstName, string lastName, DateTime birthDate, string Phone, string Email)
=======
        public async Task<Result<int>> CreateRoomie( string firstName, string lastName, DateTime birthDate, string phone, int userId )
>>>>>>> Jean:src/ITI.Roomies.DAL/RoomiesGateway.cs
                {
                    if( !IsNameValid( firstName ) ) return Result.Failure<int>( Status.BadRequest, "The first name is not valid." );
                    if( !IsNameValid( lastName ) ) return Result.Failure<int>( Status.BadRequest, "The last name is not valid." );

                    using( SqlConnection con = new SqlConnection( _connectionString ) )
                    {
                        var p = new DynamicParameters();
                        p.Add( "@FirstName", firstName );
                        p.Add( "@LastName", lastName );
                        p.Add( "@BirthDate", birthDate );
<<<<<<< HEAD:src/ITI.Roomies.DAL/RoomieGateway.cs
                        p.Add( "@Phone", Phone ?? string.Empty );
                        p.Add( "@Email", Email ?? string.Empty );
=======
                        p.Add( "@Phone", phone);
                        p.Add( "@userId", userId);
>>>>>>> Jean:src/ITI.Roomies.DAL/RoomiesGateway.cs
                        p.Add( "@RoomieId", dbType: DbType.Int32, direction: ParameterDirection.Output );
                        p.Add( "@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue );
                        await con.ExecuteAsync( "rm.sRoomiesCreate", p, commandType: CommandType.StoredProcedure );

                        int status = p.Get<int>( "@Status" );
                        if( status == 1 ) return Result.Failure<int>( Status.BadRequest, "A roomie with this name already exists." );
<<<<<<< HEAD:src/ITI.Roomies.DAL/RoomieGateway.cs
=======

>>>>>>> Jean:src/ITI.Roomies.DAL/RoomiesGateway.cs
                        Debug.Assert( status == 0 );
                        return Result.Success( Status.Created, p.Get<int>( "@RoomieId" ) );
                    }
         
        }

        public async Task<Result> Delete( int roomieId )
        {
            using( SqlConnection con = new SqlConnection( _connectionString ) )
            {
                var p = new DynamicParameters();
                p.Add( "@RoomieId", roomieId );
                p.Add( "@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue );
                await con.ExecuteAsync( "rm.sRoomiesDelete", p, commandType: CommandType.StoredProcedure );

                int status = p.Get<int>( "@Status" );
                if( status == 1 ) return Result.Failure( Status.NotFound, "Roomie not found." );

                Debug.Assert( status == 0 );
                return Result.Success();
            }
        }

        public async Task<Result> Update (int roomieId, string desc, string phone )
        {
            using( SqlConnection con = new SqlConnection( _connectionString ) )
            {
                var p = new DynamicParameters();
                p.Add( "@RoomieId", roomieId );
                p.Add( "@Description", desc );
                p.Add( "@Phone", phone );
                p.Add( "@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue );
                await con.ExecuteAsync( "rm.sRoomiesUpdate", p, commandType: CommandType.StoredProcedure );
                int status = p.Get<int>( "@Status" );
                if( status == 1 ) return Result.Failure( Status.NotFound, "Roomie not found." );
                Debug.Assert( status == 0 );
                return Result.Success( Status.Ok );
            }
        }

        bool IsNameValid( string name ) => !string.IsNullOrWhiteSpace( name );
    }
}
