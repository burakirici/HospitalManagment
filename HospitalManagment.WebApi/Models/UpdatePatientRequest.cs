using System.ComponentModel.DataAnnotations;

namespace HospitalManagment.WebApi.Models
{
    public class UpdatePatientRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string SurName { get; set; }
        [Required]
        public string HealthStatus { get; set; }
        public List<int> DiseaseIds { get; set; }
    }
}
