using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace Infrastructure.Services.APIClientService.Clients
{
    public partial class GitLabHubClient
    {
        private HttpClient Client { get; }

        public GitLabHubClient(
            HttpClient client)
        {
            client.BaseAddress = new Uri("https://gitlab.com/api/v4/");

            this.Client = client;
        }

        private HttpContent CreateHttpContent<T>(T content)
        {
            var json = JsonConvert.SerializeObject(content, MicrosoftDateFormatSettings);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private static JsonSerializerSettings MicrosoftDateFormatSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
            }
        }
    }
}