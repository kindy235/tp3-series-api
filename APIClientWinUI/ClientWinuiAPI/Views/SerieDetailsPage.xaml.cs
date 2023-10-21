using ClientWinuiAPI.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace ClientWinuiAPI.Views;

public sealed partial class SerieDetailsPage : Page
{
    public SerieDetailsViewModel ViewModel
    {
        get;
    }

    public SerieDetailsPage()
    {
        ViewModel = App.GetService<SerieDetailsViewModel>();
        DataContext = ViewModel;
        InitializeComponent();
    }
}
