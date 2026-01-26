using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Final_ProgramacionWEB.Entities
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Category { get; set; }
        
        [Required]
        public string Name { get; set; } = string.Empty;        
        /// ////////////////

        [ForeignKey("Restaurant")]
        public int Id_Restaurant { get; set; }
        public Restaurant Restaurant { get; set; } = null!;
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
