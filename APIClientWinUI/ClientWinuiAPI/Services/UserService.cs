using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientWinuiAPI.Models;

namespace ClientWinuiAPI.Services;
internal class UserService
{
    public WSService WSService
    { get; set; }
    private static UserService? instance;

    public static UserService GetService
    {
        get
        {
            instance ??= new UserService();
            return instance;
        }
    }

    private readonly string controller = "Utilisateurs";
    public UserService()
    {
        WSService = WSService.GetService(controller);
    }

    public async Task<Utilisateur> GetUserByEmail(string email)
    {
        var user = await WSService.GetAsync<Utilisateur>(controller + "/GetByEmail/" + email);
        return user;
    }

    public async Task<Utilisateur> GetUserById(int id)
    {
        var user = await WSService.GetAsync<Utilisateur>(controller + "/GetById/" + id.ToString());
        return user;
    }

    public async Task<Utilisateur> PostUser(Utilisateur utilisateur)
    {
        var user = await WSService.PostAsync(controller, utilisateur);
        return user;
    }

    public async Task<HttpResponseMessage> PutUser(int id, Utilisateur utilisateur)
    {
        var response = await WSService.PutAsync(controller + "/" + id.ToString(), utilisateur);
        return response;
    }

    public async Task<HttpResponseMessage> DeleteUser(int id)
    {
        var response = await WSService.DeleteAsync(controller + "/" + id.ToString());
        return response;
    }
}
