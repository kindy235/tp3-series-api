using ClientWinuiAPI.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace ClientWinuiAPI.ViewModels;

public partial class SerieDetailsViewModel : ObservableRecipient
{
    public Serie? serie;
    public SerieDetailsViewModel()
    {
        Serie = new Serie();
        Serie = serie;
        AddNotationtoSerie = new RelayCommand(PerformAddNotationtoSerie);

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

    public ICommand AddNotationtoSerie
    {
        get; set;
    }

    private void PerformAddNotationtoSerie()
    {
    }

    private string notationValue;

    public string NotationValue
    {
        get => notationValue;
        set => SetProperty(ref notationValue, value);
    }
}
