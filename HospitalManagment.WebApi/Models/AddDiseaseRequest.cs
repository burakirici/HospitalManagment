using System.ComponentModel.DataAnnotations;

namespace HospitalManagment.WebApi.Models
{
    public class AddDiseaseRequest
    {
        [Required]
  
        public string DiseaseName { get; set; }
    }
}
