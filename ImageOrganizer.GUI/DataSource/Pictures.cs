using ImageOrganizer.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace ImageOrganizer.GUI.DataSource
{
    class Pictures
    {
        public static Pictures Instance { get; } = new Pictures();

        private const string baseUri = "http://localhost:55850/api/";

        private HttpClient client;

        private Pictures()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(baseUri)
            };
        }

        // Add a picture to database
        public async Task<bool> AddPicture(Picture picture)
        {
            try
            {
                string objectBody = JsonConvert.SerializeObject(picture);
                var response = await client.PostAsync("pictures", new StringContent(objectBody, Encoding.UTF8, "application/json")).ConfigureAwait(false);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        // Add a picture to a group
        public async Task<bool> AddPictureToGroup(int pictureId, int groupId)
        {
            try
            {
                Uri addPictureToGroup = new Uri(baseUri+"Pictures/" + pictureId + "/Groups/" + groupId);
                var response = await client.PostAsync(addPictureToGroup, null).ConfigureAwait(false);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

    }
}
