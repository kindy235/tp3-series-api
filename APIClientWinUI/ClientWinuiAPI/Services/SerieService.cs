using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientWinuiAPI.Models;
using java.util;

namespace ClientWinuiAPI.Services;
internal class SerieService
{
    private readonly string baseUrl = "https://localhost:44358/api/Series/";

    private readonly WSService WSService;
    
    private static SerieService? instance;

    public static SerieService GetService
    {
        get
        {
            instance ??= new SerieService();
            return instance;
        }
    }

    private SerieService()
    {
        WSService = new WSService(baseUrl);
    }

    public async Task<List<Serie>?> GetSeriesByTitle(string title)
    {
        var response = await WSService.GetAsync<Serie>("GetByTitle/" + title);
        if (response.IsSuccessStatusCode)
        {
            var series = await response.Content.ReadAsAsync<List<Serie>>();
            return series;
        }
        else
        {
            return null;
        }
    }
}
