using System.ComponentModel.DataAnnotations.Schema;

namespace SSSolar_Project.Models
{
    public class AdminMaster
    {
        public int Id { get; set; }
        public String? FullName { get; set; }
        public String? UserName { get; set; }
        public String? ContactNo { get; set; }
        public string? AlternateNo { get; set; }
        public String? Email { get; set; }
        public String? Password { get; set; }

        [NotMapped]
        public String? newPassword { get; set; }

    }
}
