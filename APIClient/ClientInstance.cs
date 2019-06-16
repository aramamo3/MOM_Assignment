using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace APIBase
{
    public class ClientInstance
    {
        private readonly HttpClient _httpClient;
        private string _uri;
        private string _resourceId;

        public ClientInstance(string uri, string resourceId)
        {
            _httpClient = new HttpClient();

            _uri = uri;
            _resourceId = resourceId;

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> GetData<T>(string queryString, string value) where T: class
        {
            Uri uri = QueryBuilder(queryString, value);

            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadAsStreamAsync();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            return ser.ReadObject(data) as T;              

        }

        private Uri QueryBuilder(string queryString, string value)
        {
            StringBuilder qry = new StringBuilder();
            qry.AppendFormat("resource_id={0}", _resourceId);

            if (!string.IsNullOrEmpty(queryString))
            {
                qry.AppendFormat("&{0}={1}", queryString, value);
            }

            UriBuilder builder = new UriBuilder(_uri);
            builder.Query = qry.ToString();

            return builder.Uri;
        }
     
    }
    
}
