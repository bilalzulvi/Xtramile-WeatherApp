using System.Net.Http;
using System.Threading.Tasks;
using WeatherApi.ServiceContract.Response;

namespace WeatherApi.ServiceContract
{
    public interface IApiManager
    {
        Task<GenericResponse<string>> SendRequestAsync(string requestUrl, string httpMethod);

        Task<GenericResponse<string>> SendRequestAsync(string requestUrl, string httpMethod, StringContent content);
    }
}
