using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApi.Dto;
using WeatherApi.ServiceContract;
using WeatherApi.ServiceContract.Response;

namespace WeatherApi.Service
{
    [ExcludeFromCodeCoverage]
    public class ApiManager : IApiManager
    {
        #region Public Methods

        public async Task<GenericResponse<string>> SendRequestAsync(string requestUrl, string httpMethod)
        {
            return await this.SendRequestAsync(requestUrl, httpMethod, null);
        }

        public async Task<GenericResponse<string>> SendRequestAsync(string requestUrl, string httpMethod, StringContent content)
        {
            var response = new GenericResponse<string>();

            try
            {
                using var client = new HttpClient();
#if DEBUG
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
#endif

                using var httpRequest = new HttpRequestMessage(new HttpMethod(httpMethod), requestUrl);
                if (content != null)
                {
                    httpRequest.Content = content;
                }

                var apiResponse = await client.SendAsync(httpRequest);
                if (!apiResponse.IsSuccessStatusCode)
                {
                    var stringResponse = await apiResponse.Content.ReadAsStringAsync();
                    var errorContent = JsonConvert.DeserializeObject<ApiErrorDto>(stringResponse);

                    response.AddErrorMessage(errorContent.Message);
                    return response;
                }

                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                response.Data = apiContent;
            }
            catch (Exception exception)
            {
                response.AddErrorMessage(string.Format(Resource.General_FailedApiRequest, requestUrl, exception.Message));
            }

            return response;
        }

        #endregion
    }
}
