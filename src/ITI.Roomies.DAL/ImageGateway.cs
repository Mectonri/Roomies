using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ITI.Roomies.DAL
{
    public class ImageGateway
    {
        readonly string _connectingString;
        readonly string _path;
        readonly string _pathForDownload;

        public ImageGateway( string connectingString )
        {
            _connectingString = connectingString;
            _path = "../ITI.Roomies.WebApp/wwwroot/Pictures";
            _pathForDownload = "../ITI.Roomies.WebApp/wwwroot";

        }

        public async Task<string> UploadImage( IFormFile image, int roomieId )
        {
            string message = "imahe has been uploaded";
            string path = _path + "/" + roomieId;
            this.ExistDirectory( path );
            string filePath = Path.Combine( path, image.FileName );
            using( var fileStream = new FileStream( filePath, FileMode.Create ) )
            {
                await image.CopyToAsync( fileStream );
            }
            return message;
        }

        public async Task<MemoryStream> DownloadImage( string imageName )
        {
            MemoryStream download = new MemoryStream();
            string path = _pathForDownload + imageName;
            using( FileStream stream = new FileStream( path, FileMode.Open ) )
            {
                await stream.CopyToAsync( download );
            }
            download.Position = 0;
            return download;
        }

        public async Task<string> GetContentType( string filename )
        {
            string path = _pathForDownload + filename;

            string extension = Path.GetExtension( path );

            return extension;
        }

        internal void ExistDirectory( string path )
        {
            if( !Directory.Exists( path ) )
            {
                var test = Directory.GetCurrentDirectory();
                Directory.CreateDirectory( path );
            }
        }

    }
}
