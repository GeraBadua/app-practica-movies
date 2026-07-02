using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend_aspnet.Models
{
    [Table("directors")]
    public class Director
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Column("nationality")]
        public string Nationality { get; set; } = string.Empty;

        [Required]
        [Column("age")]
        public int Age { get; set; }

        [Required]
        [Column("active")]
        public bool Active { get; set; } = true;

        [JsonIgnore]
        public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
