using System.IO;
using ITI.Roomies.DAL;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ITI.Roomies.WebApp.Authentication;
using Microsoft.AspNetCore.Authorization;


namespace ITI.Roomies.WebApp.Controllers
{
    [Route( "api/[controller]" )]
    [Authorize( AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme )]
    public class ImageController : Controller
    {
        readonly ImageGateway _imageGateway;
        public ImageController( ImageGateway imageGateway )
        {
            _imageGateway = imageGateway;
        }

        [HttpPost( "/uploadImage" )]
        public async Task<IActionResult> UploadImage( IFormFile image )
        {
            int roomieId = int.Parse( HttpContext.User.FindFirst( c => c.Type == ClaimTypes.NameIdentifier ).Value );

            string result = await _imageGateway.UploadImage( image, roomieId );
            if( image.Length == 0 )
            {
                return Ok();
            }
            return Ok( result );

            ;
        }

        [HttpPost( "/DownloadImage" )]
        public async Task<IActionResult> DownloadImage( string imageName )
        {
            MemoryStream file = await _imageGateway.DownloadImage( imageName );
            string GetType = await _imageGateway.GetContentType( imageName );
            return File( file, "application/octet-stream", "profilePic.jpg" );
        }


    }
}
