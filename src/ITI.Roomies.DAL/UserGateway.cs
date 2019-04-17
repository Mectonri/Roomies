using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using Dapper;
using System;

namespace ITI.Roomies.DAL
{
    public class UserGateway
    {
        readonly string _connectionString;

        public UserGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<UserData> FindById(int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return await con.QueryFirstOrDefaultAsync<UserData>(
                    "select u.UserId, u.Email, u.[Password], u.GoogleRefreshToken, u.GoogleId from rm.vUser u where u.UserId = @UserId",
                    new { UserId = userId });
            }
        }

        public async Task<UserData> FindByEmail(string email)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return await con.QueryFirstOrDefaultAsync<UserData>(
                    "select u.UserId, u.Email, u.[Password], u.GoogleRefreshToken, u.GoogleId from rm.vUser u where u.Email = @Email",
                    new { Email = email });
            }
        }

        public async Task<UserData> FindByGoogleId(string googleId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return await con.QueryFirstOrDefaultAsync<UserData>(
                    "select u.UserId, u.Email, u.[Password], u.GoogleRefreshToken, u.GoogleId from rm.vUser u where u.GoogleId = @GoogleId",
                    new { GoogleId = googleId });
            }
        }

        public async Task<Result<int>> CreatePasswordUser( string email, byte[] password)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@Email", email);
                p.Add("@Password", password);
                p.Add("@UserId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("rm.sPasswordUserCreate", p, commandType: CommandType.StoredProcedure);


                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.BadRequest, "An account with this email already exists.");

                Debug.Assert(status == 0);
                return Result.Success(p.Get<int>("@UserId"));

            }
        }
        public async Task<Result<int>> CreateRoomie (string firstName, string lastName, DateTime birthDate, string Phone, int userId)
        {
            
            using( SqlConnection con = new SqlConnection( _connectionString ) )
            {
                var p = new DynamicParameters();
                
                p.Add( "@FirstName", firstName );
                p.Add( "@LastName", lastName );
                p.Add( "@userId", userId );
                p.Add( "@BirthDate", birthDate );
                p.Add( "@Phone", Phone ?? string.Empty );
                p.Add( "@RoomieId", dbType: DbType.Int32, direction: ParameterDirection.Output );
                p.Add( "@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue );
                await con.ExecuteAsync( "rm.sRoomiesCreate", p, commandType: CommandType.StoredProcedure );

                int status = p.Get<int>( "@Status" );
                if( status == 1 ) return Result.Failure<int>( Status.BadRequest, "A roomie with this name already exists." );
                Debug.Assert( status == 0 );
                return Result.Success( Status.Created, p.Get<int>( "@RoomieId" ) );
            }
        }
        public async Task<Result<RoomiesData>> FindRoomieById( int roomieId )
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
                      where s.RoomieId = @RoomieId;",
                    new { RoomieId = roomieId } );

                if( roomie == null ) return Result.Failure<RoomiesData>( Status.NotFound, "Roomie not found." );
                return Result.Success( roomie );
            }
        }


        public async Task CreateOrUpdateGoogleUser(string email, string googleId, string refreshToken)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.ExecuteAsync(
                    "rm.sGoogleUserCreateOrUpdate",
                    new { Email = email, GoogleId = googleId, RefreshToken = refreshToken },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<string>> GetAuthenticationProviders(string userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return await con.QueryAsync<string>(
                    "select p.ProviderName from rm.vAuthenticationProvider p where p.UserId = @UserId",
                    new { UserId = userId });
            }
        }

        public async Task Delete(int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.ExecuteAsync("rm.sUserDelete", new { UserId = userId }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateEmail(int userId, string email)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.ExecuteAsync(
                    "rm.sUserUpdate",
                    new { UserId = userId, Email = email },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdatePassword(int userId, byte[] password)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.ExecuteAsync(
                    "rm.sPasswordUserUpdate",
                    new { UserId = userId, Password = password },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
