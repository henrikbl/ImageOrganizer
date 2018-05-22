using ImageOrganizer.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ImageOrganizer.GUI.DataSource
{
    class Pictures
    {
        public static Pictures Instance { get; } = new Pictures();
        public static int CurrentPictureId { get; set; }

        private const string baseUri = "http://localhost:55850/api/";

        private HttpClient client;

        private Pictures()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(baseUri)
            };
        }

        // Get pictures from group in database
        public async Task<Picture[]> GetPictures(int id)
        {
            var json = await client.GetStringAsync("pictures/groups/" + id).ConfigureAwait(false);
            Picture[] pictures = JsonConvert.DeserializeObject<Picture[]>(json);
            return pictures;
        }

        // Update a picture in database
        public async Task<bool> UpdatePicture(Picture picture)
        {
            string objectBody = JsonConvert.SerializeObject(picture);
            var response = await client.PutAsync("pictures/" + picture.PictureId, new StringContent(objectBody, Encoding.UTF8, "application/json")).ConfigureAwait(false);

            return response.IsSuccessStatusCode;
        }

        // Add a picture to database
        public async Task<bool> AddPicture(Picture picture)
        {

            string objectBody = JsonConvert.SerializeObject(picture);
            var response = await client.PostAsync("pictures", new StringContent(objectBody, Encoding.UTF8, "application/json")).ConfigureAwait(false);

            string[] location = response.Headers.Location.Segments;
            CurrentPictureId = Int32.Parse(location[location.Length - 1]);

            return response.IsSuccessStatusCode;
        }

        // Add a picture to a group
        public async Task<bool> AddPictureToGroup(int pictureId, int groupId)
        {
            Uri addPictureToGroup = new Uri(baseUri + "pictures/" + pictureId + "/groups/" + groupId);
            string urlString = baseUri + "Pictures/" + pictureId + "/Groups/" + groupId;
            var response = await client.PostAsync(urlString, null).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }

        // Delete a picture from database
        public async Task<bool> DeletePicture(int id)
        {

            var response = await client.DeleteAsync("pictures/" + id).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }
    }
}
