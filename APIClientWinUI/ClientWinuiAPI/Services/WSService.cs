using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ClientWinuiAPI.Services;


public class WSService
{
    private readonly HttpClient httpClient;

    public WSService(string baseUrl)
    {
        httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        };
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<HttpResponseMessage> GetAsync<T>(string endpoint)
    {
        var response = await httpClient.GetAsync(endpoint);
        return response;
    }

    public async Task<HttpResponseMessage> PostAsync<T>(string endpoint, T data)
    {
        var response = await httpClient.PostAsJsonAsync(endpoint, data);
        return response;
    }

    public async Task<HttpResponseMessage> PutAsync<T>(string endpoint, T data)
    {
        var response = await httpClient.PutAsJsonAsync(endpoint, data);
        return response;
    }

    public async Task<HttpResponseMessage> DeleteAsync(string endpoint)
    {
        var response = await httpClient.DeleteAsync(endpoint);
        return response;
    }
}
