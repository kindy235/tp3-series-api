using ClientWinuiAPI.Services;
using ClientWinuiAPI.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace ClientWinuiAPI.Views;

public sealed partial class UtilisateurPage : Page
{
    public UtilisateurViewModel ViewModel
    {
        get;
    }

    public UtilisateurPage()
    {
        ViewModel = App.GetService<UtilisateurViewModel>();
        DataContext = ViewModel;
        InitializeComponent();
    }
}
