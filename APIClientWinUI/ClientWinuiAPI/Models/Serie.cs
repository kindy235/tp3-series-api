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

    public double AverageNote
    {
        get
        {
            if (NotesSerie != null && NotesSerie.Count > 0)
            {
                // Calculate the average note using LINQ.
                return NotesSerie.Average(notation => notation.Note);
            }
            else
            {
                return 0.0; // Return a default value when there are no ratings.
            }
        }
    }
}
