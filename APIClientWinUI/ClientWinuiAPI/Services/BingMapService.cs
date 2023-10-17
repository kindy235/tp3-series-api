using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using ClientWinuiAPI.Core.Helpers;
using ClientWinuiAPI.Models;
using Newtonsoft.Json;

namespace ClientWinuiAPI.Services;
internal class BingMapService
{
    private readonly string BaseUrl = "http://dev.virtualearth.net/REST/v1/Locations/";
    private readonly string key = "key=Ag7KBEIMrvvjF2Kpz9Ze9UaNNoj1jkizmw-_bxWFpRaLJEXzBGNW-IFl4aHj5jd1";
    public WSService WSService
    {
        get; set;
    }
    private static BingMapService? instance;

    public static BingMapService GetService
    {
        get
        {
            instance ??= new BingMapService();
            return instance;
        }
    }

    public BingMapService()
    {
        WSService = new WSService(BaseUrl);
    }


    public async Task<RootObject?> GetCoordinates(string rue, string codePostal, string ville)
    {
        var response = await WSService.GetAsync<RootObject>("FR/"+ codePostal + "/" + ville + "/" + rue + "?" + key);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<RootObject>();
        }
        else
        {
            return null;
        }
    }
}
