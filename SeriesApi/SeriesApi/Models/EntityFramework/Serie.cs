using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SeriesApi.Models.EntityFramework
{
    [Table("t_e_serie_ser")]
    public partial class Serie
    {
        [Key]
        [Column("ser_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SerieId { get; set; }

        [Required]
        [Column("ser_titre", TypeName = "varchar(100)")]
        [Index(IsUnique = false)] // Non unique
        public string? Titre { get; set; }

        [Column("ser_resume", TypeName = "TEXT")]
        public string? Resume { get; set; }

        [Column("ser_nbsaisons")]
        public int? NbSaisons { get; set; }

        [Column("ser_nbepisodes")]
        public int? NbEpisodes { get; set; }

        [Column("ser_anneecreation")]
        public int? AnneeCreation { get; set; }

        [Column("ser_network", TypeName = "varchar(50)")]
        public string? Network { get; set; }

        [InverseProperty("SerieNotee")]
        public virtual ICollection<Notation> NotesSerie { get; set; } = new List<Notation>();
    }

}
