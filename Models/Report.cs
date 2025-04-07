using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MunicipalManagementSystem.Models
{
    public class Report
    {
        [Key]
        public int ReportID { get; set; }

        [Required(ErrorMessage = "Citizen is required")]
        [Display(Name = "Citizen")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Citizen selection")]
        public int CitizenID { get; set; }

        [Required(ErrorMessage = "Report Type is required")]
        [StringLength(50, ErrorMessage = "Report Type cannot exceed 50 characters")]
        [Display(Name = "Report Type")]
        public string ReportType { get; set; }

        [Required(ErrorMessage = "Details are required")]
        [StringLength(500, ErrorMessage = "Details cannot exceed 500 characters")]
        public string Details { get; set; }

        [Display(Name = "Submission Date")]
        public DateTime SubmissionDate { get; set; } = DateTime.Now;

        [Required]
        public string Status { get; set; } = "Under Review";

        [ForeignKey("CitizenID")]
        public Citizen Citizen { get; set; }
    }
}