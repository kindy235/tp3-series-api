using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ClientWinuiAPI.Services;


public class WSService
{
    private readonly string baseUrl = "https://localhost:44358/api/";
    private readonly HttpClient httpClient;
    private static WSService? instance;

    public static WSService GetService(string serviceName)
    {
            instance ??= new WSService(serviceName);
            return instance;
    }


    public WSService(string controller)
    {
        httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseUrl+controller)
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
        /*if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<T>();
        }
        else
        {
            throw new Exception("Error accessing the API: " + response.ReasonPhrase);
        }*/
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
