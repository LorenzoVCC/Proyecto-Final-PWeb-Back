using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Final_ProgramacionWEB.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Product { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public int Price { get; set; }

        public int? Discount { get; set; }

        public string? URLImage { get; set; }


        [ForeignKey("Category")]
        public int Id_Category { get; set; }
        public Category Category { get; set; } = null!;

    }
}
