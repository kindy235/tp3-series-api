using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ClientWinuiAPI.Models;
using ClientWinuiAPI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;

namespace ClientWinuiAPI.ViewModels;

public partial class SerieViewModel : ObservableRecipient
{
    private readonly SerieService serieService;
    private ObservableCollection<Serie> seriesList;

    public SerieViewModel()
    {
        serieService = SerieService.GetService;
        seriesList = new ObservableCollection<Serie>();
        SearchSeriesByTitle = new AsyncRelayCommand(ActionSearchSeriesByTitle);
        //LoadSeriesAsync("titan");
    }


    public ObservableCollection<Serie> SeriesList
    {
        get => seriesList;
        set
        {
            if (seriesList != value)
            {
                seriesList = value;
                OnPropertyChanged(nameof(SeriesList));
            }
        }
    }


    public ICommand SearchSeriesByTitle
    {
        get; set;
    }
    public string TextSearchSeries
    {
        get => textSearchSeries;
        set
        {
            textSearchSeries = value;
            OnPropertyChanged(nameof(textSearchSeries));
        }
    }

    private string textSearchSeries;

    private async Task ActionSearchSeriesByTitle()
    {
        var series = await serieService.GetSeriesByTitle(textSearchSeries);

        if (series == null)
        {
            await ShowDialog("titre " + textSearchSeries + " introuvable !");
        }
        else
        {
            seriesList.Clear();

            foreach (var serie in series)
            {
                seriesList.Add(serie);
            }
            SeriesList = seriesList;
        }
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
