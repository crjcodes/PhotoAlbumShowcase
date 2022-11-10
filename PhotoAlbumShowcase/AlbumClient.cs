using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text.Json;

namespace PhotoAlbumShowcase
{
    static internal class AlbumClient
    {
        /// <summary>
        /// In a production application, there would be auth and perhaps this connection
        /// would be stored in something like Azure App Configuration
        /// </summary>
        private const string _apiEndpoint = "https://jsonplaceholder.typicode.com/";

        private static readonly HttpClient _httpClient = new HttpClient()
        {
            BaseAddress = new Uri(_apiEndpoint)
        };

        private static readonly JsonSerializerOptions _jsonSerializerOptions
            = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        /// <summary>
        /// Accesses the endpoint for the requested album. In a real-life app, would be doing 
        /// this asynchronously, but that's coding overhead not needed for this showcase at this time
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<Album>> Get(int albumId)
        {
            var suffix = string.Format("photos?albumId={0}", albumId);
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<Album>>(suffix) ?? 
                new List<Album>();

            return response;
        }
    }
}
