using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace ITI.Roomies.DAL.Tests
{
    public static class TestHelpers
    {
        static readonly Random _random = new Random();
        static IConfiguration _configuration;

        public static string ConnectionString
        {
            get
            {
                return Configuration["ConnectionStrings:RoomiesDB"];
            }
        }

        static IConfiguration Configuration
        {
            get
            {
                if( _configuration == null )
                {
                    _configuration = new ConfigurationBuilder()
                        .SetBasePath( Directory.GetCurrentDirectory() )
                        .AddJsonFile( "appsettings.json", optional: true )
                        .AddEnvironmentVariables()
                        .Build();
                }

                return _configuration;
            }
        }

        public static string RandomPhone() => _random.Next(99999, 9999999).ToString();

        public static string RandomTestName() => string.Format( "Test-{0}", Guid.NewGuid().ToString().Substring( 24 ) );

        public static DateTime RandomBirthDate( int age ) => DateTime.UtcNow.AddYears( -age ).AddMonths( _random.Next( -11, 0 ) ).Date;
    }
}
