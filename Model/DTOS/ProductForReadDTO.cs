namespace Proyecto_Final_ProgramacionWEB.Model.DTOS
{
    public class ProductForReadDTO
    {
        public int Id_Product { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Price { get; set; }
        public int? Discount { get; set; }
        public string? URLImage { get; set; }
        public int Id_Category { get; set; }
    }
}
