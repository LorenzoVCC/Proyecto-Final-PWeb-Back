namespace Proyecto_Final_ProgramacionWEB.Model.DTOS
{
    public class ProductSearchDTO
    {
        public int RestaurantId { get; set; }

        public string? Q { get; set; }
        public int? CategoryId { get; set; }

        public bool? Featured { get; set; }
        public bool? HappyHour { get; set; }

        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}