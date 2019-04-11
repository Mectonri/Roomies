using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using Dapper;

namespace ITI.Roomies.DAL
{
    public class RoomieGateway
    {
        readonly string _connectionString;

        

        public RoomieGateway( string connectionString )
        {
            _connectionString = connectionString;
        }
        public async Task<Result<RoomieData>> FindById( int roomieId )
        {
            using( SqlConnection con = new SqlConnection( _connectionString ) )
            {
                RoomieData roomie = await con.QueryFirstOrDefaultAsync<RoomieData>(
                    @"select s.RoomieId,
                             s.FirstName,
                             s.LastName,
                             s.BirthDate,
                             s.Phone,
                             s.Email
                      from rm.tRoomies s
                      where s.RoomieId = @RoomieId;",
                    new { RoomieId = roomieId } );

                if( roomie == null ) return Result.Failure<RoomieData>( Status.NotFound, "Roomie not found." );
                return Result.Success( roomie );
            }
        }

        public async Task<Result<int>> CreateRoomie( string firstName, string lastName, DateTime birthDate, string Phone, string Email )
                {
                    if( !IsNameValid( firstName ) ) return Result.Failure<int>( Status.BadRequest, "The first name is not valid." );
                    if( !IsNameValid( lastName ) ) return Result.Failure<int>( Status.BadRequest, "The last name is not valid." );

                    using( SqlConnection con = new SqlConnection( _connectionString ) )
                    {
                        var p = new DynamicParameters();
                        p.Add( "@FirstName", firstName );
                        p.Add( "@LastName", lastName );
                        p.Add( "@BirthDate", birthDate );
                        p.Add( "@Phone", Phone ?? string.Empty );
                        p.Add( "@Email", Phone ?? string.Empty );
                        p.Add( "@RoomieId", dbType: DbType.Int32, direction: ParameterDirection.Output );
                        p.Add( "@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue );
                        await con.ExecuteAsync( "rm.sRoomieCreate", p, commandType: CommandType.StoredProcedure );

                        int status = p.Get<int>( "@Status" );
                        if( status == 1 ) return Result.Failure<int>( Status.BadRequest, "A roomie with this name already exists." );
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
                await con.ExecuteAsync( "iti.sRoomieDelete", p, commandType: CommandType.StoredProcedure );

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
                await con.ExecuteAsync( "iti.sRoomieUpdate", p, commandType: CommandType.StoredProcedure );

                int status = p.Get<int>( "@Status" );
                if( status == 1 ) return Result.Failure( Status.NotFound, "Roomie not found." );
                
                Debug.Assert( status == 0 );
                return Result.Success( Status.Ok );
            }
        }

        bool IsNameValid( string name ) => !string.IsNullOrWhiteSpace( name );
    }
}
