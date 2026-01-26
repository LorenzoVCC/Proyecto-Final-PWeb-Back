//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace Proyecto_Final_ProgramacionWEB.Entities
//{
//    public class Gastronomy
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int Id_Gastronomy { get; set; }
        
//        /////////////////////////// ///
        
//        [Required]
//        public string Name { get; set; } = string.Empty;

//        public string? ImageUrl { get; set; }

//        public ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();

//    }
//}
