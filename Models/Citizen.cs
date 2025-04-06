using System.ComponentModel.DataAnnotations;

namespace MunicipalManagementSystem.Models
{
    public class Citizen
    {
        [Key]
        public int CitizenID { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        // Navigation properties
        public ICollection<ServiceRequest>? ServiceRequests { get; set; }
        public ICollection<Report>? Reports { get; set; }
    }
}
