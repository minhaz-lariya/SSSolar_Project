namespace SSSolar_Project.Models
{
    public class SpecialPackages
    {
        public int Id { get; set; }
        public string? imgPath { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string? Status { get; set; }
    }
}
