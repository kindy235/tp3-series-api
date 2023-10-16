namespace ClientWinuiAPI.Models;

public class Notation
{
    public int UtilisateurId { get; set; }

    public int SerieId { get; set; }
 
    public int Note { get; set; }

    public virtual Utilisateur? UtilisateurNotant { get; set; }

    public virtual Serie? SerieNotee { get; set; }
}
