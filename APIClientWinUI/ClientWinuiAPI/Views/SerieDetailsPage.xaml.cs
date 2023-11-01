using ClientWinuiAPI.Models;
using ClientWinuiAPI.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

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

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        var selectedSerie = (Serie)e.Parameter;
        if (selectedSerie != null ) {
            ViewModel.Serie = selectedSerie;
        }
    }

    private void BackButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        // Use Frame to navigate back to the list of series
        if (Frame.CanGoBack)
        {
            Frame.GoBack();
        }
        else
        {
            Frame.Navigate(typeof(SeriePage));
        }
    }
}
