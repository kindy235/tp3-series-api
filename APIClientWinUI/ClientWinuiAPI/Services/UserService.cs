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

    public async Task<Utilisateur?> GetUserByEmail(string email)
    {
        var response = await WSService.GetAsync<Utilisateur>(controller + "/GetByEmail/" + email);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<Utilisateur>();
        }
        else
        {
            return null;
        }
    }

    public async Task<Utilisateur?> GetUserById(int id)
    {
        var response = await WSService.GetAsync<Utilisateur>(controller + "/GetById/" + id.ToString());
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<Utilisateur>();
        }
        else
        {
            return null;
        }
    }

    public async Task<Utilisateur?> PostUser(Utilisateur utilisateur)
    {
        utilisateur.UtilisateurId = 0;
        var response = await WSService.PostAsync(controller, utilisateur);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<Utilisateur>();
        }
        else
        {
            return null;
        }
    }

    public async Task<bool> PutUser(int id, Utilisateur utilisateur)
    {
        var response = await WSService.PutAsync(controller + "/" + id.ToString(), utilisateur);
        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> DeleteUser(int id)
    {
        var response = await WSService.DeleteAsync(controller + "/" + id.ToString());
        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
