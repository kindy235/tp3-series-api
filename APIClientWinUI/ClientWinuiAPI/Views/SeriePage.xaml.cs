using System.Collections.ObjectModel;
using ClientWinuiAPI.Models;
using ClientWinuiAPI.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace ClientWinuiAPI.Views;

public sealed partial class SeriePage : Page
{
    public SerieViewModel ViewModel
    {
        get;
    }

    public SeriePage()
    {
        ViewModel = App.GetService<SerieViewModel>();
        DataContext = ViewModel;
        InitializeComponent();
    }

    private void OnSerieSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // The selected item is in the 'AddedItems' collection.
        if (e.AddedItems.Count > 0)
        {
            if (e.AddedItems[0] is Serie selectedItem)
            {
                ViewModel.NavigationFrame = Frame;
                ViewModel.SelectedSerie = selectedItem;
            }
        }
    }

}
