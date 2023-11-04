using ClientWinuiAPI.Models;
using ClientWinuiAPI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;
using static com.sun.tools.javap.TypeAnnotationWriter;

namespace ClientWinuiAPI.ViewModels;

public partial class SerieDetailsViewModel : ObservableRecipient
{
    public Serie? serie;
    private readonly UserService _userService;
    public SerieDetailsViewModel()
    {
        Serie = new Serie();
        Serie = serie;
        _userService = UserService.GetService;
        listSerieNotes = new ObservableCollection<Notation>();
        AddNotationtoSerie = new AsyncRelayCommand(PerformAddNotationtoSerie);

    }

    public Serie? Serie
    {
        get => serie;
        set
        {
            if (serie != value)
            {
                serie = value;
                if (serie?.NotesSerie !=null)
                {
                    foreach (var notation in serie.NotesSerie)
                    {
                        listSerieNotes.Add(notation);
                    }
                    ListSerieNotes = listSerieNotes;
                }
                OnPropertyChanged(nameof(serie));
            }
        }
    }
    public ICommand AddNotationtoSerie
    {
        get; set;
    }

    private async Task PerformAddNotationtoSerie()
    {
        var utilisateur = await _userService.GetUserByEmail(TextUserMail);
        if (utilisateur != null)
        {
            var note = NotationValue;

            var notation = new Notation
            {
                UtilisateurId = utilisateur.UtilisateurId,
                SerieId = Serie.SerieId,
                Note = note,
                UtilisateurNotant = utilisateur,
                SerieNotee = Serie
            };
            listSerieNotes.Add(notation);
            Serie.NotesSerie.Add(notation);
            OnPropertyChanged(nameof(Serie));
            ListSerieNotes = listSerieNotes;
            // Serialize
            //var json = JsonSerializer.Serialize(notation);

            // Save to a JSON file
            //File.WriteAllText("userNotes.json", json);

           /* // Deserialize
            var jsontxt = File.ReadAllText("userNotes.json");
            var notes = JsonSerializer.Deserialize<List<Notation>>(json);*/
        }
        else
        {
            await ShowDialog("email utilisateur introuvable");
        }

    }


    private ObservableCollection<Notation> listSerieNotes;
    public ObservableCollection<Notation> ListSerieNotes
    {
        get => listSerieNotes;
        set
        {
            if (listSerieNotes != value)
            {
                listSerieNotes = value;
                OnPropertyChanged(nameof(listSerieNotes));
            }
        }
    }

    private int notationValue;
    public int NotationValue
    {
        get => notationValue;
        set
        {
            if (notationValue != value)
            {
                notationValue = value;
                OnPropertyChanged(nameof(notationValue));
            }
        }
    }

    private string textUserMail;
    public string TextUserMail
    {
        get => textUserMail;
        set
        {
            textUserMail = value;
            OnPropertyChanged(nameof(textUserMail));
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
