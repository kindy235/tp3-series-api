using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SeriesApi.Models.EntityFramework
{
    [Table("t_e_utilisateur_utl")]
    public partial class Utilisateur
    {
        [Key]
        [Column("utl_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UtilisateurId { get; set; }

        [Column("utl_nom", TypeName = "varchar(50)")]
        public string? Nom { get; set; }

        [Column("utl_prenom", TypeName = "varchar(50)")]
        public string? Prenom { get; set; }

        [Column("utl_mobile", TypeName = "char(10)")]
        [RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "le mobile doit contenir 10 chiffres")]
        public string? Mobile { get; set; }

        [Required]
        [Column("utl_mail", TypeName = "varchar(100)")]
        [EmailAddress(ErrorMessage = "adresse email invalide")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La longueur d’un email doit être comprise entre 6 et 100 caractères.")]
        [Index(IsUnique = true)] // Unique
        public string? Mail { get; set; }

        [Required]
        [Column("utl_pwd", TypeName = "varchar(64)")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[\\W_])[A-Za-z\\d\\W_]{6,10}$",
        ErrorMessage = "Le mot de passe doit contenir entre 6 et 10 caractères, au moins 1 lettre majuscule, 1 lettre minuscule, 1 chiffre et 1 caractère spécial.")]
        public string? Pwd { get; set; }

        [Column("utl_rue", TypeName = "varchar(200)")]
        public string? Rue { get; set; }

        [Column("utl_cp", TypeName = "char(5)")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "le code postal doit contenir 5 chiffres")]
        public string? CodePostal { get; set; }

        [Column("utl_ville", TypeName = "varchar(50)")]
        public string? Ville { get; set; }

        [Column("utl_pays", TypeName = "varchar(50)")]
        [DefaultValue("France")]
        public string? Pays { get; set; } = "France";

        [Column("utl_latitude")]
        public decimal? Latitude { get; set; }

        [Column("utl_longitude")]
        public decimal? Longitude { get; set; }

        [Required]
        [Column("utl_datecreation", TypeName = "date")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateCreation { get; set; }

        [InverseProperty("UtilisateurNotant")]
        public virtual ICollection<Notation> NotesUtilisateur { get; set; } = new List<Notation>();
    }
}
