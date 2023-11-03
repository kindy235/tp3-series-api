using ClientWinuiAPI.Models;
using ClientWinuiAPI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using static com.sun.tools.javap.TypeAnnotationWriter;

namespace ClientWinuiAPI.ViewModels;

public partial class SerieDetailsViewModel : ObservableRecipient
{
    public Serie? serie;
    //private readonly UserService _userService;
    public SerieDetailsViewModel()
    {
        Serie = new Serie();
        Serie = serie;
        //_userService = UserService.GetService;
        listSerieNotes = new ObservableCollection<Notation>();
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

    private void PerformAddNotationtoSerie()
    {
        var note = NotationValue;

        var notation = new Notation {
            UtilisateurId = note%2,
            SerieId = Serie.SerieId,
            Note = note,
        };
        listSerieNotes.Add(notation);
        Serie.NotesSerie.Add(notation);
        OnPropertyChanged(nameof(Serie));
        ListSerieNotes = listSerieNotes;
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
}
