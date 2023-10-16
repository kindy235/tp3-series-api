namespace ClientWinuiAPI.Models;

public class Serie
{
    public int SerieId { get; set; }

    public string? Titre { get; set; }

    public string? Resume { get; set; }

    public int? NbSaisons { get; set; }

    public int? NbEpisodes { get; set; }

    public int? AnneeCreation { get; set; }

    public string? Network { get; set; }

    public virtual ICollection<Notation> NotesSerie { get; set; } = new List<Notation>();
}
