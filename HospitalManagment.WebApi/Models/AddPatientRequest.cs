using System.ComponentModel.DataAnnotations;

namespace HospitalManagment.WebApi.Models
{
    public class AddPatientRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string SurName { get; set; }
        [Required]
        public string HealthStatus { get; set; }
        [Required]
        public List<int> DiseaseIds { get; set; }
    }
}
