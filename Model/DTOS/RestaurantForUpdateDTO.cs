namespace Proyecto_Final_ProgramacionWEB.Model.DTOS
{
    public class RestaurantForUpdateDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Description { get; set; }
        public string? ImageURL { get; set; }

        public string? BGImage { get; set; }
        public string? Address { get; set; }
        public string? Slug { get; set; }
        public bool? IsActive { get; set; }
        //public int? Id_Gastronomy { get; set; }
    }
}
