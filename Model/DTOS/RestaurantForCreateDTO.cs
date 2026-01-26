namespace Proyecto_Final_ProgramacionWEB.Model.DTOS
{
    public class RestaurantForCreateDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageURL { get; set; }
        public string? BGImage { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
       // public int Id_Gastronomy { get; set; }
    }
}
