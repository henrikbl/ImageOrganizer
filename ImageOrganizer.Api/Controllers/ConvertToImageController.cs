using ImageOrganizer.DataAccess;
using ImageOrganizer.Model;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ImageOrganizer.Api.Controllers
{
    public class ConvertToImageController : ApiController
    {

        private ImageOrganizerContext db = new ImageOrganizerContext();


        [Route("api/images/0/{url}")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> GetAsync(string url)
        {
            try
            {
                string filePath = url;
                var image = File.ReadAllBytes(filePath);
                var ms = new MemoryStream(image);
                var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StreamContent(ms) };
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(@"image/jpeg");
                return response;
            }
            catch (System.Exception)
            {
                throw;
            }

        }

        // Creates an image object of the filepath stored in the database. 
        /*
         * TODO move to pictureController and add to get method.
         */
        [Route("api/images/{id}")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> GetFromDbAsync(int id)
        {
            try
            {
                Picture picture = await db.Pictures.FindAsync(id);

                string url = picture.FilePath;
                var image = File.ReadAllBytes(url);
                var ms = new MemoryStream(image);
                var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StreamContent(ms) };
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(@"image/jpeg");
                return response;
            }
            catch (System.Exception)
            { 
                throw;
            }
            
        }
    }
}
