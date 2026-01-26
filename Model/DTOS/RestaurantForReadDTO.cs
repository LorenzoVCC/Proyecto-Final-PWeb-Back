namespace Proyecto_Final_ProgramacionWEB.Model.DTOS
{
    public class RestaurantForReadDTO
    {
        public int Id_Restaurant { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageURL { get; set; }
        public string? BGImage { get; set; }
        public string Address { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string Slug { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
       // public int Id_Gastronomy { get; set; }
    }
}
