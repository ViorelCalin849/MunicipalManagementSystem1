using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MunicipalManagementSystem.Models
{
    public class ServiceRequest
    {
        [Key]
        public int RequestID { get; set; }

        [Required]
        [Display(Name = "Citizen")]
        public int CitizenID { get; set; }

        [Required]
        [Display(Name = "Service Type")]
        public string ServiceType { get; set; }

        [Display(Name = "Request Date")]
        public DateTime RequestDate { get; set; } = DateTime.Now;

        [Required]
        public string Status { get; set; } = "Pending";

        // Navigation properties
        [ForeignKey("CitizenID")]
        public Citizen Citizen { get; set; }

    }
}