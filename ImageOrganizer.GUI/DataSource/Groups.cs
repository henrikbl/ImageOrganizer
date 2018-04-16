using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ImageOrganizer.GUI.DataSource
{
    class Groups
    {
        public static Groups Instance { get; } = new Groups();

        private const string baseUri = "";

        private HttpClient client;

        private Groups()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(baseUri)
            };
        }
        
        // Retrieve list of groups from the database.
        public async Task<Groups[]> getGroups()
        {
            try
            {
                var json = await client.GetStringAsync("groups").ConfigureAwait(false);
                Groups[] groups = JsonConvert.DeserializeObject<Groups[]>(json);
                return groups;
            }
            catch (Exception)
            {
                throw;
            }     
        }
        
    }
}
