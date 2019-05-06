using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ITI.Roomies.DAL
{
    public class CourseGateway
    {
        readonly string _connectionString;

        public CourseGateway( string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Result<CourseData>> FindById( int courseId )
        {
            using( SqlConnection con = new SqlConnection( _connectionString ) )
            {
                CourseData course = await con.QueryFirstOrDefault<CourseData>(
                    @"select c.CourseId,
                             c.CourseName,
                             c.CourseDate,
                             c.CoursePrice,
                            c.CollocId
                        from rm.tCourse c
                        where c.CourseId = @CourseId;",
                    new { CourseId = courseId } );

                if( course == null ) return Result.Failure<CourseData>( Status.NotFound, "Course not found" );
                return Result.Success( course );
            }
        }
    }
}
