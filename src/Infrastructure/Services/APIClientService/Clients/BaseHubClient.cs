using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Infrastructure.Services.APIClientService.Clients
{
    public abstract class BaseHubClient
    {
        protected HttpContent CreateHttpContent<T>(T content)
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

        protected static T GetCreatedResponse<T>(string responseMessage)
        {
            var model = JsonConvert.DeserializeObject<T>(responseMessage);

            return model;
        }
    }
}