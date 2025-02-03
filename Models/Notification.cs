namespace SSSolar_Project.Models
{
	public class Notification
	{
        public int Id { get; set; }
		public string? Title { get; set; } = null;
		public string? Message { get; set; }
		public string? Status { get; set; }
		public DateTime? CreatedAt { get; set; } = System.DateTime.Now;
	}
}
