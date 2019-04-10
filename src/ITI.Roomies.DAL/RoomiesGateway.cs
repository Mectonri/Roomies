using System;
using System.Collections.Generic;
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

        public RoomiesGateway( string connectionString )
        {
            _connectionString = connectionString;
        }
        public async Task<Result<RoomiesData>> FindById( int roomieId )
        {
            using( SqlConnection con = new SqlConnection( _connectionString ) )
            { 
                RoomiesData roomie = await con.QueryFirstOrDefaultAsync<RoomiesData>(
                    @"select s.RoomieId,
                             s.FirstName,
                             s.LastName,
                             s.BirthDate,
                             s.Phone,
                             s.Email
                      from rm.tRoomies s
                      where s.roomieId = @RoomieId;",
                    new { RoomieId = roomieId } );

                if( roomie == null ) return Result.Failure<RoomiesData>( Status.NotFound, "Student not found." );
                return Result.Success( roomie );
            }
        }

        public async Task<Result<int>> CreateRoomie( string firstName, string lastName, DateTime birthDate, string phone, int userId )
                {
                    if( !IsNameValid( firstName ) ) return Result.Failure<int>( Status.BadRequest, "The first name is not valid." );
                    if( !IsNameValid( lastName ) ) return Result.Failure<int>( Status.BadRequest, "The last name is not valid." );

                    using( SqlConnection con = new SqlConnection( _connectionString ) )
                    {
                        var p = new DynamicParameters();
                        p.Add( "@FirstName", firstName );
                        p.Add( "@LastName", lastName );
                        p.Add( "@BirthDate", birthDate );
                        p.Add( "@Phone", phone);
                        p.Add( "@userId", userId);
                        p.Add( "@RoomieId", dbType: DbType.Int32, direction: ParameterDirection.Output );
                        p.Add( "@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue );
                        await con.ExecuteAsync( "rm.sRoomiesCreate", p, commandType: CommandType.StoredProcedure );

                        int status = p.Get<int>( "@Status" );
                        if( status == 1 ) return Result.Failure<int>( Status.BadRequest, "A roomie with this name already exists." );

                        Debug.Assert( status == 0 );
                        return Result.Success( Status.Created, p.Get<int>( "@RoomieId" ) );
                    }
         
        }
        bool IsNameValid( string name ) => !string.IsNullOrWhiteSpace( name );
    }
}
