using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Final_ProgramacionWEB.Entities
{
    public class Restaurant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Restaurant { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
      
        [Required]
        public string Password { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageURL { get; set; }
        public string? BGImage { get; set; }
        [Required]
        public string Address { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        [Required]
        public string Slug { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        /// ////////////
        //[ForeignKey("Gastronomy")]
        //public int Id_Gastronomy { get; set; }
        //public Gastronomy Gastronomy { get; set; } = null!;
        /// ////////////
        public ICollection<Category> Categories { get; set; } = new List<Category>();

    }
}
