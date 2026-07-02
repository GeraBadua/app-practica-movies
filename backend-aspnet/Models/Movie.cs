using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_aspnet.Models
{
    [Table("movies")]
    public class Movie
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column("release_year")]
        public int ReleaseYear { get; set; }

        [Required]
        [StringLength(50)]
        [Column("genre")]
        public string Genre { get; set; } = string.Empty;

        [Required]
        [Column("duration")]
        public int Duration { get; set; }

        [Required]
        [Column("director_id")]
        public int DirectorId { get; set; }

        // Propiedades de navegación de Entity Framework
        [ForeignKey("DirectorId")]
        public virtual Director? Director { get; set; }
    }
}