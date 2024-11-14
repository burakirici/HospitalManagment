using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagment.Business.Operations.Patient.Dtos
{
    public class AddPatientDto
    {
        
        public string Name { get; set; }
        
        public string SurName { get; set; }
        public  string HealthStatus { get; set; }
        public List<int> DiseaseIds { get; set; }
    }
}
