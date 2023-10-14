using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SeriesApi.Models.EntityFramework
{
    [Table("t_j_notation_not")]
    public partial class Notation
    {
        [Column("utl_id")]
        public int UtilisateurId { get; set; }

        [Column("ser_id")]
        public int SerieId { get; set; }

        [Column("not_note")]
        [Range(0, 5)]
        public int Note { get; set; }

        [ForeignKey("UtilisateurId")]
        [InverseProperty("NotesUtilisateur")]
        [JsonIgnore]
        public virtual Utilisateur? UtilisateurNotant { get; set; }

        [ForeignKey("SerieId")]
        [InverseProperty("NotesSerie")]
        [JsonIgnore]
        public virtual Serie? SerieNotee { get; set; }
    }
}
