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

        // Al ponerle el '?' y el '=' null, le decimos a .NET: 
        // "Al recibir un POST, no me obligues a que el front mande este objeto embebido".
        [ForeignKey("DirectorId")]
        public virtual Director? Director { get; set; } = null!;
    }
}
