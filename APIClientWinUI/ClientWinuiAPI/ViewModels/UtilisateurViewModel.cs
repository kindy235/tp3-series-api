using System.Diagnostics.Tracing;
using System.Windows.Input;
using ClientWinuiAPI.Models;
using ClientWinuiAPI.Services;
using com.sun.org.apache.xpath.@internal.operations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using java.beans;
using java.lang;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Exception = System.Exception;

namespace ClientWinuiAPI.ViewModels;


public partial class UtilisateurViewModel : ObservableRecipient
{
    private readonly UserService userService;
    private readonly BingMapService bingMapService;
    private Utilisateur utilisateur;

    public UtilisateurViewModel()
    {
        userService = UserService.GetService;
        bingMapService = BingMapService.GetService;
        utilisateur = new Utilisateur();
        Utilisateur = utilisateur;
        SearchUserByEmail = new AsyncRelayCommand(ActionSearchUserByEmail);
        BtnModifyUtilisateurCommand = new AsyncRelayCommand(ActionBtnModifyUtilisateurCommand);
        BtnClearUtilisateurCommand = new AsyncRelayCommand(ActionBtnClearUtilisateurCommand);
        BtnAddUtilisateurCommand = new AsyncRelayCommand(ActionBtnAddUtilisateurCommand);

    }

   

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
        if (Utilisateur == null)
        {
            await ShowDialog("email "+ textSearchMail + " introuvable !");
        }
    }

    public async Task ActionBtnModifyUtilisateurCommand()
    {
        if (Utilisateur == null)
        {
            await ShowDialog("Formulaire invalide !");
            return;
        }

        var success = await userService.PutUser(Utilisateur.UtilisateurId, Utilisateur);

        if (success)
        {
            await ShowDialog("Utilisateur " + Utilisateur.Nom + " modifié avec succès !");
        }
        else
        {
            await ShowDialog("Echec de la Modification !");
        }
    }

    private async Task ActionBtnAddUtilisateurCommand()
    {
        if (Utilisateur == null)
        {
            await ShowDialog("Formulaire invalide !");
            return;
        }

        try
        {
            var rootObject = await bingMapService.GetCoordinates(Utilisateur.Rue, Utilisateur.CodePostal, Utilisateur.Ville);
            if (rootObject != null)
            {
                var lat = rootObject.resourceSets[0].resources[0].point.coordinates[0];
                var lng = rootObject.resourceSets[0].resources[0].point.coordinates[1];
                Utilisateur.Latitude = (decimal)lat;
                Utilisateur.Longitude = (decimal)lng;
            }
            else
            {
                await ShowDialog("Adresse fournie invalide");
                return;
            }
        }
        catch (Exception) { }

        var user = await userService.PostUser(Utilisateur);

        if (user != null)
        {
            await ShowDialog("Utilisateur "+ Utilisateur.Nom + " ajouté avec succès !");
        }
        else
        {
            await ShowDialog("Opération d'ajout non réussie !");
        }
    }
    private Task ActionBtnClearUtilisateurCommand()
    {
        Utilisateur = null;
        return Task.CompletedTask;
    }

    private static async Task ShowDialog(string message)
    {
        try
        {
            var contentDialog = new ContentDialog
            {
                Title = "Information",
                Content = message,
                PrimaryButtonText = "OK",
                XamlRoot = App.MainRoot.XamlRoot
            };
            await contentDialog.ShowAsync();
        }
        catch (System.Exception) { }
    }
}
