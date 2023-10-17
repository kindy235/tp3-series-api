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
        if (Utilisateur == null)
        {
            await ShowDialog("Modification", "Formulaire invalide");
            return;
        }

        var success = await userService.PutUser(Utilisateur.UtilisateurId, Utilisateur);

        if (success)
        {
            await ShowDialog("Modification", "Modification réussie");
        }
        else
        {
            await ShowDialog("Modification", "Echec de la Modification");
        }
    }

    private async Task ActionBtnAddUtilisateurCommand()
    {
        if (Utilisateur == null)
        {
            await ShowDialog("Ajout", "Formulaire invalide");
            return;
        }

        var user = await userService.PostUser(Utilisateur);

        if (user != null)
        {
            await ShowDialog("Ajout", "Utilisateur ajouté avec succès");
        }
        else
        {
            await ShowDialog("Ajout", "Opération d'ajout non réussie");
        }
    }
    private Task ActionBtnClearUtilisateurCommand()
    {
        Utilisateur = null;
        return Task.CompletedTask;
    }

    private static async Task ShowDialog(string title, string message)
    {
        try
        {
            var contentDialog = new ContentDialog
            {
                Title = title,
                Content = message,
                PrimaryButtonText = "OK",
                XamlRoot = App.MainRoot.XamlRoot
            };
            await contentDialog.ShowAsync();
        }
        catch (System.Exception) { }
    }
}
