using ClientWinuiAPI.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ClientWinuiAPI.ViewModels;

public partial class SerieDetailsViewModel : ObservableRecipient
{
    public Serie? serie;
    public SerieDetailsViewModel()
    {
        Serie = new Serie();
        Serie = serie;
    }

    public Serie? Serie
    {
        get => serie;
        set
        {
            if (serie != value)
            {
                serie = value;
                OnPropertyChanged(nameof(serie));
            }
        }
    }
}
