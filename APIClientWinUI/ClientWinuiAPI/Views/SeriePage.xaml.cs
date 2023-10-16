using ClientWinuiAPI.ViewModels;

using Microsoft.UI.Xaml.Controls;

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
        InitializeComponent();
    }
}
