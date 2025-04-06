using System.ComponentModel.DataAnnotations;

namespace MunicipalManagementSystem.Models
{
    public class Report
    {
        [Key]
        public int ReportID { get; set; }

        [Required]
        public int CitizenID { get; set; }

        [Required]
        public int StaffID { get; set; }

        [Required]
        public string ReportType { get; set; }

        [Required]
        public string Details { get; set; }

        public DateTime SubmissionDate { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Under Review";

        // Navigation properties
        public Citizen? Citizen { get; set; }
        public Staff? Staff { get; set; }
    }
}
