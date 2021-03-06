using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace ITI.Roomies.WebApp
{
    public class Program
    {
        public static void Main( string[] args )
        {
            BuildWebHost( args ).Run();
        }

        public static IWebHost BuildWebHost( string[] args ) =>
            new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot( Directory.GetCurrentDirectory() )
                .ConfigureAppConfiguration( ( hostingContext, config ) =>
                 {

                     config.AddJsonFile( "appsettings.json", false, true );
                     config.AddEnvironmentVariables();

                     if( args != null )
                     {
                         config.AddCommandLine( args );
                     }
                 } )
                .ConfigureLogging( ( hostingContext, logging ) =>
                 {
                     logging.AddConfiguration( hostingContext.Configuration.GetSection( "Logging" ) );
                     logging.AddConsole();
                     logging.AddDebug();
                 } )
                .UseStartup<Startup>()
                .UseUrls( "http://0.0.0.0:5000" )
                .Build();
    }
}
