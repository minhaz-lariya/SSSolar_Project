namespace SSSolar_Project.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string? Category { get; set; }
        public string? Brand { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? ProductImage { get; set; }
        public string? BriefDescription { get; set; }
        public string? SEOKeywords { get; set; }
		public string? isShowPrice { get; set; }
        public string? isSpecialProduct { get; set; }
    }
}
