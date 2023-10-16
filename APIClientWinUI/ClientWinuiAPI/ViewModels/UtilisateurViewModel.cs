using System.Diagnostics.Tracing;
using System.Windows.Input;
using ClientWinuiAPI.Models;
using ClientWinuiAPI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using java.beans;
using java.lang;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace ClientWinuiAPI.ViewModels;


public partial class UtilisateurViewModel : ObservableRecipient
{
    private readonly UserService userService;
    public UtilisateurViewModel()
    {
        userService = UserService.GetService;
        SearchUserByEmail = new AsyncRelayCommand(ActionSearchUserByEmail);
        BtnModifyUtilisateurCommand = new AsyncRelayCommand(ActionBtnModifyUtilisateurCommand);
        BtnClearUtilisateurCommand = new AsyncRelayCommand(ActionBtnClearUtilisateurCommand);
        BtnAddUtilisateurCommand = new AsyncRelayCommand(ActionBtnAddUtilisateurCommand);
    }

   

    private Utilisateur utilisateur;
    public Utilisateur? Utilisateur
    {
        get => utilisateur;
        set
        {
            if (utilisateur != value)
            {
                utilisateur = value;
                OnPropertyChanged(nameof(Utilisateur));
            }
        }
    }

    public ICommand BtnModifyUtilisateurCommand{ get; set;}
    public ICommand BtnClearUtilisateurCommand{ get; set;}
    public ICommand BtnAddUtilisateurCommand{ get; set;}

    public ICommand SearchUserByEmail
    {
        get; set;
    }
    public string TextSearchMail
    {
        get => textSearchMail;
        set
        {
            textSearchMail = value;
            OnPropertyChanged(nameof(TextSearchMail));
        }
    }

    private string textSearchMail;
    

    public async Task ActionSearchUserByEmail()
    {
        Utilisateur = await userService.GetUserByEmail(textSearchMail);
    }

    public async Task ActionBtnModifyUtilisateurCommand()
    {
        // Assurez-vous que l'utilisateur n'est pas null
        if (Utilisateur == null)
        {
            // Gérez l'erreur ou affichez un message à l'utilisateur
            return;
        }

        // Appel de la méthode de service PUT pour mettre à jour l'utilisateur
        var response = await userService.PutUser(Utilisateur.UtilisateurId, Utilisateur);

        // Gérez la réponse, par exemple, vérifiez si la mise à jour a réussi
        if (response.IsSuccessStatusCode)
        {
            // La mise à jour a réussi, vous pouvez afficher un message ou effectuer d'autres actions.
            // Où que vous soyez dans votre code
            var contentDialog = new ContentDialog
            {
                Title = "Modification",
                Content = "Modification réussie",
                PrimaryButtonText = "OK"
            };
            try
            {
                await contentDialog.ShowAsync();
            }
            catch (System.Exception){}

        }
        else
        {
            // La mise à jour a échoué, gérez l'erreur ou affichez un message d'erreur.
        }
    }

    private Task ActionBtnAddUtilisateurCommand() => throw new NotImplementedException();
    private Task ActionBtnClearUtilisateurCommand()
    {
        Utilisateur = null;
        return Task.CompletedTask;
    }

    /*public string UserNom { get; set; }
    public string UserPrenom{ get; set;}
    public string UserMobile { get; set; }
    public string UserEmail { get; set; }
    public string UserPwd { get; set;}
    public string UserRue { get; set;}
    public string UserPostal { get; set;}
    public string UserVille { get; set;}
    public string UserPays { get; set;}
    public string UserLat { get; set;}
    public string UserLong { get; set;}*/
}
