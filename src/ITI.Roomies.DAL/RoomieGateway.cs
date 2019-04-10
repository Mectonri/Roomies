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
                RoomieData student = await con.QueryFirstOrDefaultAsync<RoomieData>(
                    @"select s.RoomieId,
                             s.FirstName,
                             s.LastName,
                             s.BirthDate,
                             s.Phone,
                             s.Email
                      from rm.tRoomies s
                      where s.roomieId = @RoomieId;",
                    new { RoomieId = roomieId } );

                if( student == null ) return Result.Failure<RoomieData>( Status.NotFound, "Student not found." );
                return Result.Success( student );
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
                        if( status == 1 ) return Result.Failure<int>( Status.BadRequest, "A student with this name already exists." );
                        if( status == 2 ) return Result.Failure<int>( Status.BadRequest, "A student with GitHub login already exists." );

                        Debug.Assert( status == 0 );
                        return Result.Success( Status.Created, p.Get<int>( "@RoomieId" ) );
                    }
         
        }
        bool IsNameValid( string name ) => !string.IsNullOrWhiteSpace( name );
    }
}
